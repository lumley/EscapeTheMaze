using UnityEngine;
using UnityEngine.EventSystems;

namespace Input.Events
{
    class FreeRotationEvent
    {
        public static void Execute(IFreeRotationEventHandler handler, BaseEventData eventData)
        {
            handler.OnFreeRotation(ExecuteEvents.ValidateEventData<FreeRotationEventData>(eventData));
        }

        private static ExecuteEvents.EventFunction<IFreeRotationEventHandler> FreeRotationEventHandler
        {
            get { return Execute; }
        }

        public static void FreeRotate(GameObject gameObject, float horizontalRotation)
        {
            ExecuteEvents.Execute(gameObject, FreeRotationEventData.Create(horizontalRotation), FreeRotationEventHandler);
        }
    }
}
