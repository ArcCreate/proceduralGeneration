using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings shapeSettings;
    InoiseFilter[] noiseFilters;
    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        this.noiseFilters = new InoiseFilter[shapeSettings.noiseLayers.Length];
        for(int i = 0; i < shapeSettings.noiseLayers.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory.createNoiseFilter(shapeSettings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 point)
    {
        float maskValue = 0;
        float height = 0;

        if (noiseFilters.Length > 0)
        {
            maskValue = noiseFilters[0].Evaluate(point);
            if (shapeSettings.noiseLayers[0].enaled)
            {
                height = maskValue;
            }
        }

        for (int i = 1; i < shapeSettings.noiseLayers.Length; ++i)
        {
            if (shapeSettings.noiseLayers[i].enaled)
            {
                float maskk = (shapeSettings.noiseLayers[i].useMasking ? maskValue : 1);
                height += noiseFilters[i].Evaluate(point) * maskk;
            }
        }
        return point * shapeSettings.planetRadius * (1 + height);
    }
}
