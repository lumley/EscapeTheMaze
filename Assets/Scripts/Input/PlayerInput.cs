using UnityEngine;
using System.Collections;
using Model;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour {
	
	public TakeDamageEvent takeDamageEvent;

    private const string HorizontalMovementAxis = "Horizontal";
    private const string VerticalMovementAxis = "Vertical";
    private const string HorizontalOrientationAxis = "Mouse X";
    private const string AttackButton = "Fire1";
    private const string CancelButton = "Cancel";
    private const string SubmitButton = "Submit";
    private const string Rotate90DegreesLeft = "Rotate 90 Left";
	private const string Rotate90DegreesRight = "Rotate 90 Right";

    public float mouseSensitivity=3.0f;

    private float lastHorizontalOrientation = 0.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw(HorizontalMovementAxis) < 0)
		{
            MovementEvent.Move(gameObject, RelativeDirection.LEFT);
        }
		else if (Input.GetAxisRaw(HorizontalMovementAxis) > 0)
		{
            MovementEvent.Move(gameObject, RelativeDirection.RIGHT);
        }

        if (Input.GetAxisRaw(VerticalMovementAxis) < 0)
		{
            MovementEvent.Move(gameObject, RelativeDirection.BACKWARDS);
        }
		else if (Input.GetAxisRaw(VerticalMovementAxis) > 0)
		{
            MovementEvent.Move(gameObject, RelativeDirection.FORWARDS);
        }

        if (Input.GetButtonDown(Rotate90DegreesLeft))
        {
            RotationEvent.Rotate(gameObject, RelativeDirection.LEFT);
        }
        else if (Input.GetButtonDown(Rotate90DegreesRight))
        {
            RotationEvent.Rotate(gameObject, RelativeDirection.RIGHT);
        }

        if (CursorLockMode.Locked == Cursor.lockState)
		{
            float currentHorizontalOrientation = Input.GetAxis(HorizontalOrientationAxis) * mouseSensitivity;
            if (lastHorizontalOrientation != currentHorizontalOrientation)
            {
                gameObject.SendMessage("SetRotation", currentHorizontalOrientation);
                this.lastHorizontalOrientation = currentHorizontalOrientation;
            }
            
		}

		if (Input.GetButtonDown(AttackButton)) {
			gameObject.SendMessage("Attack");
		}
		
		if (Input.GetButtonDown("Fire2")){
			takeDamageEvent.Invoke(TakeDamageEventData.Create(2));
		}
		
		if (Input.GetButtonDown(CancelButton))
		{
			SetCursorState(CursorLockMode.None);
		}
		else if (Input.touchCount > 0 || Input.GetButtonDown(SubmitButton))
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

	private static void SetCursorState(CursorLockMode lockState)
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
