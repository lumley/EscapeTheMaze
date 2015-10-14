using UnityEngine;


public class Sword : MonoBehaviour, IWeapon {

	private Animator animator;
	
	public void Awake(){
		animator =GetComponent<Animator>();
	}

	public void Attack(){
		animator.Play("Attack");
		Debug.Log("Swing!");
		RaycastHit raycast;
		
		
	}
	public void Hit(){
		Debug.Log("hit!");
	}

}
