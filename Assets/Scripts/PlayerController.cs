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

    CursorLockMode wantedMode = CursorLockMode.Locked;

    public enum RelativeDirection{
		FORWARD, RIGHT, BACKWARD, LEFT
	}

	void Awake(){
		Application.targetFrameRate=60;
	}

	// Use this for initialization
	void Start () {
		currentTile = ((TileMap)tileMap.GetComponent<TileMap> ()).StartingTile;
        SetCursorState();
	}


    // Update is called once per frame
    void Update()
    {
        if (IsMoving())
        {
            interpolant += Time.smoothDeltaTime * speed;
            // the interpolated vector between start and finish is the
            transform.position = Vector3.Lerp(start, finish, interpolant);
            //controller.SimpleMove(Vector3.Lerp(start, finish, interpolant));
        }

        if (IsMoving() == false)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                MoveLeft();
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                MoveRight();
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                MoveBackward();
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                MoveForward();
            }
            else
            {
                interpolant = 1.0f;
            }
        }

        if (CursorLockMode.Locked == Cursor.lockState)
        {
            //Rotation
            float yRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, yRotation, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            wantedMode = CursorLockMode.None;
            SetCursorState();
        }
        else if (Input.touchCount > 0)
        {
            wantedMode = CursorLockMode.Locked;
            SetCursorState();
        }
    }

    void OnGUI()
    {
        if(CursorLockMode.Locked != Cursor.lockState)
        {
            GUI.Label(new Rect(Screen.width/2 - 75, Screen.height/2 - 15, 150, 30), "Game paused");
        }
    }

    void OnApplicationFocus(bool isFocused)
    {
        if(isFocused)
        {
            wantedMode = CursorLockMode.Locked;
            SetCursorState();
        }
    }

    private void SetCursorState()
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
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
			interpolant=0.0f;
			start=transform.position;
			finish=transform.position;
			finish+=GetDirectionVector(GetCardinalDirectionAtRelativeDirection(direction));
		}
	}

	private Vector3 GetDirectionVector(Direction direction){
		switch (direction)
        {
			case Direction.NORTH:	return Vector3.forward;
			case Direction.EAST:	return Vector3.right;
			case Direction.SOUTH:	return Vector3.back;
			case Direction.WEST:	return Vector3.left;
			default: throw new InvalidDirectionException();
		}
	}

	private void MoveToTile(Tile tile){
		if (tile!= null)
        {
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

	private Direction GetCardinalDirectionAtRelativeDirection(RelativeDirection direction){
		switch (direction){
			case RelativeDirection.LEFT:		return GetLeftCardinalDirection();
			case RelativeDirection.RIGHT:		return GetRightCardinalDirection();
			case RelativeDirection.FORWARD:		return GetCardinalDirection();
			case RelativeDirection.BACKWARD:	return GetBackwardCardinalDirection();
			default:							throw new InvalidDirectionException();
		}
	}

	private Direction GetLeftCardinalDirection(){
		switch(GetCardinalDirection()){
			case Direction.NORTH:	return Direction.WEST;
			case Direction.EAST:	return Direction.NORTH;
			case Direction.SOUTH:	return Direction.EAST;
			case Direction.WEST:	return Direction.SOUTH;
			default:					throw new InvalidDirectionException();
		}
	}

	private Direction GetRightCardinalDirection(){
		switch(GetCardinalDirection()){
			case Direction.NORTH:	return Direction.EAST;
			case Direction.EAST:	return Direction.SOUTH;
			case Direction.SOUTH:	return Direction.WEST;
			case Direction.WEST:	return Direction.NORTH;
			default:					throw new InvalidDirectionException();
		}
	}

	private Direction GetBackwardCardinalDirection(){
		switch(GetCardinalDirection()){
			case Direction.NORTH:	return Direction.SOUTH;
			case Direction.EAST:	return Direction.WEST;
			case Direction.SOUTH:	return Direction.NORTH;
			case Direction.WEST:	return Direction.EAST;
			default:					throw new InvalidDirectionException();
		}
	}
		
	private Direction GetCardinalDirection(){
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.forward)){
			return Direction.NORTH;
		}
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.back)){
			return Direction.SOUTH;
		}
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.right)){
			return Direction.EAST;
		}
		if (IsPlayerFacingIntoTheDirectionOf(Vector3.left)){
			return Direction.WEST;
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
