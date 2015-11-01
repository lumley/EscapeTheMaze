using UnityEngine;
using System.Collections;
using Model;

public class OrientationController : MonoBehaviour {

    public float degreesPerSecond = 1.0f;
    private float rotationInterpolation = 1.0f;
    private float  rotationTo;

	public void SetRotation(float rotation){
        rotationInterpolation = 1.0f; // Stop any interpolation!
        transform.Rotate (0, rotation, 0);
		GameObject.Find ("Compass").SendMessage ("SetRotation", rotation);
	}

    public void RotateLeft()
    {
        if (!IsRotating())
        {
            this.rotationTo = -90.0f;
            this.rotationInterpolation = 0.0f;
        }
    }

    public void RotateRight()
    {
        if (!IsRotating())
        {
            this.rotationTo = 90.0f;
            this.rotationInterpolation = 0.0f;
        }
    }
    
    void Update()
    {
        if (IsRotating())
        {
            this.rotationInterpolation += Time.smoothDeltaTime * this.degreesPerSecond;
            transform.Rotate(0, this.rotationTo * this.degreesPerSecond * Time.smoothDeltaTime, 0, Space.Self);
        }
    }

    private bool IsRotating()
    {
        return this.rotationInterpolation < 1.0f;
    }

}
