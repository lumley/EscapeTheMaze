using System;
using System.Collections.Generic;
using Commons;
using Map.Model;
using UnityEngine;
using Type = Map.Model.TileAttribute.Type;

//[ExecuteInEditMode] // Use this to also awake inside Unity Editor
namespace Map
{
    public class TileMap : MonoBehaviour {

        public PrefabHolder prefabHolder = new PrefabHolder();

        [SerializeField]
        private BoardCreator boardCreator = new BoardCreator();

        private Dictionary<IntPair, Tile> createdTileMap;
        private GameObject boardHolder;

        public IEnumerable<Tile> Tiles
        {
            get {
                return this.createdTileMap.Values;
            }
        }
    
        public Dictionary<IntPair, Tile> Map2D {
            get {
                return this.createdTileMap;
            }
        }

        public void GenerateMap()
        {
            createdTileMap = boardCreator.GenerateMap();
            CreateBoardHolder();
            DrawTiles(createdTileMap);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CreateBoardHolder()
        {
            boardHolder = new GameObject("boardHolder");
            boardHolder.transform.position = transform.position;
        }

        private void DrawTiles(Dictionary<IntPair, Tile> createdTileMap)
        {
            foreach (KeyValuePair<IntPair, Tile> entry in createdTileMap)
            {
                IntPair position = entry.Key;
                Tile tile = entry.Value;
                CreateFloor(position);
                CreateCeiling(position);
                CreateWalls(position, tile);

                CreateAttribute(position, tile);
            }
        }

        private void CreateAttribute(IntPair position, Tile tile)
        {
            if (tile.HasAttribute(Type.ENGING_POINT))
            {
                CreateEndingAttribute(position);
            }
            if (tile.HasAttribute(Type.ENEMY_SPAWNING_POINT))
            {
                CreateEnemy(position);
            }
        }

        private void CreateEndingAttribute(IntPair position)
        {
            CreateGameObject(prefabHolder.endingPoint, position, 0.5f);
        }

        private void CreateGameObject(GameObject what, IntPair where, float y)
        {
            Vector3 gameObjectPosition = new Vector3(where.x, y, where.y) + transform.position;
            GameObject gameObjectInstance = Instantiate(what, gameObjectPosition, Quaternion.identity) as GameObject;
            gameObjectInstance.transform.parent = boardHolder.transform;
        }

        private void CreateWalls(IntPair position, Tile tile)
        {
            foreach (Direction direciton in Model.Utils.GetAllDirections())
            {
                if (tile.GetNeighbour(direciton) == null)
                {
                    CreateWall(position.Move(direciton));
                }
            }
        }

        private void CreateWall(IntPair position)
        {
            CreateGameObject(prefabHolder.wall, position, 1.0f);
        }

        private void CreateFloor(IntPair position)
        {
            CreateGameObject(prefabHolder.floor, position, 0.0f);
        }

        private void CreateCeiling(IntPair position)
        {
            CreateGameObject(prefabHolder.ceiling, position, 2.0f);
        }
    
        private void CreateEnemy(IntPair position){
            CreateGameObject(prefabHolder.enemy, position, 0.0f);
        }

        [Serializable]
        public struct PrefabHolder
        {
            public GameObject floor;
            public GameObject wall;
            public GameObject ceiling;
            public GameObject endingPoint;
            public GameObject enemy;
        }
    }
}
