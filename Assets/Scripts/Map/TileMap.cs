using UnityEngine;
using System.Collections.Generic;

//[ExecuteInEditMode] // Use this to also awake inside Unity Editor
public class TileMap : MonoBehaviour {

    public GameObject floorPrefab;

    [SerializeField]
    private Model.BoardCreator boardCreator = new Model.BoardCreator();

    private Dictionary<IntPair, Model.Tile> createdTileMap;
    private GameObject boardHolder;

    public IEnumerable<Model.Tile> Tiles
    {
        get {
            return this.createdTileMap.Values;
        }
    }
    
    public Dictionary<IntPair, Model.Tile> Map2D {
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

    private void DrawTiles(Dictionary<IntPair, Model.Tile> createdTileMap)
    {
        foreach (KeyValuePair<IntPair, Model.Tile> entry in createdTileMap)
        {
            IntPair position = entry.Key;
            Vector3 gameObjectPosition = new Vector3(position.x, 0.0f, position.y) + transform.position;
            Instantiate(floorPrefab, gameObjectPosition, Quaternion.identity);
        }
    }

    private static Vector2 MoveVectorToDirection(Vector2 origin, Model.Direction direction)
    {
        Vector2 newPosition = new Vector2(origin.x, origin.y);

        switch (direction)
        {
            case Model.Direction.EAST:
                newPosition.x += 1.0f;
                break;
            case Model.Direction.WEST:
                newPosition.x -= 1.0f;
                break;
            case Model.Direction.NORTH:
                newPosition.y += 1.0f;
                break;
            case Model.Direction.SOUTH:
                newPosition.y -= 1.0f;
                break;
        }

        return newPosition;
    }
}
