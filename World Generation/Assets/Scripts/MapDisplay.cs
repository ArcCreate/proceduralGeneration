using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    // The renderer component where the generated noise texture will be applied
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    // Function to visualize a 2D float array (noise map) as a texture
    public void DrawTecture(Texture2D texture)   {
        

        // Assign the generated texture to the plane
        textureRenderer.sharedMaterial.mainTexture = texture;

        // Scale the renderer's transform to match the texture size
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
