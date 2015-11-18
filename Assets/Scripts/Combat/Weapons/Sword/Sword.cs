using Combat.Events;
using Combat.Model;
using UnityEngine;

namespace Combat.Weapons.Sword
{
    public class Sword : MonoBehaviour, IWeapon
    {

        public int damage = 10;
        private bool enemyHit = false;

        private bool isHitting = false;

        private Animator animator;

        public void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (isHitting)
            {
                RaycastHit hit;

                UnityEngine.Debug.Log("Forward " + Camera.main.transform.forward);
                UnityEngine.Debug.Log("Transform position " + transform.parent.position);
                if (enemyHit == false && Physics.Raycast(transform.parent.position, Camera.main.transform.forward, out hit, 2.0f))
                {
                    UnityEngine.Debug.Log("Sword hit something! collider");
                    UnityEngine.Debug.Log("Name " + hit.collider.name);
                    CombatEvents.ApplyDamage(hit.transform.gameObject, damage);
                    enemyHit = true;
                }
            }
        }

        public void Attack()
        {
            animator.Play("Attack");
            UnityEngine.Debug.Log("Swing!");

        }
        public void Hit()
        {
            UnityEngine.Debug.Log("hit!");
        }

        public void HitStart()
        {
            UnityEngine.Debug.Log("hit start!");
            isHitting = true;
        }

        public void HitEnd()
        {
            UnityEngine.Debug.Log("hit end!");
            isHitting = false;
            enemyHit = false;
        }


    }
}
