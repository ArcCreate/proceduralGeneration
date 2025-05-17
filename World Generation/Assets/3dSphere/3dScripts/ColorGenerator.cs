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
        settings.planetMaterial.SetFloat("_LowThreshhold", settings.LowThreshold);
        settings.planetMaterial.SetFloat("_HighThreshhold", settings.HighThreshold);
        settings.planetMaterial.SetFloat("_SteepThreshold", settings.SteepThreshold);
        settings.planetMaterial.SetFloat("_GrassLimit", settings.GrassHeightLimit);

        // Set Colors
        settings.planetMaterial.SetColor("_shoreLow", settings.ShoreLow);
        settings.planetMaterial.SetColor("_shoreHigh", settings.ShoreHigh);

        settings.planetMaterial.SetColor("_FlatLow1", settings.FlatLowA);
        settings.planetMaterial.SetColor("_FlatLow2", settings.FlatLowB);

        settings.planetMaterial.SetColor("_FlatHigh1", settings.FlatHighA);
        settings.planetMaterial.SetColor("_FlatHigh2", settings.FlatHighB);

        settings.planetMaterial.SetColor("_SteepLow", settings.SteepLow);
        settings.planetMaterial.SetColor("_SteepHigh", settings.SteepHigh);

    }
}