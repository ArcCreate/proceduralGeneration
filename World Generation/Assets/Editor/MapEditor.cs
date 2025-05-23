using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;
        if (DrawDefaultInspector())
        {
            if (mapGenerator.updateMap)
            {
                mapGenerator.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGenerator.GenerateMap();
        }        
    }
}
