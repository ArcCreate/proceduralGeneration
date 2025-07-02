using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColorGenerator
{

    ColorSettings settings;
    NewColorSetting newSettings;
    Texture2D texture;
    Texture2D steepTexture;
    const int resolution = 50;

    public void UpdateSettings(NewColorSetting settings)
    {
        this.newSettings = settings;
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
        //settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
        newSettings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
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

    public void UpdateColorsNew()
    {
        //shore
        newSettings.planetMaterial.SetColor("_ShoreFlatColor", newSettings.shoreFlat);
        newSettings.planetMaterial.SetFloat("_ShoreOriginHeight", newSettings.shoreOriginHeight);
        newSettings.planetMaterial.SetFloat("_ShoreFadeHeight", newSettings.shoreFadeHeight);
        newSettings.planetMaterial.SetFloat("_ShoreSteepness", newSettings.shoreSteepness);

        //grass flats
        newSettings.planetMaterial.SetColor("_GrassFlatColor", newSettings.grassFlat);
        newSettings.planetMaterial.SetFloat("_GrassOriginHeight", newSettings.grassOriginHeight);
        newSettings.planetMaterial.SetFloat("_GrassFadeHeight", newSettings.grassFadeHeight);
        newSettings.planetMaterial.SetFloat("_GrassSteepness", newSettings.grassSteepness);

        //forest greens
        newSettings.planetMaterial.SetColor("_ForestFlatColor", newSettings.forestFlat);
        newSettings.planetMaterial.SetFloat("_ForestOriginHeight", newSettings.forestOriginHeight);
        newSettings.planetMaterial.SetFloat("_ForestFadeHeight", newSettings.forestFadeHeight);
        newSettings.planetMaterial.SetFloat("_ForestSteepness", newSettings.forestSteepness);

        // snow caps
        newSettings.planetMaterial.SetColor("_SnowColor", newSettings.snow);
        newSettings.planetMaterial.SetFloat("_SnowOriginHeight", newSettings.snowOriginHeight);
        newSettings.planetMaterial.SetFloat("_SnowFadeHeight", newSettings.snowFadeHeight);
        newSettings.planetMaterial.SetFloat("_SnowSteepness", newSettings.snowSteepness);

        // cliff face
        newSettings.planetMaterial.SetColor("_CliffColor", newSettings.cliffColor);
        newSettings.planetMaterial.SetFloat("_CliffOriginHeight", newSettings.cliffOriginHeight);
        newSettings.planetMaterial.SetFloat("_CliffFadeHeight", newSettings.cliffFadeHeight);
        newSettings.planetMaterial.SetFloat("_CliffSteepness", newSettings.cliffSteepness);

        // snow flats (high & flat)
        newSettings.planetMaterial.SetColor("_SnowFlatColor", newSettings.snowFlatColor);
        newSettings.planetMaterial.SetFloat("_SnowFlatOriginHeight", newSettings.snowFlatOriginHeight);
        newSettings.planetMaterial.SetFloat("_SnowFlatFadeHeight", newSettings.snowFlatFadeHeight);
        newSettings.planetMaterial.SetFloat("_SnowFlatSteepness", newSettings.snowFlatSteepness);

        // vertical spires (steep & high)
        newSettings.planetMaterial.SetColor("_SpireColor", newSettings.spireColor);
        newSettings.planetMaterial.SetFloat("_SpireOriginHeight", newSettings.spireOriginHeight);
        newSettings.planetMaterial.SetFloat("_SpireFadeHeight", newSettings.spireFadeHeight);
        newSettings.planetMaterial.SetFloat("_SpireSteepness", newSettings.spireSteepness);


    }
}