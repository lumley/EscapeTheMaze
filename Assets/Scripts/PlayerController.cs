using UnityEngine;
using System.Collections;
using Model;

public class PlayerController : MonoBehaviour {

	public GameObject tileMap;
	public float mouseSensitivity=3.0f;

	private Model.Tile currentTile;

	private bool isMoving=false;
	// determinates how quick the player should move.
	// The unit expected here is tiles per second
	private float speed=5.0f;

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
		if (isMoving == false){
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
		}

		//Rotation
		float yRotation = Input.GetAxis ("Mouse X") * mouseSensitivity;
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

		if (destinationTile!= null){
			StartCoroutine(AnimateMovementIntoDirection(direction));
		}

	}

	private IEnumerator AnimateMovementIntoDirection(RelativeDirection direction){

		Vector3 start = transform.position;
		Vector3 finish = Vector3.zero;

		float t=0.0f;
		float totalDelta=0.0f;

		switch (GetCardinalDirectionAtRelativeDirection(direction)){
			case Tile.Direction.NORTH:
				finish+=start+Vector3.forward;
				break;
			case Tile.Direction.EAST:
				finish+=start+Vector3.right;
				break;
			case Tile.Direction.SOUTH:
				finish+=start+Vector3.back;
				break;
			case Tile.Direction.WEST:
				finish+=start+Vector3.left;
				break;
		}

		while (t < 1f) {
			// calculate the state of the transition from start to finish aka the interpolant
			t += Time.deltaTime * speed;

			// the interpolated vector between start and finish is the
			transform.position = Vector3.Lerp(start, finish, t);
			yield return null;
		}
		
		isMoving = false;

		//NOTES:
		// we could abort the iteration anytime with
		// yield break;
		// We could pass the result of each iteration to the consumer of the enumerator by calling 
		// yield return 0;
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
