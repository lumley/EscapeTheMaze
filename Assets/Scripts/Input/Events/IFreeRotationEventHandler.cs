using UnityEngine.EventSystems;

namespace Input.Events
{
    interface IFreeRotationEventHandler : IEventSystemHandler
    {
        void OnFreeRotation(FreeRotationEventData rotationEvent);
    }
}
