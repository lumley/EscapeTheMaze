using System.Collections.Generic;

namespace Model
{
    public class Tile
    {
        private Dictionary<Direction, Tile> neighbourMap = new Dictionary<Direction,Tile>(4);
        private Dictionary<int, TileComponent> attributeMap;

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

        public Tile GetNeighbour(Direction directionFromMe) // TODO: Unit test this!
        {
            Tile neighbour;
            this.neighbourMap.TryGetValue(directionFromMe, out neighbour);
            return neighbour;
        }

        public bool HasAttribute<T>(int attributeType)
        {
            if (this.attributeMap != null)
            {
                return this.attributeMap.ContainsKey(attributeType);
            }
            return false;
        }

    }
}