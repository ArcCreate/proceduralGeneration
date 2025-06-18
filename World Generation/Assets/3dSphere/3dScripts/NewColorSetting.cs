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
    [Range(0f, 1f)] public float shoreOriginHeight;
    [Range(0f, 1f)] public float shoreFadeHeight;
    [Range(0f, 1f)] public float shoreSteepness;

    [Header("Grass Flat")]
    public Color grassFlat;
    [Range(0f, 1f)] public float grassOriginHeight;
    [Range(0f, 1f)] public float grassFadeHeight;
    [Range(0f, 1f)] public float grassSteepness;

    [Header("Forest Green")]
    public Color forestFlat;
    [Range(0f, 1f)] public float forestOriginHeight;
    [Range(0f, 1f)] public float forestFadeHeight;
    [Range(0f, 1f)] public float forestSteepness;

    [Header("Snow Caps")]
    public Color snowFlat;
    [Range(0f, 1f)] public float snowOriginHeight;
    [Range(0f, 1f)] public float snowFadeHeight;
    [Range(0f, 1f)] public float snowSteepness;

    [Header("Cliff Face")]
    public Color cliffColor;
    [Range(0f, 1f)] public float cliffOriginHeight;
    [Range(0f, 1f)] public float cliffFadeHeight;
    [Range(0f, 1f)] public float cliffSteepness;


}
