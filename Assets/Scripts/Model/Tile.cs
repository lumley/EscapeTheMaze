using System.Collections.Generic;

namespace Model
{
    public class Tile
    {
        private Dictionary<Direction, Tile> neighbourMap = new Dictionary<Direction,Tile>(4);

        public enum Direction
        {
            NORTH = 0, EAST, SOUTH, WEST
        }

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

    }

    namespace Direction
    {
        public class Utils
        {
            public static Tile.Direction[] GetAllDirections()
            {
                return (Tile.Direction[]) System.Enum.GetValues(typeof(Tile.Direction));
            }

            public static Tile.Direction[] GetAllDirectionsExcluding(params Tile.Direction[] exlusions)
            {
                Tile.Direction[] directions = new Tile.Direction[4 - exlusions.Length];
                int directionCount = 0;
                foreach (Tile.Direction direction in GetAllDirections())
                {
                    if(System.Array.IndexOf(exlusions, direction) < 0)
                    {
                        directions[directionCount++] = direction;
                    }
                }

                return directions;
            }

            public static Tile.Direction Reverse(Tile.Direction direction)
            {
                return (Tile.Direction)(((int)direction + 2) % 4);
            }
        }
    }
}