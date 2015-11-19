using Combat.Events;
using Input.Events;
using Map.Model;
using UnityEngine;

namespace Input
{
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

        private float lastHorizontalOrientation;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
            if (UnityEngine.Input.GetAxisRaw(HorizontalMovementAxis) < 0)
            {
                MovementEvent.Move(gameObject, RelativeDirection.LEFT);
            }
            else if (UnityEngine.Input.GetAxisRaw(HorizontalMovementAxis) > 0)
            {
                MovementEvent.Move(gameObject, RelativeDirection.RIGHT);
            }

            if (UnityEngine.Input.GetAxisRaw(VerticalMovementAxis) < 0)
            {
                MovementEvent.Move(gameObject, RelativeDirection.BACKWARDS);
            }
            else if (UnityEngine.Input.GetAxisRaw(VerticalMovementAxis) > 0)
            {
                MovementEvent.Move(gameObject, RelativeDirection.FORWARDS);
            }

            if (UnityEngine.Input.GetButtonDown(Rotate90DegreesLeft))
            {
                RotationEvent.Rotate(gameObject, RelativeDirection.LEFT);
            }
            else if (UnityEngine.Input.GetButtonDown(Rotate90DegreesRight))
            {
                RotationEvent.Rotate(gameObject, RelativeDirection.RIGHT);
            }

            if (CursorLockMode.Locked == Cursor.lockState)
            {
                float currentHorizontalOrientation = UnityEngine.Input.GetAxis(HorizontalOrientationAxis) * mouseSensitivity;
                if (!lastHorizontalOrientation.Equals(currentHorizontalOrientation))
                {
                    FreeRotationEvent.FreeRotate(gameObject, currentHorizontalOrientation);
                    this.lastHorizontalOrientation = currentHorizontalOrientation;
                }
            
            }

            if (UnityEngine.Input.GetButtonDown(AttackButton)) {
                gameObject.SendMessage("Attack");
            }
		
            if (UnityEngine.Input.GetButtonDown("Fire2")){
                takeDamageEvent.Invoke(TakeDamageEventData.Create(2));
            }
		
            if (UnityEngine.Input.GetButtonDown(CancelButton))
            {
                SetCursorState(CursorLockMode.None);
            }
            else if (UnityEngine.Input.touchCount > 0 || UnityEngine.Input.GetButtonDown(SubmitButton))
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
}
