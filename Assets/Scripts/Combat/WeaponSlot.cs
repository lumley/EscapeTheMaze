using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour {

	public GameObject weapon;

	public void Attack(){
		weapon.SendMessage ("Attack");
	}
}