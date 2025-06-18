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
    [Range(0f, 1f)] public float shoreMinHeight;
    [Range(0f, 1f)] public float shoreMaxHeight;
    [Range(0f, 1f)] public float shoreSteepness;

}
