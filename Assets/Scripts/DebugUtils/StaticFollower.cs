using UnityEngine;

namespace DebugUtils
{
    public class StaticFollower : MonoBehaviour {

        public GameObject gameObjectToFollow;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
            if (UnityEngine.Debug.isDebugBuild)
            {
                this.transform.position = gameObjectToFollow.transform.position;
            }
        }
    }
}
