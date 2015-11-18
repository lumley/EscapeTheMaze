using Combat.Events;
using UnityEngine;

namespace Combat
{
    [RequireComponent (typeof (Animator))]
    public class Health : MonoBehaviour, ITakeDamageHandler {
	
        private Animator animator;

        public int health=100;

        private bool IsDead{get; set;}
        private bool IsDying{get;set;}
	
        private bool IsDamageable{get;set;}

        public void Awake(){
            IsDead = false;
            IsDying = false;
            IsDamageable = true;
            animator = GetComponent<Animator>();
        }

        public void Update(){
            if (health <= 0 && IsAlive()){
                Die ();
            }

            if (IsDying){
                //do some dying animation
                IsDying=true;
                animator.Play("Dying");
            } else if (HasFinishedDying()){
                Destroy(gameObject);
            }
        }

        public bool IsAlive(){
            return IsDead == false;
        }

        public bool HasFinishedDying(){
            return IsDead && (IsDying == false);
        }

        public void ApplyDamage(int damage){
            if (IsAlive() && IsDamageable){
                IsDamageable = false;
                UnityEngine.Debug.Log("Received "+damage+" damage!");
                animator.Play("Damaged");
                health -= damage;
            }
        }

        public void Die(){
            IsDead=true;
            IsDying=true;
            IsDamageable=false;
            health=0;
        }
	
        public void FinishedDying(){
            IsDying=false;
        }
	
        public void Damageable(){
            IsDamageable=true;
        }

        public void OnTakeDamage(TakeDamageEventData damage)
        {
            ApplyDamage(damage.damage);
        }
    }
}
