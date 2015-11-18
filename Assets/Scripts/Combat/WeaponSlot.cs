using UnityEngine;

namespace Combat
{
    public class WeaponSlot : MonoBehaviour {

        public GameObject weapon;

        public void Attack(){
            weapon.SendMessage ("Attack");
        }
    }
}