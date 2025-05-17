using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    public Gradient gradient;
    public Gradient steepness;
    public Material planetMaterial;

    [Range(0f, 1f)]
    public float steepnessThreshold = 0.5f;
    [Range(0f, 1f)]
    public float steepnessBlend = 0.5f;

    //new elevation colors
    [Range(0f, 1f)] public float LowThreshold;
    [Range(0f, 1f)] public float HighThreshold;
    [Range(0f, 0.5f)] public float SteepThreshold;
    [Range(0f, 1f)] public float GrassHeightLimit;

    [Header("Color Controls")]
    public Color ShoreLow;
    public Color ShoreHigh;

    public Color FlatLowA;
    public Color FlatLowB;

    public Color FlatHighA;
    public Color FlatHighB;

    public Color SteepLow;
    public Color SteepHigh;
}
