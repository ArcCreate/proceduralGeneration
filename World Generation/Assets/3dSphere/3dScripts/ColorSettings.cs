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
}
