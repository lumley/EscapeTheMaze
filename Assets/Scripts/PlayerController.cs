using UnityEngine;
using System.Collections;
using Model;

public class PlayerController : MonoBehaviour {

	public GameObject tileMap;
	public float mouseSensitivity=3.0f;

	private Model.Tile currentTile;

	// determinates how quick the player should move.
	// The unit expected here is tiles per second
	private float speed=2.0f;

	private float interpolant=1.0f;

	private Vector3 start=Vector3.zero;
	private Vector3 finish=Vector3.zero;

	public enum RelativeDirection{
		FORWARD, RIGHT, BACKWARD, LEFT
	}

	void Awake(){
		Application.targetFrameRate=60;
	}

	// Use this for initialization
	void Start () {
		currentTile = ((TileMapGenerator)tileMap.GetComponent<TileMapGenerator> ()).StartingTile;
		Cursor.lockState= CursorLockMode.Locked;
		Cursor.visible=false;
	}


	// Update is called once per frame
	void Update () {
		if (IsMoving()){
			interpolant += Time.smoothDeltaTime * speed;
			// the interpolated vector between start and finish is the
			transform.position = Vector3.Lerp(start, finish, interpolant);
		}

		if (IsMoving()==false){
			if (Input.GetAxisRaw("Horizontal")<0) {
				MoveLeft();
			} else if (Input.GetAxisRaw("Horizontal")>0){
				MoveRight();
			} else if (Input.GetAxisRaw("Vertical")<0) {
				MoveBackward();
			} else if (Input.GetAxisRaw("Vertical")>0){
				MoveForward();
			} else {
				interpolant=1.0f;
			}
		}

		//Rotation
		float yRotation = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, yRotation, 0);

	}

	private bool IsMoving(){
		return interpolant<1.0f;
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
		if (destinationTile!= null ){
			MoveToTile(destinationTile);
			// to avoid stuttering it is needed to start with the interpolant overflow of the previous movement
			if (interpolant>1.0f)
				interpolant-=1.0f;
			else {
				interpolant=0.0f;
			}

			start=transform.position;
			finish=transform.position;
			switch (GetCardinalDirectionAtRelativeDirection(direction)){
				case Tile.Direction.NORTH:
					finish+=Vector3.forward;
					break;
				case Tile.Direction.EAST:
					finish+=Vector3.right;
					break;
				case Tile.Direction.SOUTH:
					finish+=Vector3.back;
					break;
				case Tile.Direction.WEST:
					finish+=Vector3.left;
					break;
			}
		}
	}

	private void MoveToTile(Model.Tile tile){
		if (tile!= null){
			currentTile=tile;
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
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.forward)){
			return Tile.Direction.NORTH;
		}
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.back)){
			return Tile.Direction.SOUTH;
		}
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.right)){
			return Tile.Direction.EAST;
		}
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.left)){
			return Tile.Direction.WEST;
		}
		throw new InvalidDirectionException();
	}

	private bool IsPlayerFacingIntoTheDirectionOf(Vector3 direction){
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
