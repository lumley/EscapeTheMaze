using UnityEngine.EventSystems;

namespace Input.Events
{
    public class FreeRotationEventData : BaseEventData
    {
        public readonly float horizontalRotation;

        private FreeRotationEventData(EventSystem eventSystem, float horizontalRotation) : base(eventSystem)
        {
            this.horizontalRotation = horizontalRotation;
        }

        public static FreeRotationEventData Create(float horizontalRotation)
        {
            return new FreeRotationEventData(EventSystem.current, horizontalRotation);
        }
    }
}