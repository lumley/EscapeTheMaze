using UnityEngine;
using System.Collections;
using Model;

public class PlayerController : MonoBehaviour {

	public GameObject tileMap;
	private Model.Tile currentTile;

	public enum RelativeDirection{
		FORWARD, RIGHT, BACKWARD, LEFT
	}

	// Use this for initialization
	void Start () {
		currentTile = ((TileMapGenerator)tileMap.GetComponent<TileMapGenerator> ()).StartingTile;
	}
	
	// Update is called once per frame
	void Update () {
		Model.Tile destinationTile;
		if (Input.GetAxisRaw ("Horizontal")<0) {
			MoveLeft();
		} else if (Input.GetAxisRaw ("Horizontal")>0){
			MoveRight();
		}

		if (Input.GetAxisRaw ("Vertical")<0) {
			MoveBackwards();
		} else if (Input.GetAxisRaw ("Vertical")>0){
			MoveForward();
		}
	}



	private void MoveLeft(){
		MoveToTile(GetLeftTile());
	}

	private void MoveRight(){
		MoveToTile(GetRightTile());
	}
	
	private void MoveForward(){
		MoveToTile(GetFrontTile());
	}
	
	private void MoveBackwards(){
		MoveToTile(GetBackTile());
	}

	private void MoveToTile(Model.Tile tile){
		if (tile!= null){
			// move!
		}
	}

	private Tile GetLeftTile() {
		return GetTileInDirection(RelativeDirection.LEFT);
	}
	
	private Tile GetRightTile() {
		return GetTileInDirection(RelativeDirection.RIGHT);
	}
	
	private Tile GetFrontTile() {
		return GetTileInDirection(RelativeDirection.FORWARD);
	}
	
	private Tile GetBackTile() {
		return GetTileInDirection(RelativeDirection.BACKWARD);
	}


	private Model.Tile GetTileInDirection(RelativeDirection direction){
		return currentTile.GetNeighbour(GetCardinalDirectionAtRelativeDirection(direction));
	}

	private Tile.Direction GetCardinalDirectionAtRelativeDirection(RelativeDirection direction){
		switch (direction){
			case RelativeDirection.LEFT:		return GetLeftCardinalDirection();
			case RelativeDirection.RIGHT:		return GetRightCardinalDirection();
			case RelativeDirection.FORWARD:		return GetCardinalDirection();
			case RelativeDirection.BACKWARD:	return GetBackwardCardinalDirection();
		}
	}

	private Tile.Direction GetLeftCardinalDirection(){
		switch(GetCardinalDirection()){
			case Tile.Direction.NORTH:	return Tile.Direction.WEST;
			case Tile.Direction.EAST:	return Tile.Direction.NORTH;
			case Tile.Direction.SOUTH:	return Tile.Direction.EAST;
			case Tile.Direction.WEST:	return Tile.Direction.SOUTH;
		}
	}

	private Tile.Direction GetRightCardinalDirection(){
		switch(GetCardinalDirection()){
			case Tile.Direction.NORTH:	return Tile.Direction.EAST;
			case Tile.Direction.EAST:	return Tile.Direction.SOUTH;
			case Tile.Direction.SOUTH:	return Tile.Direction.WEST;
			case Tile.Direction.WEST:	return Tile.Direction.NORTH;
		}
	}

	private Tile.Direction GetBackwardCardinalDirection(){
		switch(GetCardinalDirection()){
			case Tile.Direction.NORTH:	return Tile.Direction.SOUTH;
			case Tile.Direction.EAST:	return Tile.Direction.WEST;
			case Tile.Direction.SOUTH:	return Tile.Direction.NORTH;
			case Tile.Direction.WEST:	return Tile.Direction.EAST;
		}
	}
		
	private Tile.Direction GetCardinalDirection(){
		if (isPlayerFacingIntoTheDirectionOf(Vector3.forward)){
			return Tile.Direction.NORTH;
		}
		if (isPlayerFacingIntoTheDirectionOf(Vector3.back)){
			return Tile.Direction.SOUTH;
		}
		if (isPlayerFacingIntoTheDirectionOf(Vector3.right)){
			return Tile.Direction.EAST;
		}
		if (isPlayerFacingIntoTheDirectionOf(Vector3.left)){
			return Tile.Direction.WEST;
		}
		throw new InvalidDirectionException();
	}

	private bool isPlayerFacingIntoTheDirectionOf(Vector3 direction){
		float angle =Vector3.Angle(transform.forward, direction);
		return angle>=22.5f && angle<=22.5f;
	}

	public class InvalidDirectionException : System.Exception
	{
		public InvalidDirectionException() : base() { }
		public InvalidDirectionException(string message) : base(message) { }
		public InvalidDirectionException(string message, System.Exception inner) : base(message, inner) { }
		
		
		protected InvalidDirectionException(System.Runtime.Serialization.SerializationInfo info,
		                                    System.Runtime.Serialization.StreamingContext context) { }
	}

}
