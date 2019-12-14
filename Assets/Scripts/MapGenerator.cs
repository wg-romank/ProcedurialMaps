using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public int seed;
    public float noiseScale;
    public bool autoUpdate;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;

    public void GenerateMap()
    {
        var noiseMap = Noise.GenerateNoise(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        var display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (noiseScale < 0)
        {
            noiseScale = 0;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
    }
}
