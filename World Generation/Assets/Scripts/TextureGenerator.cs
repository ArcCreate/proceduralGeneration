using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator
{
    public static Texture2D TextureFromColorMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        // Get dimensions of the noise map
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        // Create a new empty texture with same dimensions
        Texture2D texture = new Texture2D(width, height);

        // Prepare a 1D color array to represent the texture pixels
        Color[] colorMap = new Color[width * height];

        // Loop over every point in the noise map
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Convert noise value (0 to 1) to grayscale color
                // Lerp between black (0) and white (1)
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }
}
