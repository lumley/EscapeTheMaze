using Model;
using UnityEngine.EventSystems;

public class MovementEventData : BaseEventData
{
    public RelativeDirection relativeDirection;

    public MovementEventData(EventSystem eventSystem, RelativeDirection relativeDirection) : base(eventSystem)
    {
        this.relativeDirection = relativeDirection;
    }

    public static MovementEventData Create(RelativeDirection relativeDirection)
    {
        return new MovementEventData(EventSystem.current, relativeDirection);
    }
}
