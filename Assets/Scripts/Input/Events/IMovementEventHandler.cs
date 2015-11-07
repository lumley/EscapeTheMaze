using Model;
using UnityEngine.EventSystems;

public interface IMovementEventHandler : IEventSystemHandler
{
    void OnMove(MovementEventData movementEvent);
}

