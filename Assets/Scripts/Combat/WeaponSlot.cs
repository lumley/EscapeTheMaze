using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour {

	public GameObject weapon;
	public string fireButton;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(fireButton)){
			weapon.SendMessage("Attack");
		}
	}
}
