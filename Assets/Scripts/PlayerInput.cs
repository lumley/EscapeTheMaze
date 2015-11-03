using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    private const string horizontalMovementAxis = "Horizontal";
    private const string verticalMovementAxis = "Vertical";
    private const string horizontalOrientationAxis = "Mouse X";
    private const string attackButton = "Fire1";
    private const string cancelButton = "Cancel";
    private const string submitButton = "Submit";
    private const string rotate90DegreesLeft = "Rotate 90 Left";
	private const string rotate90DegreesRight = "Rotate 90 Right";

    public float mouseSensitivity=3.0f;

    private float lastHorizontalOrientation = 0.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw(horizontalMovementAxis) < 0)
		{
			gameObject.SendMessage("MoveLeft");
		}
		else if (Input.GetAxisRaw(horizontalMovementAxis) > 0)
		{
			gameObject.SendMessage("MoveRight");
		}

        if (Input.GetAxisRaw(verticalMovementAxis) < 0)
		{
			gameObject.SendMessage("MoveBackward");
		}
		else if (Input.GetAxisRaw(verticalMovementAxis) > 0)
		{
			gameObject.SendMessage("MoveForward");
		}

        if (Input.GetButtonDown(rotate90DegreesLeft))
        {
            gameObject.SendMessage("RotateLeft");
        }
        else if (Input.GetButtonDown(rotate90DegreesRight))
        {
            gameObject.SendMessage("RotateRight");
        }

        if (CursorLockMode.Locked == Cursor.lockState)
		{
            float currentHorizontalOrientation = Input.GetAxis(horizontalOrientationAxis) * mouseSensitivity;
            if (this.lastHorizontalOrientation != currentHorizontalOrientation)
            {
                gameObject.SendMessage("SetRotation", currentHorizontalOrientation);
                this.lastHorizontalOrientation = currentHorizontalOrientation;
            }
            
		}

		if (Input.GetButtonDown(attackButton)) {
			gameObject.SendMessage("Attack");
		}
		
		if (Input.GetButtonDown(cancelButton))
		{
			SetCursorState(CursorLockMode.None);
		}
		else if (Input.touchCount > 0 || Input.GetButtonDown(submitButton))
		{
			SetCursorState(CursorLockMode.Locked);
		}
	}
	void OnGUI()
	{
		if(CursorLockMode.Locked != Cursor.lockState)
		{
			GUI.Label(new Rect(Screen.width/2 - 75, Screen.height/2 - 15, 150, 30), "Game paused");
		}
	}
	private void SetCursorState(CursorLockMode lockState)
	{
		Cursor.lockState = lockState;
		// Hide cursor when locking
		Cursor.visible = (CursorLockMode.Locked != lockState);
	}

	void OnApplicationFocus(bool isFocused)
	{
		if(isFocused)
		{
			SetCursorState(CursorLockMode.Locked);
		}
	}
}
