using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings shapeSettings;
    NoiseFilter noiseFilter;
    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        this.noiseFilter = new NoiseFilter(shapeSettings.noiseSettings);
    }

    public Vector3 CalculatePointOnPlanet(Vector3 point)
    {
        float height = noiseFilter.Evaluate(point);
        return point * shapeSettings.planetRadius * (1 + height);
    }
}
