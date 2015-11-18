using Model;
using UnityEngine.EventSystems;

public class RotationEventData : BaseEventData
{
    public RelativeDirection relativeDirection;

    public RotationEventData(EventSystem eventSystem, RelativeDirection relativeDirection) : base(eventSystem)
    {
        this.relativeDirection = relativeDirection;
    }

    public static RotationEventData Create(RelativeDirection relativeDirection)
    {
        return new RotationEventData(EventSystem.current, relativeDirection);
    }
}
