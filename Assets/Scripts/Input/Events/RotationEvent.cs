using Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationEvent
{
    private static void Execute(IRotationEventHandler handler, BaseEventData eventData)
    {
        handler.OnRotate(ExecuteEvents.ValidateEventData<RotationEventData>(eventData));
    }

    public static ExecuteEvents.EventFunction<IRotationEventHandler> MovementEventHandler
    {
        get { return Execute; }
    }

    public static void Rotate(GameObject gameObject, RelativeDirection direction)
    {
        ExecuteEvents.Execute(gameObject, RotationEventData.Create(direction), MovementEventHandler);
    }
}