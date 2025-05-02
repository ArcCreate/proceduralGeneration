using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public float scale;

    public bool updateMap;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoise(width, height, scale);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);

    }
}
