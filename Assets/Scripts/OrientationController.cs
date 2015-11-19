using Input.Events;
using Map;
using Map.Model;
using UnityEngine;

public class OrientationController : MonoBehaviour, IRotationEventHandler, IFreeRotationEventHandler
{
    public float maxDegreesPerSecond = 360.0f;
    private float rotationInterpolation = 1.0f;
    private float actualDegreesPerSecond = 1.0f;
    private Quaternion rotationFrom;
    private Quaternion rotationTo;

    private void SetRotation(float rotation)
    {
        rotationInterpolation = 1.0f; // Stop any interpolation!
        transform.Rotate(0, rotation, 0);
        GameObject.Find("Compass").SendMessage("SetRotation", rotation);
    }

    private void RotateLeft()
    {
        if (!IsRotating())
        {
            Direction facingDirection = FacingHelper.GetFacingDirection(transform);
            Vector3 rotationVector = FacingHelper.GetRotationVector(Map.Model.Utils.TurnLeft(facingDirection));
            InitializeRotationAnimation(rotationVector);
        }
    }

    private void RotateRight()
    {
        if (!IsRotating())
        {
            Direction facingDirection = FacingHelper.GetFacingDirection(transform);
            Vector3 rotationVector = FacingHelper.GetRotationVector(Map.Model.Utils.TurnRight(facingDirection));
            InitializeRotationAnimation(rotationVector);
        }
    }

    private void InitializeRotationAnimation(Vector3 rotationVector)
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = rotationVector;
        this.rotationTo = rotation;
        this.rotationFrom = transform.rotation;

        float angle = Mathf.Abs(Quaternion.Angle(this.rotationFrom, this.rotationTo));
        this.actualDegreesPerSecond = this.maxDegreesPerSecond / angle;

        this.rotationInterpolation = 0.0f;
    }

    void Update()
    {
        if (IsRotating())
        {
            this.rotationInterpolation += Time.smoothDeltaTime * this.actualDegreesPerSecond;
            transform.rotation = Quaternion.Lerp(this.rotationFrom, this.rotationTo, this.rotationInterpolation);
        }
    }

    private bool IsRotating()
    {
        return this.rotationInterpolation < 1.0f;
    }

    public void OnRotate(RotationEventData rotationEvent)
    {
        switch (rotationEvent.relativeDirection)
        {
            case RelativeDirection.LEFT:
                RotateLeft();
                break;
            case RelativeDirection.RIGHT:
                RotateRight();
                break;
        }
    }

    public void OnFreeRotation(FreeRotationEventData rotationEvent)
    {
        SetRotation(rotationEvent.horizontalRotation);
    }
}
