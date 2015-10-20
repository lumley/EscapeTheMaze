using UnityEngine;
using System.Collections.Generic;

//[ExecuteInEditMode] // Use this to also awake inside Unity Editor
public class TileMap : MonoBehaviour {

    private Dictionary<IntPair, Model.Tile> createdTileMap;

    [SerializeField]
    private Model.BoardCreator boardCreator = new Model.BoardCreator();
    
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawAllTiles(Model.Tile tile, Vector2 position, HashSet<Model.Tile> visitedTileSet)
    {
        if (!visitedTileSet.Contains(tile))
        {
            visitedTileSet.Add(tile);

            // Paint myself
            GameObject instantiatedGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Vector3 gameObjectPosition = new Vector3(position.x, 0.0f, position.y);
            instantiatedGameObject.transform.position = gameObjectPosition;

            // TODO: Remove the direction where we come from!
            foreach (Model.Direction neighbourDirection in Model.Utils.GetAllDirections())
            {
                Model.Tile neighbour = tile.GetNeighbour(neighbourDirection);
                if (neighbour != null)
                {
                    Vector2 neighbourPosition = MoveVectorToDirection(position, neighbourDirection);
                    DrawAllTiles(neighbour, neighbourPosition, visitedTileSet);
                }
            }
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
