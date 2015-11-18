using Model;
using UnityEngine.EventSystems;

public interface IRotationEventHandler : IEventSystemHandler
{
    void OnRotate(RotationEventData rotationEvent);
}

