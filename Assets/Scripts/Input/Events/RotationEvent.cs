using Map.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Input.Events
{
    public class RotationEvent
    {
        private static void Execute(IRotationEventHandler handler, BaseEventData eventData)
        {
            handler.OnRotate(ExecuteEvents.ValidateEventData<RotationEventData>(eventData));
        }

        private static ExecuteEvents.EventFunction<IRotationEventHandler> MovementEventHandler
        {
            get { return Execute; }
        }

        public static void Rotate(GameObject gameObject, RelativeDirection direction)
        {
            ExecuteEvents.Execute(gameObject, RotationEventData.Create(direction), MovementEventHandler);
        }
    }
}