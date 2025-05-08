using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGeneration : MonoBehaviour
{
    // The resolution (number of vertices per side) of each face of the sphere.
    // Can be set between 2 and 256 in the inspector.
    [Range(2, 256)]
    public int resolution = 10;

    // Array of MeshFilter components, one for each cube face (6 in total).
    // Hidden in the Inspector to avoid clutter.
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;

    // Array of SquareFace objects used to generate and manage each mesh face.
    SquareFace[] terrainFaces;

    private void OnValidate()
    {
        Initialize();  
        GenerateMesh();
    }

    // Initializes the mesh filters and face data for each of the six cube faces
    void Initialize()
    {
        // If meshFilters haven't been created yet, initialize them
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }

        // Initialize the terrain face array
        terrainFaces = new SquareFace[6];

        // Define the six directions corresponding to the faces of a cube
        Vector3[] directions = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        // Loop through each face of the cube
        for (int i = 0; i < 6; i++)
        {
            // If this face's mesh filter doesn't exist, create a new GameObject and set it up
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform; // Make it a child of the planet

                // Add required components to render the mesh
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            // Create a SquareFace to generate the mesh for this face
            terrainFaces[i] = new SquareFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        foreach (SquareFace face in terrainFaces)
        {
            face.generateMesh();
        }
    }
}
