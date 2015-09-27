using System.Collections.Generic;

namespace Model
{
    public class Tile
    {
        private Dictionary<Direction, Tile> neighbourMap = new Dictionary<Direction,Tile>(4);

        public enum Direction
        {
            NORTH = 0, WEST, SOUTH, EAST
        }

        public void SetNeighbour(Tile neighbour, Direction directionFromMe)
        {
            this.neighbourMap.Add(directionFromMe, neighbour);
        }

        public Tile GetNeighbour(Direction directionFromMe)
        {
            if (this.neighbourMap.ContainsKey(directionFromMe))
            {
                return this.neighbourMap[directionFromMe];
            }
            
            return null;
        }

    }
}