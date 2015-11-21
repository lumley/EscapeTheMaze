using UnityEngine.EventSystems;

namespace Input.Events
{
    public class AttackEventData : BaseEventData
    {
        private AttackEventData(EventSystem eventSystem) : base(eventSystem)
        {
        }

        public static AttackEventData Create()
        {
            return new AttackEventData(EventSystem.current);
        }
    }
}
