using UnityEngine;
using System.Collections.Generic;
using Model;
using System;

//[ExecuteInEditMode] // Use this to also awake inside Unity Editor
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

        }
    }

    private void CreateWalls(IntPair position, Tile tile)
    {
        foreach (Direction direciton in Utils.GetAllDirections())
        {
            if (tile.GetNeighbour(direciton) == null)
            {
                CreateWall(position.Move(direciton));
            }
        }
    }

    private void CreateWall(IntPair position)
    {
        Vector3 gameObjectPosition = new Vector3(position.x, 1.0f, position.y) + transform.position;
        GameObject tileInstance = Instantiate(prefabHolder.wall, gameObjectPosition, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = boardHolder.transform;
    }

    private void CreateFloor(IntPair position)
    {
        Vector3 gameObjectPosition = new Vector3(position.x, 0.0f, position.y) + transform.position;
        GameObject tileInstance = Instantiate(prefabHolder.floor, gameObjectPosition, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = boardHolder.transform;
    }

    private void CreateCeiling(IntPair position)
    {
        Vector3 gameObjectPosition = new Vector3(position.x, 2.0f, position.y) + transform.position;
        GameObject tileInstance = Instantiate(prefabHolder.ceiling, gameObjectPosition, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = boardHolder.transform;
    }

    [Serializable]
    public struct PrefabHolder
    {
        public GameObject floor;
        public GameObject wall;
        public GameObject ceiling;
    }
}
