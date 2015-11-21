using UnityEngine;
using UnityEngine.EventSystems;

namespace Input.Events
{
    class AttackEvent
    {
        public static void Execute(IAttackEventHandler handler, BaseEventData eventData)
        {
            handler.OnAttack(ExecuteEvents.ValidateEventData<AttackEventData>(eventData));
        }

        private static ExecuteEvents.EventFunction<IAttackEventHandler> AttackEventHandler
        {
            get { return Execute; }
        }

        public static void Attack(GameObject gameObject)
        {
            ExecuteEvents.Execute(gameObject, AttackEventData.Create(), AttackEventHandler);
        }
    }
}
