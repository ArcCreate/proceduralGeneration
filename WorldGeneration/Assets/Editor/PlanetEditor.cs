using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Numerics;

[CustomEditor(typeof(PlanetGeneration))]
public class PlanetEditor : Editor
{

    PlanetGeneration planet;
    Editor shapeEditor;
    Editor colourEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.generatePlanet();
            }
        }

        if (GUILayout.Button("Generate Planet"))
        {
            planet.generatePlanet();
        }

        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeUpdate, ref planet.shapeSettingFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.colorSettings, planet.OnColorUpdate, ref planet.colorSettingFoldout, ref colourEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        planet = (PlanetGeneration)target;
    }
}