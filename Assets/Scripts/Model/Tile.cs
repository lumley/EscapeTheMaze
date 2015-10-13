using System.Collections.Generic;

namespace Model
{
    public class Tile
    {
        private Dictionary<Direction, Tile> neighbourMap = new Dictionary<Direction,Tile>(4);
        private Dictionary<TileAttribute.Type, TileComponent> attributeMap;

        public void BindNeighbours(Tile neighbour, Direction directionFromMe)
        {
            SetNeighbour(neighbour, directionFromMe);
            Model.Direction reversedDirection = Utils.Reverse(directionFromMe);
            neighbour.SetNeighbour(this, reversedDirection);
        }

        private void SetNeighbour(Tile neighbour, Direction directionFromMe)
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

        public Tile GetNeighbour(Direction directionFromMe)
        {
            Tile neighbour;
            this.neighbourMap.TryGetValue(directionFromMe, out neighbour);
            return neighbour;
        }

        public bool HasAttribute(TileAttribute.Type attributeType)
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