using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum MapMode { NoiseMap, ColorMap};
    public MapMode mapMode;

    public int width;
    public int height;
    public float scale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;
    public bool updateMap;

    public TerrainType[] terrains;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoise(width, height, scale, octaves, persistance, lacunarity, seed, offset);

        Color[] colourMap = new Color[width * height];
        for(int y = 0; y < height; y++)
        {
            for(int x = 0;  x < width; x++)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < terrains.Length; ++i)
                {
                    if(currentHeight <= terrains[i].height)
                    {
                        colourMap[y * width + x] = terrains[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(mapMode == MapMode.NoiseMap)
        {
            display.DrawTecture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(mapMode == MapMode.ColorMap)
        {
            display.DrawTecture(TextureGenerator.TextureFromColorMap(colourMap, width, height));
        }

    }

    private void OnValidate()
    {
        if(width < 1)
        {
            width = 1;
        }
        if(height < 1)
        {
            height = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
