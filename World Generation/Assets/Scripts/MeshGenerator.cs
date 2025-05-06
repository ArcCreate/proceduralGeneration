using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateMesh(float[,] heightMap, float multiplier)
    {
        //storing widht and height of map
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vindex = 0;

        //loop through the height map
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                meshData.vertices[vindex] = new Vector3(topLeftX + (width - 1 - x), heightMap[x, y] * multiplier, topLeftZ - (height - 1 - y));
                meshData.uvs[vindex] = new Vector2(x/(float)width, y/(float)height);

                if(x < width  - 1 && y < height - 1)
                {
                    meshData.AddTraingles(vindex, vindex + width + 1, vindex + width);
                    meshData.AddTraingles(vindex + width + 1, vindex, vindex + 1);
                }
                vindex++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int triangleIndex;

    public MeshData(int meshWdith, int meshHeight)
    {
        vertices = new Vector3[meshWdith * meshHeight];
        triangles = new int[(meshWdith - 1) * (meshHeight - 1) * 6];
        uvs = new Vector2[meshWdith * meshHeight];
    }

    public void AddTraingles(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
