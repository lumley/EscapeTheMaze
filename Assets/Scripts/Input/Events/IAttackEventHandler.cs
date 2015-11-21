using UnityEngine.EventSystems;

namespace Input.Events
{
    interface IAttackEventHandler : IEventSystemHandler
    {
        void OnAttack(AttackEventData attackEvent);
    }
}
