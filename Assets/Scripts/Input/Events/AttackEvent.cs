using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Input.Events
{
    public class AttackEvent
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

        /// <summary>
        /// Event system can only go UP the hierarchy with ExecuteHierarchy, to go down, one needs to do that manually
        /// </summary>
        /// <param name="gameObject">Root game object from where to start</param>
        public static void SendAttackToAnyChildren(GameObject gameObject)
        {
            var attackEventData = AttackEventData.Create();
            (from Transform transform in gameObject.transform
                select ExecuteEvents.Execute(transform.gameObject, attackEventData, AttackEventHandler)).Any(
                    hasExecuted => hasExecuted);
        }
    }
}
