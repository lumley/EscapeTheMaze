using UnityEngine.EventSystems;

namespace Combat.Events
{
    public interface ITakeDamageHandler : IEventSystemHandler
    {

        void OnTakeDamage(TakeDamageEventData damage);
    }
}
