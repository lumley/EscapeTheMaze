using System.Collections.Generic;

namespace Model
{
    public class Tile
    {
        private Dictionary<Direction, Tile> neighbourMap = new Dictionary<Direction,Tile>(4);
        private Dictionary<TileAttribute.Type, TileComponent> attributeMap;

        public void SetNeighbour(Tile neighbour, Direction directionFromMe) // TODO: Unit test this!
        {
            this.neighbourMap[directionFromMe] = neighbour;
        }

        internal void AddAttribute(TileComponent attribute)
        {
            if (this.attributeMap == null) {
                this.attributeMap = new Dictionary<TileAttribute.Type, TileComponent>();
            }
            
            this.attributeMap[attribute.GetType()] = attribute;
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