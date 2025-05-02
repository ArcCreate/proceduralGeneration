using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoise (int width, int height, float scale)
    {
        float[,] map = new float[width, height];

        //if scale is 0 because you don't want a divide by 0 error
        if(scale <= 0)
        {
            scale = 0.0001f;
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                map[x, y] = perlinValue;
            }
        }

        return map;
    }
}
