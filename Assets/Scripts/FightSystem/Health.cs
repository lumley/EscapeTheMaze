using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health=100;

	private bool IsDead{get; set;}
	private bool IsDying{get;set;}

	public void Awake(){
		IsDead=false;
		IsDying=false;
	}

	public void Update(){
		if (health<=0 && IsAlive()){
			Die ();
		}

		if (IsDying){
			//do some dying animation
		}
	}

	private bool IsAlive(){
		return IsDead == false;
	}

	private bool HasFinishedDying(){
		return IsDead && IsDying == false;
	}

	public void ApplyDamage(int damage){
		if (IsAlive()){
			health-=damage;
		}
	}

	public void Die(){
		IsDead=true;
		IsDying=true;
		health=0;
	}
}
