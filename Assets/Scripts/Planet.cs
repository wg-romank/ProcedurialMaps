using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool autoUpdate;
    [Range(1, 255)]
    public int resolution = 6;

    TerrainFace[] faces;
    MeshFilter[] meshFilters;
    Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    public void Initialize()
    {
        if (faces == null || faces.Length == 0)
        {
            faces = new TerrainFace[directions.Length];
            meshFilters = new MeshFilter[directions.Length];

            for (int i = 0; i < directions.Length; i++)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
        }
    }

    public void ConstructMesh()
    {
        Initialize();
        for (int i = 0; i < directions.Length; i++) {
                faces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
                faces[i].ConstructMesh();
        }
    }
}
