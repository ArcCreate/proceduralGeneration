using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    // The renderer component where the generated noise texture will be applied
    public Renderer textureRenderer;

    // Function to visualize a 2D float array (noise map) as a texture
    public void DrawNoiseMap(float[,] noiseMap)
    {
        // Get dimensions of the noise map
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

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
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        // Apply the color array to the texture
        texture.SetPixels(colorMap);
        texture.Apply();

        // Assign the generated texture to the plane
        textureRenderer.sharedMaterial.mainTexture = texture;

        // Scale the renderer's transform to match the texture size
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }
}
