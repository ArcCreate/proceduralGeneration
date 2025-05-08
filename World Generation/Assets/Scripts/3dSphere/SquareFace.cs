using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareFace
{
    // The mesh that this face modifies
    Mesh mesh;

    // Number of vertices per side of the face (higher = more detailed)
    int resolution;

    // The "up" direction for this face of the cube (defines orientation)
    Vector3 localUp;

    // Vectors perpendicular to localUp that define the face's local 2D space
    Vector3 axis1;
    Vector3 axis2;

    // Constructor: initializes mesh and orientation data for this square face
    public SquareFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        // Create two perpendicular axes on the plane of the face
        axis1 = new Vector3(localUp.y, localUp.z, localUp.x); // arbitrarily rotated version of localUp
        axis2 = Vector3.Cross(localUp, axis1); // ensures axis2 is orthogonal to both
    }

    // Generates the mesh geometry for this face, mapping it onto a sphere
    public void generateMesh()
    {
        // Create arrays for vertex positions and triangle indices
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        // Loop through a grid of points based on resolution
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // Flatten 2D grid coordinates into a 1D array index
                int i = x + y * resolution;

                // Get normalized grid coordinates (0 to 1 range)
                Vector2 percent = new Vector2(x, y) / (resolution - 1);

                // Calculate the position on the face of the cube
                // Shifts from -1 to 1 range across the face using axis1 and axis2
                Vector3 point = localUp + (percent.x - 0.5f) * 2 * axis1 + (percent.y - 0.5f) * 2 * axis2;

                // Project the point onto a unit sphere (cube-to-sphere projection)
                Vector3 pointOnSphere = point.normalized;

                // Store the calculated vertex position
                vertices[i] = pointOnSphere;

                // Create two triangles per grid square, except on the final row/column
                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    // Move to next triangle slot
                    triIndex += 6; 
                }
            }
        }

        // Finalize and assign the mesh data
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); 
    }
}
