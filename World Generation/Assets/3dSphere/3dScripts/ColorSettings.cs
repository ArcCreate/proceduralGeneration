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
    // Color settings
    public Color sandColor;
    public Color rockyLowColor;
    public Color rockyHighColor;

    public Color grassA;
    public Color grassB;
    public Color grassC;
    public Color grassD;

    // Control parameters
    [Range(0, 1)] public float grassMaxElevation = 0.6f;
    [Range(0, 1)] public float flatSteepnessThreshold = 0.3f;
    [Range(0, 1)] public float rockySteepnessThreshold = 0.6f;

    [Range(0, 1)] public float elevationLowThreshold = 0.3f;
    [Range(0, 1)] public float elevationHighThreshold = 0.7f;
}
