using UnityEngine;
using System.Collections;
using Model;

public class PlayerController : MonoBehaviour {

	public GameObject tileMap;
	public float mouseSensitivity=3.0f;
	private Model.Tile currentTile;

	public enum RelativeDirection{
		FORWARD, RIGHT, BACKWARD, LEFT
	}

	// Use this for initialization
	void Start () {
		currentTile = ((TileMapGenerator)tileMap.GetComponent<TileMapGenerator> ()).StartingTile;
		Cursor.lockState= CursorLockMode.Locked;
		Cursor.visible=false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.A)|| Input.GetKeyUp (KeyCode.LeftArrow)) {
			MoveLeft();
		}
		if (Input.GetKeyUp (KeyCode.D)|| Input.GetKeyUp (KeyCode.RightArrow)){
			MoveRight();
		}
		if (Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp (KeyCode.DownArrow)) {
			MoveBackward();
		} 
		if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp (KeyCode.UpArrow)){
			MoveForward();
		}

		//Rotation
		float yRotation = Input.GetAxis ("Mouse X") * mouseSensitivity;
		float xRotation = Input.GetAxis ("Mouse Y") * mouseSensitivity;
		transform.Rotate (0, yRotation, 0);


	}



	private void MoveLeft(){
		Move(RelativeDirection.LEFT);
	}

	private void MoveRight(){
		Move(RelativeDirection.RIGHT);
	}
	
	private void MoveForward(){
		Move(RelativeDirection.FORWARD);
	}
	
	private void MoveBackward(){
		Move(RelativeDirection.BACKWARD);
	}


	private void Move(RelativeDirection direction){
		Tile destinationTile=GetTileInDirection(direction);
		MoveToTile(destinationTile);

		// TODO move and replace this by the coordinates that will be stored later on in the tiles
		if (destinationTile!= null){
			switch (GetCardinalDirectionAtRelativeDirection(direction)){
				case Tile.Direction.NORTH:
					transform.position+=Vector3.forward;
					break;
				case Tile.Direction.EAST:
					transform.position+=Vector3.right;
					break;
				case Tile.Direction.SOUTH:
					transform.position+=Vector3.back;
					break;
				case Tile.Direction.WEST:
					transform.position+=Vector3.left;
					break;
			}
		}

	}

	private void MoveToTile(Model.Tile tile){
		if (tile!= null){
			currentTile=tile;
			// TODO move to coordinates defined by tile
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
			default:							throw new InvalidDirectionException();
		}
	}

	private Tile.Direction GetLeftCardinalDirection(){
		switch(GetCardinalDirection()){
			case Tile.Direction.NORTH:	return Tile.Direction.WEST;
			case Tile.Direction.EAST:	return Tile.Direction.NORTH;
			case Tile.Direction.SOUTH:	return Tile.Direction.EAST;
			case Tile.Direction.WEST:	return Tile.Direction.SOUTH;
			default:					throw new InvalidDirectionException();
		}
	}

	private Tile.Direction GetRightCardinalDirection(){
		switch(GetCardinalDirection()){
			case Tile.Direction.NORTH:	return Tile.Direction.EAST;
			case Tile.Direction.EAST:	return Tile.Direction.SOUTH;
			case Tile.Direction.SOUTH:	return Tile.Direction.WEST;
			case Tile.Direction.WEST:	return Tile.Direction.NORTH;
			default:					throw new InvalidDirectionException();
		}
	}

	private Tile.Direction GetBackwardCardinalDirection(){
		switch(GetCardinalDirection()){
			case Tile.Direction.NORTH:	return Tile.Direction.SOUTH;
			case Tile.Direction.EAST:	return Tile.Direction.WEST;
			case Tile.Direction.SOUTH:	return Tile.Direction.NORTH;
			case Tile.Direction.WEST:	return Tile.Direction.EAST;
			default:					throw new InvalidDirectionException();
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
		return angle>=-45.0f && angle<=45.0f;
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
