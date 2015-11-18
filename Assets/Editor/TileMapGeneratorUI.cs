using Map;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (TileMap))]
public class TileMapGeneratorUI : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate"))
        {
            Resources.UnloadUnusedAssets();
            ((TileMap) target).GenerateMap();
        }
    }
}