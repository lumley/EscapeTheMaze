using UnityEngine;

public class GameManager : MonoBehaviour {
	
	[SerializeField]
	private TileMapGenerator tileMap;
	
	[SerializeField]
	private GameObject player;

	void Awake() {
		tileMap.GenerateMap();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
