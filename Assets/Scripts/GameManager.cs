using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {
	
	[SerializeField]
	private TileMapGenerator tileMap;
	
	[SerializeField]
	private GameObject player;
	
	public RandomProvider seedProvider;

	void Awake() {
		tileMap.GenerateMap();
		
		List<KeyValuePair<Vector2, Model.Tile>> startingPoints = new List<KeyValuePair<Vector2, Model.Tile>>();
		foreach(KeyValuePair<Vector2, Model.Tile> entry in tileMap.Map2D){
			if (entry.Value.HasAttribute(Model.TileAttribute.Type.SPAWNING_POINT)){
				startingPoints.Add(entry);
			}
		}
		
		KeyValuePair<Vector2, Model.Tile> selectedStartingTile = seedProvider.GetRandomElement(startingPoints);
		Vector2 selectedStartingPoint = selectedStartingTile.Key;
		player.transform.position = new Vector3(selectedStartingPoint.x, player.transform.position.y, selectedStartingPoint.y);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
