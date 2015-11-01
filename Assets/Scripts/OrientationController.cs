using UnityEngine;
using System.Collections;

public class OrientationController : MonoBehaviour {

	public void SetRotation(float rotation){
		transform.Rotate (0, rotation, 0);
		GameObject.Find ("Compass").SendMessage ("SetRotation", rotation);
	}

}
