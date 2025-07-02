using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory
{
    public static InoiseFilter createNoiseFilter(NoiseSettings noiseSettings)
    {
        switch(noiseSettings.type)
        {
            case NoiseSettings.filterType.Simple:
                return new SimpleNoiseFilter(noiseSettings.simpleNoiseSettings);
            case NoiseSettings.filterType.Rigid:
                return new RigidNoiseFilter(noiseSettings.rigidNoiseSettings);
        }
        return null;
    }
}
