using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class NewColorSetting : ScriptableObject
{
    //system
    public Material planetMaterial;

    [Header("Shore")]
    public Color shoreFlat;
    public Color shoreSteep;
    [Range(0f, 1f)] public float shoreOriginHeight;
    [Range(0f, 1f)] public float shoreFadeHeight;
    [Range(0f, 1f)] public float shoreSteepness;

    [Header("Grass/Greens")]
    public Color grassFlat;
    public Color grassSteep;
    [Range(0f, 1f)] public float grassOriginHeight;
    [Range(0f, 1f)] public float grassFadeHeight;
    [Range(0f, 1f)] public float grassSteepness;

}
