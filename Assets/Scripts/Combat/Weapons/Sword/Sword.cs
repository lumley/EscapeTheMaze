﻿using UnityEngine;


public class Sword : MonoBehaviour, IWeapon {
	
	public int damage=10;
	private bool enemyHit=false;
	
	private bool isHitting=false;

	private Animator animator;
	
	public void Awake(){
		animator =GetComponent<Animator>();
	}
	
	public void Update(){
		if (isHitting){
			RaycastHit hit;
			
			Debug.Log("Forward "+Camera.main.transform.forward);
			Debug.Log("Transform position "+transform.parent.position);
			if (enemyHit==false && Physics.Raycast(transform.parent.position, Camera.main.transform.forward, out hit, 2.0f)){
				Debug.Log("Sword hit something! collider");
				Debug.Log("Name "+hit.collider.name);
				hit.transform.SendMessage("ApplyDamage", damage);
				enemyHit=true;
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
		enemyHit=false;
	}


}
