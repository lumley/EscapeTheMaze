using UnityEngine.EventSystems;

namespace Input.Events
{
    public interface IAttackEventHandler : IEventSystemHandler
    {
        void OnAttack(AttackEventData attackEvent);
    }
}
