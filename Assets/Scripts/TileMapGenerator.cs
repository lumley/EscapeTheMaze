using UnityEngine;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class TileMapGenerator : MonoBehaviour {

    public Vector2 startingPoint;
	public List<int> endingPointLengthFromStarting;
    public List<int> randomRamificationLength;
    private Model.Tile startingTile;
    private Dictionary<Vector2, Model.Tile> createdTileMap;
	public Model.Tile StartingTile{
		get
		{
			return startingTile;
		}
	}
    
    public System.Collections.Generic.IEnumerable<Model.Tile> Tiles{
        get {
            return this.createdTileMap.Values;
        }
    }
    
    public Dictionary<Vector2, Model.Tile> Map2D {
        get {
            return this.createdTileMap;
        }
    }

    public void GenerateMap()
    {
        this.createdTileMap = new Dictionary<Vector2, Model.Tile>();
        this.startingTile = new Model.Tile();
        this.startingTile.AddAttribute(new Model.TileAttribute.SpawningPoint());
        createdTileMap.Add(this.startingPoint, this.startingTile);
        
        // For each ending, generate a path to it
        Model.Direction[] directions = (Model.Direction[]) System.Enum.GetValues(typeof(Model.Direction));
        Vector2 scenePosition = new Vector2(transform.position.x, transform.position.z);
        foreach (int maxSteps in endingPointLengthFromStarting){
            GenerateRandomPath(RandomProvider.GetRandomElement(directions), startingPoint + scenePosition, maxSteps, this.startingTile, createdTileMap);
        }

        HashSet<Model.Tile> visitedTileSet = new HashSet<Model.Tile>();
        DrawAllTiles(this.startingTile, startingPoint, visitedTileSet);

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

    /**
     * It generates a random path that can collide with parts of the already generated path
     */
    private void GenerateRandomPath(Model.Direction directionFromLastGeneratedTile, Vector2 lastPosition, int stepsLeft, Model.Tile lastGeneratedTile, Dictionary<Vector2, Model.Tile> createdTileMap)
    {
        Vector2 currentPosition = MoveVectorToDirection(lastPosition, directionFromLastGeneratedTile);
        Model.Tile currentTile;
        if(!createdTileMap.TryGetValue(currentPosition, out currentTile))
        {
            currentTile = new Model.Tile();
            createdTileMap.Add(currentPosition, currentTile);
        }

        // Link my position with every position around me
        Model.Direction[] directions = Model.Utils.GetAllDirections();
        foreach (Model.Direction direction in directions)
        {
            Vector2 neighbourPosition = MoveVectorToDirection(currentPosition, direction);
            Model.Tile neighbour;
            if(createdTileMap.TryGetValue(neighbourPosition, out neighbour))
            {
                currentTile.SetNeighbour(neighbour, direction);
                Model.Direction reversedDirection = Model.Utils.Reverse(direction);
                neighbour.SetNeighbour(currentTile, reversedDirection);
            }
        }

        if (stepsLeft > 1)
        {
            Model.Direction reversedDirection = Model.Utils.Reverse(directionFromLastGeneratedTile);
            Model.Direction nextDirection = RandomProvider.GetRandomElementExcluding(directions, reversedDirection);
            GenerateRandomPath(nextDirection, currentPosition, stepsLeft - 1, currentTile, createdTileMap);
        } else {
            currentTile.AddAttribute(new Model.TileAttribute.EndingPoint());
        }
    }

    private static Vector2 MoveVectorToDirection(Vector2 origin, Model.Direction direction)
    {
        Vector2 newPosition = new Vector2(origin.x, origin.y);

        switch (direction)
        {
            case Model.Direction.EAST:
                newPosition.x += 1;
                break;
            case Model.Direction.WEST:
                newPosition.x -= 1;
                break;
            case Model.Direction.NORTH:
                newPosition.y += 1;
                break;
            case Model.Direction.SOUTH:
                newPosition.y -= 1;
                break;
        }

        return newPosition;
    }

}
