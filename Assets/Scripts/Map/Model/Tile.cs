using System.Collections.Generic;
using Map.Model.TileAttribute;

namespace Map.Model
{
    public class Tile
    {
        private Dictionary<Direction, Tile> neighbourMap = new Dictionary<Direction,Tile>(4);
        private Dictionary<Type, ITileComponent> attributeMap;

        public void BindNeighbours(Tile neighbour, Direction directionFromMe)
        {
            SetNeighbour(neighbour, directionFromMe);
            Direction reversedDirection = Utils.Reverse(directionFromMe);
            neighbour.SetNeighbour(this, reversedDirection);
        }

        private void SetNeighbour(Tile neighbour, Direction directionFromMe)
        {
            this.neighbourMap[directionFromMe] = neighbour;
        }

        public void AddAttribute(ITileComponent attribute)
        {
            if (this.attributeMap == null) {
                this.attributeMap = new Dictionary<Type, ITileComponent>();
            }
            
            this.attributeMap[attribute.GetType()] = attribute;
        }

        public Tile GetNeighbour(Direction directionFromMe)
        {
            Tile neighbour;
            this.neighbourMap.TryGetValue(directionFromMe, out neighbour);
            return neighbour;
        }

        public bool HasAttribute(global::Map.Model.TileAttribute.Type attributeType)
        {
            if (this.attributeMap != null)
            {
                return this.attributeMap.ContainsKey(attributeType);
            }
            return false;
        }
        
        public T GetAttribute<T>(global::Map.Model.TileAttribute.Type attributeType) where T : ITileComponent{
            ITileComponent component = null;
            if (this.attributeMap != null)
            {
                this.attributeMap.TryGetValue(attributeType, out component);
            }
            return (T) component;
        }

    }
}