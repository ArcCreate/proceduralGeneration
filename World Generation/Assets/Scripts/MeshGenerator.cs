using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static void GenerateMesh(float[,] heightMap)
    {
        //storing widht and height of map
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / 2;
        float topLeftZ = (height - 1) / 2;

        MeshData meshData = new MeshData(width, height);
        int vindex = 0;

        //loop through the height map
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                meshData.vertices[vindex] = new Vector3(topLeftX + x, heightMap[x, y], topLeftZ - y);
                vindex++;
            }
        }
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;

    int triangleIndex;

    public MeshData(int meshWdith, int meshHeight)
    {
        vertices = new Vector3[meshWdith * meshHeight];
        triangles = new int[(meshWdith - 1) * (meshHeight - 1) * 6];
    }

    public void AddTraingles(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }
}
