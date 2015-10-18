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

        [SerializeField]
        private GameObject boardHolder;

        public Dictionary<IntPair, Tile> GenerateMap()
        {
            int roomCount = roomsCount.Random;
            Corridor[] corridors = GenerateCorridors(roomCount);

            Dictionary<IntPair, Tile> createdTileMap = GenerateTiles(corridors);

            DrawTiles(createdTileMap);

            return createdTileMap;
        }

        private Dictionary<IntPair, Tile> GenerateTiles(Corridor[] corridors)
        {
            Dictionary<IntPair, Tile> allTiles = new Dictionary<IntPair, Tile>();
            GenerateTilesForRoom(corridors[0].entranceRoom, this.startingPoint, allTiles);
            // Create starting room from first corridor
            // For each corridor
            //  Select wall in the direction and choose one tile to start the corridor
            //  Generate corridor from that tile, in the direction of corridor
            //  At the end of corridor, choose a random x/y position from exit room
            //  Generate room from top-left corner


            return allTiles;
        }

        private void GenerateTilesForRoom(Room room, IntPair topLeftCorner, Dictionary<IntPair, Tile> allTiles)
        {
            // Start filling up tiles from top left corner using width/height
        }

        private void DrawTiles(Dictionary<IntPair, Tile> createdTileMap)
        {

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
