using UnityEngine.EventSystems;

namespace Input.Events
{
    public interface IRotationEventHandler : IEventSystemHandler
    {
        void OnRotate(RotationEventData rotationEvent);
    }
}

