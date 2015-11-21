using Map.Model;
using UnityEngine.EventSystems;

namespace Input.Events
{
    public class RotationEventData : BaseEventData
    {
        public RelativeDirection relativeDirection;

        private RotationEventData(EventSystem eventSystem, RelativeDirection relativeDirection) : base(eventSystem)
        {
            this.relativeDirection = relativeDirection;
        }

        public static RotationEventData Create(RelativeDirection relativeDirection)
        {
            return new RotationEventData(EventSystem.current, relativeDirection);
        }
    }
}