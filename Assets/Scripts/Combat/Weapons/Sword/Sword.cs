using Combat.Events;
using Combat.Model;
using Input.Events;
using UnityEngine;

namespace Combat.Weapons.Sword
{
    public class Sword : MonoBehaviour, IWeapon, IAttackEventHandler
    {
        private Animator animator;

        public int damage = 10;
        private bool enemyHit;

        private bool isHitting;


        public void OnAttack(AttackEventData attackEvent)
        {
            Attack();
        }

        public void Attack()
        {
            animator.Play("Attack");
            Debug.Log("Swing!");
        }

        public void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (isHitting)
            {
                RaycastHit hit;

                Debug.Log("Forward " + Camera.main.transform.forward);
                Debug.Log("Transform position " + transform.parent.position);
                if (enemyHit == false &&
                    Physics.Raycast(transform.parent.position, Camera.main.transform.forward, out hit, 2.0f))
                {
                    Debug.Log("Sword hit something! collider");
                    Debug.Log("Name " + hit.collider.name);
                    CombatEvents.ApplyDamage(hit.transform.gameObject, damage);
                    enemyHit = true;
                }
            }
        }

        public void Hit()
        {
            Debug.Log("hit!");
        }

        public void HitStart()
        {
            Debug.Log("hit start!");
            isHitting = true;
        }

        public void HitEnd()
        {
            Debug.Log("hit end!");
            isHitting = false;
            enemyHit = false;
        }
    }
}