using UnityEngine;
using System.Collections;

public class OrientationController : MonoBehaviour {

	public void SetRotation(float rotation){
		transform.Rotate (0, rotation, 0);
	}

}
