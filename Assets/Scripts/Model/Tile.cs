using System;
using System.Collections.Generic;
using Model.TileAttribute;

namespace Model
{
    public class Tile
    {
        private Dictionary<Direction, Tile> neighbourMap = new Dictionary<Direction,Tile>(4);
        private Dictionary<TileAttribute.Type, TileComponent> attributeMap;

        public void SetNeighbour(Tile neighbour, Direction directionFromMe) // TODO: Unit test this!
        {
            Tile alreadyNeighbour;
            if(!this.neighbourMap.TryGetValue(directionFromMe, out alreadyNeighbour))
            {
                this.neighbourMap.Add(directionFromMe, neighbour);
            }
            else if(!alreadyNeighbour.Equals(neighbour))
            {
                throw new System.ArgumentException("Attempting to add a different neighbour in a direction where there is already one");
            }
        }

        internal void AddAttribute(TileComponent attribute)
        {
            if (this.attributeMap == null) {
                this.attributeMap = new Dictionary<TileAttribute.Type, TileComponent>();
            }
            
            
        }

        public Tile GetNeighbour(Direction directionFromMe) // TODO: Unit test this!
        {
            Tile neighbour;
            this.neighbourMap.TryGetValue(directionFromMe, out neighbour);
            return neighbour;
        }

        public bool HasAttribute<T>(TileAttribute.Type attributeType)
        {
            if (this.attributeMap != null)
            {
                return this.attributeMap.ContainsKey(attributeType);
            }
            return false;
        }
        
        public T GetAttribute<T>(TileAttribute.Type attributeType) where T : TileComponent{
            TileComponent component = null;
            if (this.attributeMap != null)
            {
                this.attributeMap.TryGetValue(attributeType, out component);
            }
            return (T) component;
        }

    }
}