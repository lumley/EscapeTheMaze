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
			if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)) {
				MoveLeft();
			}
			if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow)){
				MoveRight();
			}
			if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow)) {
				MoveBackward();
			} 
			if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow)){
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


		if (destinationTile!= null && isMoving==false){
			//Debug.Log("Moving "+direction);
			MoveToTile(destinationTile);
			StartCoroutine(AnimateMovementIntoDirection(direction));
		}

	}

	private IEnumerator AnimateMovementIntoDirection(RelativeDirection direction){

		isMoving=true;

		Vector3 start = transform.position;
		Vector3 finish = transform.position;

		float t=0.0f;

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

		while (t < 1f) {
			// calculate the state of the transition from start to finish aka the interpolant
			t += Time.deltaTime * speed;

			// the interpolated vector between start and finish is the
			transform.position = Vector3.Lerp(start, finish, t);
			yield return null;
		}

		transform.position=finish;
		isMoving = false;
		/*Debug.Log("finished to move");
		Debug.Log("original position: "+ start);
		Debug.Log("targeted position: "+ finish);
		Debug.Log("actual position: "+ transform.position);*/
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
