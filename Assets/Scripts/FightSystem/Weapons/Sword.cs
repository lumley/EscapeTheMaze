using UnityEngine;


public class Sword : MonoBehaviour, IWeapon {
	
	public int damage=10;
	
	private bool isHitting=false;

	private Animator animator;
	
	public void Awake(){
		animator =GetComponent<Animator>();
	}
	
	public void Update(){
		if (isHitting){
			RaycastHit hit;
			Debug.Log("Forward "+transform.forward);
			if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f)){
				Debug.Log("Sword hit something!");
				
				hit.collider.SendMessageUpwards("ApplyDamage", damage);
			}
			
		}
	}

	public void Attack(){
		animator.Play("Attack");
		Debug.Log("Swing!");
		
	}
	public void Hit(){
		Debug.Log("hit!");
	}
	
	public void HitStart(){
		Debug.Log("hit start!");
		isHitting=true;
	}
	
	public void HitEnd(){
		Debug.Log("hit end!");
		isHitting=false;
	}


}
