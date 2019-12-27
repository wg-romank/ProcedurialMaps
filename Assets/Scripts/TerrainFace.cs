using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int trinagleIndex = 0;

        float[,] heightMap = Noise.GenerateNoise(resolution, resolution, 0, 100, 8, 0.271f, 1f, Vector2.zero); //new Vector2(localUp.x, localUp.z));

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int vertexIndex = y * resolution + x;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUniCube = localUp + (percent.x * 2 - 1) * axisA + (percent.y * 2 - 1) * axisB;
                Vector3 pointOnUniSphere = pointOnUniCube.normalized * (1 + heightMap[x, y]);
                vertices[vertexIndex] = pointOnUniSphere;

                if (x < resolution - 1 && y < resolution - 1)
                {
                    triangles[trinagleIndex++] = vertexIndex;
                    triangles[trinagleIndex++] = vertexIndex + resolution + 1;
                    triangles[trinagleIndex++] = vertexIndex + resolution;

                    triangles[trinagleIndex++] = vertexIndex;
                    triangles[trinagleIndex++] = vertexIndex + 1;
                    triangles[trinagleIndex++] = vertexIndex + resolution + 1;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
