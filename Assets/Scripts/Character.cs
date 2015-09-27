using UnityEngine;

public class Character : MonoBehaviour {
	private NavMeshAgent agent;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 forward = Vector3.zero;
	private Vector3 right = Vector3.zero;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update()
	{
		Vector3 movingDirection = Vector3.zero;

		movingDirection.x=Input.GetAxis ("Horizontal") * Time.deltaTime;

		movingDirection.z = Input.GetAxis ("Vertical") * Time.deltaTime;
		Debug.Log ("V "+Input.GetAxis ("Vertical")+" H "+Input.GetAxis ("Horizontal"));
		agent.Move(movingDirection);
	}

}
