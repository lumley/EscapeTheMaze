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
			IsDying=false;
		} else if (HasFinishedDying()){
			Destroy(gameObject);
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
			Debug.Log("Received "+damage+" damage!");
			health-=damage;
		}
	}

	public void Die(){
		IsDead=true;
		IsDying=true;
		health=0;
	}
}
