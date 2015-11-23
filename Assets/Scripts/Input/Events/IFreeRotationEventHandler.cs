using UnityEngine.EventSystems;

namespace Input.Events
{
    public interface IFreeRotationEventHandler : IEventSystemHandler
    {
        void OnFreeRotation(FreeRotationEventData rotationEvent);
    }
}
