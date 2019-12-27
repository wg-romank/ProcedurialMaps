using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {

  public static Mesh GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve meshHightCurve, int lod) {
    int width = heightMap.GetLength(0);
    int height = heightMap.GetLength(1);

    float topLeftX = (width - 1) / -2f;
    float topLeftZ = (height - 1) / 2f;

    int incrementStep = lod > 0 ? lod * 2 : 1;
    int verticesPerLine = (width - 1) / incrementStep + 1;

    Vector3[] vertices = new Vector3[verticesPerLine * verticesPerLine];
    Vector2[] uvs = new Vector2[verticesPerLine * verticesPerLine];
    int[] triangles = new int[(verticesPerLine - 1) * (verticesPerLine - 1) * 6];

    int vertexIdx = 0;
    int trianglesIdx = 0;

    for (int y = 0; y < height; y += incrementStep) {
      for (int x = 0; x < width; x += incrementStep) {
        vertices[vertexIdx] = new Vector3(topLeftX + x, heightMultiplier * meshHightCurve.Evaluate(heightMap[x, y]), topLeftZ - y);
        uvs[vertexIdx] = new Vector2(x / (float)width, y / (float)height);

        if (x < width - 1 && y < height - 1) {
          triangles[trianglesIdx++] = vertexIdx;
          triangles[trianglesIdx++] = vertexIdx + verticesPerLine + 1;
          triangles[trianglesIdx++] = vertexIdx + verticesPerLine;

          triangles[trianglesIdx++] = vertexIdx + verticesPerLine + 1;
          triangles[trianglesIdx++] = vertexIdx;
          triangles[trianglesIdx++] = vertexIdx + 1;
        }

        vertexIdx++;
      }
    }

    var mesh = new Mesh();
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uvs;
    mesh.RecalculateNormals();

    return mesh;
  }

}
