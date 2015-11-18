using UnityEngine.EventSystems;

namespace Combat.Events
{
    public class TakeDamageEventData : BaseEventData
    {

        public int damage;

        private TakeDamageEventData(EventSystem eventSystem, int damage) : base(eventSystem)
        {
            this.damage = damage;
        }

        public static TakeDamageEventData Create(int damage)
        {
            return new TakeDamageEventData(EventSystem.current, damage);
        }
    }
}
