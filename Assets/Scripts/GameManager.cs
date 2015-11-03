using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {
	
	[SerializeField]
	private TileMap tileMap;
	
	[SerializeField]
	private GameObject player;

    [SerializeField]
    private int seed;

	void Awake() {
        Random.seed = seed;
		tileMap.GenerateMap();
		
		List<KeyValuePair<IntPair, Model.Tile>> startingPoints = new List<KeyValuePair<IntPair, Model.Tile>>();
		foreach(KeyValuePair<IntPair, Model.Tile> entry in tileMap.Map2D){
			if (entry.Value.HasAttribute(Model.TileAttribute.Type.SPAWNING_POINT)){
				startingPoints.Add(entry);
			}
		}
		
		KeyValuePair<IntPair, Model.Tile> selectedStartingTile = RandomProvider.GetRandomElement(startingPoints);
        Vector3 selectedStartingPoint = new Vector3(selectedStartingTile.Key.x, 1.3f, selectedStartingTile.Key.y); ;
        player.transform.position = selectedStartingPoint;
        player.SendMessage("SetCurrentTile", selectedStartingTile.Value);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
