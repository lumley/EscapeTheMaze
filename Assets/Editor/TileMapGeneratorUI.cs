using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileMapGenerator))]
public class TileMapGeneratorUI : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate"))
        {
            Resources.UnloadUnusedAssets();
            ((TileMapGenerator)target).GenerateMap();
        }
    }
}
