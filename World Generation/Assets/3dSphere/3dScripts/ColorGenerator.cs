using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColorGenerator
{

    ColorSettings settings;
    Texture2D texture;
    Texture2D steepTexture;
    const int resolution = 50;

    public void UpdateSettings(ColorSettings settings)
    {
        this.settings = settings;
        if(texture == null)
        {
            texture = new Texture2D(resolution, 1);
        }
        if(steepTexture == null)
        {
            steepTexture = new Texture2D(resolution, 1);
        }
    }

    public void UpdateElevation(MinMaxHeight elevationMinMax)
    {
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[resolution];
        Color[] steepColor = new Color[resolution];
        for(int i = 0; i < resolution; i++)
        {
            colors[i] = settings.gradient.Evaluate(i / (resolution - 1f));
            steepColor[i] = settings.steepness.Evaluate(i / (resolution - 1f));

        }
        texture.SetPixels(colors);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);

        steepTexture.SetPixels(steepColor);
        steepTexture.Apply();
        settings.planetMaterial.SetTexture("_CliffTexture", steepTexture);
        settings.planetMaterial.SetFloat("_SteepnessThreshold", settings.steepnessThreshold);
        settings.planetMaterial.SetFloat("_SteepnessBlending", settings.steepnessBlend);

        //new steepness
        settings.planetMaterial.SetFloat("_GrassMaxElevation", settings.grassMaxElevation);
        settings.planetMaterial.SetFloat("_FlatSteepnessThreshold", settings.flatSteepnessThreshold);
        settings.planetMaterial.SetFloat("_RockySteepnessThreshold", settings.rockySteepnessThreshold);

        settings.planetMaterial.SetFloat("_ElevationLowThreshold", settings.elevationLowThreshold);
        settings.planetMaterial.SetFloat("_ElevationHighThreshold", settings.elevationHighThreshold);

        settings.planetMaterial.SetColor("_SandColor", settings.sandColor);
        settings.planetMaterial.SetColor("_RockyLowColor", settings.rockyLowColor);
        settings.planetMaterial.SetColor("_RockyHighColor", settings.rockyHighColor);
        settings.planetMaterial.SetColor("_GrassA", settings.grassA);
        settings.planetMaterial.SetColor("_GrassB", settings.grassB);
        settings.planetMaterial.SetColor("_GrassC", settings.grassC);
        settings.planetMaterial.SetColor("_GrassD", settings.grassD);
    }
}