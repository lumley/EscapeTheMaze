using UnityEngine;

namespace HUD
{
    public class Compass : MonoBehaviour {

        public void SetRotation(float rotation){
            transform.Rotate (0, 0, rotation);
        }
    }
}
