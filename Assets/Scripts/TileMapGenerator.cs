﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class TileMapGenerator : MonoBehaviour {

	private Model.Tile startingTile;

    public Vector2 startingPoint;
	public List<int> endingPointLenghtFromStarting;

    public int width;
    public int height;

    public RandomProvider seedProvider;

	public Model.Tile StartingTile{
		get
		{
			return startingTile;
		}
	}

    public void GenerateMap()
    {
        Random.seed = seedProvider.seed;
        Dictionary<int, Model.Tile> createdTileMap = new Dictionary<int, Model.Tile>();
        this.startingTile = new Model.Tile();
        createdTileMap.Add(Vector2Int(startingPoint, this.width), this.startingTile);

        // For each ending, generate a path to it
        Model.Tile.Direction[] directions = (Model.Tile.Direction[]) System.Enum.GetValues(typeof(Model.Tile.Direction));
        foreach (int maxSteps in endingPointLenghtFromStarting){
            GenerateRandomPath(seedProvider.GetRandomElement(directions), new Vector2(startingPoint.x, startingPoint.y), maxSteps, this.startingTile, createdTileMap);
        }

        HashSet<Model.Tile> visitedTileSet = new HashSet<Model.Tile>();
        DrawAllTiles(this.startingTile, startingPoint, visitedTileSet);

    }

    // Use this for initialization
    void Awake()
    {
        this.GenerateMap();
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
            Vector3 gameObjectPosition = new Vector3(position.x, 0.0f, position.y) + transform.position;
            instantiatedGameObject.transform.position = gameObjectPosition;

            // TODO: Remove the direction where we come from!
            foreach (Model.Tile.Direction neighbourDirection in System.Enum.GetValues(typeof(Model.Tile.Direction)))
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

    private void GenerateRandomPath(Model.Tile.Direction directionFromLastGeneratedTile, Vector2 lastPosition, int stepsLeft, Model.Tile lastGeneratedTile, Dictionary<int, Model.Tile> createdTileMap)
    {
        Model.Tile.Direction reversedDirection = (Model.Tile.Direction)(((int)directionFromLastGeneratedTile + 2) % 4);
        Model.Tile generatedTile = new Model.Tile();
        lastGeneratedTile.SetNeighbour(generatedTile, directionFromLastGeneratedTile);
        generatedTile.SetNeighbour(lastGeneratedTile, reversedDirection);

        Vector2 newPosition = MoveVectorToDirection(lastPosition, directionFromLastGeneratedTile);

        createdTileMap.Add(Vector2Int(newPosition, width), generatedTile);

        if (stepsLeft > 1)
        {
            Model.Tile.Direction[] directions = (Model.Tile.Direction[])System.Enum.GetValues(typeof(Model.Tile.Direction));
            Model.Tile.Direction nextDirection = seedProvider.GetRandomElementExcluding(directions, reversedDirection);
            GenerateRandomPath(nextDirection, newPosition, stepsLeft - 1, generatedTile, createdTileMap);
        }
    }

    private static Vector2 MoveVectorToDirection(Vector2 origin, Model.Tile.Direction direction)
    {
        Vector2 newPosition = new Vector2(origin.x, origin.y);

        switch (direction)
        {
            case Model.Tile.Direction.EAST:
                newPosition.x += 1;
                break;
            case Model.Tile.Direction.WEST:
                newPosition.x -= 1;
                break;
            case Model.Tile.Direction.NORTH:
                newPosition.y += 1;
                break;
            case Model.Tile.Direction.SOUTH:
                newPosition.y -= 1;
                break;
        }
        //int intDirection = (int)direction;
        //newPosition.x = origin.x + ((intDirection) & 0x1 * (intDirection - 2));
        //newPosition.y = origin.y + ((intDirection + 1) & 0x1 * (intDirection - 1));

        return newPosition;
    }

    private static int Vector2Int(Vector2 vector, int width)
    {
        return (int)vector.y * width + (int)vector.x;
    }

}
