using UnityEngine;
using System.Collections.Generic;
using System;

namespace Model
{
    [Serializable]
    public class BoardCreator
    {
        [SerializeField]
        private IntPair startingPoint = new IntPair(0, 0);

        [SerializeField]
        private IntRange roomsCount = new IntRange(5, 10);

        [SerializeField]
        private IntRange roomsWidth = new IntRange(2, 4);

        [SerializeField]
        private IntRange roomsHeight = new IntRange(2, 4);

        [SerializeField]
        private IntRange corridorLength = new IntRange(3, 6);

        public float enemyDensity=5.0f;

        public Dictionary<IntPair, Tile> GenerateMap()
        {
            int roomCount = roomsCount.Random;
            Corridor[] corridors = GenerateCorridors(roomCount);

            Dictionary<IntPair, Tile> createdTileMap = GenerateTiles(corridors);

            return createdTileMap;
        }

        private Dictionary<IntPair, Tile> GenerateTiles(Corridor[] corridors)
        {
            Dictionary<IntPair, Tile> allTiles = new Dictionary<IntPair, Tile>();

            // Create starting room from first corridor
            IntPair position = this.startingPoint;
            GenerateTilesForRoom(corridors[0].entranceRoom, position, allTiles);
            
            Tile startingTile = SetStartingTile(allTiles);
            
            foreach (Corridor corridor in corridors)
            {
                //  Select wall in the direction and choose one tile to start the corridor
                position = MovePositionToBorderOfRoomInGivenDirection(position, corridor.entranceRoom, corridor.direction);
                //  Generate corridor from that tile, in the direction of corridor
                position = GenerateTilesForCorridor(corridor, position, allTiles);
                //  At the end of corridor, choose a random x/y position from exit room
                position = MovePositionFromCorridorBeginningToBottomLeftEndRoom(position, corridor);
                //  Generate room from top-left corner
                GenerateTilesForRoom(corridor.exitRoom, position, allTiles);
                
            }

            SetEndingTile(allTiles);

            return allTiles;
        }

        private void SetEndingTile(Dictionary<IntPair, Tile> allTiles)
        {
            Tile randomTile = RandomProvider.GetRandomElement(allTiles.Values);
            if (randomTile.HasAttribute(TileAttribute.Type.SPAWNING_POINT))
            {
                randomTile = RandomProvider.GetRandomElementExcluding(allTiles.Values, randomTile);
            }
            
            randomTile.AddAttribute(new TileAttribute.EndingPoint());
        }

        private Tile SetStartingTile(Dictionary<IntPair, Tile> allTiles)
        {
            // Set any random tile from the first room as a starting point
            var iterator = allTiles.Values.GetEnumerator();
            Tile startingTile = null;
            if (iterator.MoveNext())
            {
                startingTile = iterator.Current;
                startingTile.AddAttribute(new TileAttribute.SpawningPoint());
            }

            return startingTile;
        }

        // Assumes positon is at the end of the corridor
        private IntPair MovePositionFromCorridorBeginningToBottomLeftEndRoom(IntPair position, Corridor corridor)
        {
            int offset;
            switch (corridor.direction)
            {
                case Direction.WEST:
                    offset = corridor.exitRoom.size.x;
                    break;
                case Direction.SOUTH:
                    offset = corridor.exitRoom.size.y;
                    break;
                default:
                    offset = 1;
                    break;
            }

            return position.Move(corridor.direction, offset);
        }

        // Assumes position is in top-left corner of room
        private IntPair MovePositionToBorderOfRoomInGivenDirection(IntPair position, Room room, Direction direction)
        {
            int offset;
            // TODO: Add some randomized factor so that rooms are not linked always from top-left corner
            switch (direction)
            {
                case Direction.EAST:
                    offset = room.size.x - 1;
                    break;
                case Direction.NORTH:
                    offset = room.size.y - 1;
                    break;
                default:
                    offset = 0;
                    break;
            }
            return position.Move(direction, offset);
        }

        // Assumes position is at end of starting room
        // Returns the position where the corridor ends
        private IntPair GenerateTilesForCorridor(Corridor corridor, IntPair position, Dictionary<IntPair, Tile> allTiles)
        {
            for(int i=0; i < corridor.length; ++i)
            {
                position = position.Move(corridor.direction);
                CreateOrUpdateTileIn(position, allTiles);
            }
            return position;
        }

        private void GenerateTilesForRoom(Room room, IntPair topLeftCorner, Dictionary<IntPair, Tile> allTiles)
        {
            // Start filling up tiles from top left corner using width/height
            int width = room.size.x;
            int height = room.size.y;
            Tile tile;

            for (int i=0; i< width; ++i)
            {
                for (int j=0; j< height; ++j)
                {
                    tile=CreateOrUpdateTileIn(new IntPair(i + topLeftCorner.x, j + topLeftCorner.y), allTiles);
                    if (UnityEngine.Random.Range(0.0f, 100.0f) <= enemyDensity){
                        tile.AddAttribute(new TileAttribute.EnemySpawningPoint());
                    }
                }
            }
        }

        private Tile CreateOrUpdateTileIn(IntPair position, Dictionary<IntPair, Tile> allTiles)
        {
            Tile tile;
            allTiles.TryGetValue(position, out tile);
            if (tile == null)
            {
                tile = new Tile();
                allTiles.Add(position, tile);
            }

            foreach (Direction direction in Utils.GetAllDirections())
            {
                IntPair neighbourPosition = position.Move(direction);
                Tile neighbour;
                allTiles.TryGetValue(neighbourPosition, out neighbour);
                if(neighbour != null)
                {
                    tile.BindNeighbours(neighbour, direction);
                }
            }
            return tile;
        }

        // Visible for testing
        public Corridor[] GenerateCorridors(int roomCount)
        {
            int corridorCount = roomCount - 1;
            Corridor[] corridors = new Corridor[corridorCount];
            Room lastCreatedRoom = new Room(this.roomsWidth, this.roomsHeight);

            Direction[] excludedDirections = null;
            for (int i = 0; i < corridorCount; ++i)
            {
                Direction[] possibleDirections = Utils.GetAllDirectionsExcluding(excludedDirections);
                Direction direction = RandomProvider.GetRandomElement(possibleDirections);
                Room exitRoom = new Room(this.roomsWidth, this.roomsHeight);
                corridors[i] = new Corridor(this.corridorLength, lastCreatedRoom, exitRoom, direction);
                lastCreatedRoom = exitRoom;
                
                if(excludedDirections == null)
                {
                    excludedDirections = new Direction[1];
                }
                excludedDirections[0] = Utils.Reverse(direction);
            }

            return corridors;
        }

        public struct Room
        {
            public IntPair size;

            public Room(IntRange widthRange, IntRange heightRange)
            {
                this.size = new IntPair(widthRange.Random, heightRange.Random);
            }
        }

        // Visible for testing
        public struct Corridor
        {
            public int length;
            public Room entranceRoom;
            public Room exitRoom;
            public Direction direction; // From entrance to exit

            public Corridor(IntRange lengthRange, Room entranceRoom, Room exitRoom, Direction direction)
            {
                this.length = lengthRange.Random;
                this.entranceRoom = entranceRoom;
                this.exitRoom = exitRoom;
                this.direction = direction;
            }
        }
    }
}
