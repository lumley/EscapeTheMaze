using UnityEngine.EventSystems;

namespace Input.Events
{
    public interface IMovementEventHandler : IEventSystemHandler
    {
        void OnMove(MovementEventData movementEvent);
    }
}

