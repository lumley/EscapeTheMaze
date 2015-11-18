using Map.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Input.Events
{
    public class MovementEvent
    {
        private static void Execute(IMovementEventHandler handler, BaseEventData eventData)
        {
            handler.OnMove(ExecuteEvents.ValidateEventData<MovementEventData>(eventData));
        }

        public static ExecuteEvents.EventFunction<IMovementEventHandler> MovementEventHandler
        {
            get { return Execute; }
        }

        public static void Move(GameObject gameObject, RelativeDirection direction)
        {
            ExecuteEvents.Execute(gameObject, MovementEventData.Create(direction), MovementEventHandler);
        }
    }
}

