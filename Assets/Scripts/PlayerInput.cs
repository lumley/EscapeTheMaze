using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public string horizontalAxis="Horizontal";
	public string verticalAxis="Vertical";
	public string attackButton="Fire1";


	public float mouseSensitivity=3.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw(horizontalAxis) < 0)
		{
			gameObject.SendMessage("MoveLeft");
		}
		else if (Input.GetAxisRaw(horizontalAxis) > 0)
		{
			gameObject.SendMessage("MoveRight");
		}
		else if (Input.GetAxisRaw(verticalAxis) < 0)
		{
			gameObject.SendMessage("MoveBackward");
		}
		else if (Input.GetAxisRaw(verticalAxis) > 0)
		{
			gameObject.SendMessage("MoveForward");
		}
		if (CursorLockMode.Locked == Cursor.lockState)
		{
			gameObject.SendMessage("SetRotation", Input.GetAxis("Mouse X")*mouseSensitivity);
		}

		if (Input.GetButtonDown(attackButton)) {
			gameObject.SendMessage("Attack");
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SetCursorState(CursorLockMode.None);
		}
		else if (Input.touchCount > 0)
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
