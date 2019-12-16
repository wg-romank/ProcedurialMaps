using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { HeightMap, Texture2D, Mesh };
    public DrawMode drawMode;

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

    public float heightMultiplier;
    public AnimationCurve meshHeightCurve;

    public TerrainType[] types;

    public TerrainType MatchTerrainType(float height)
    {
        for (int i = 0; i < types.Length; i++)
        {
            if (types[i].height >= height)
            {
                return types[i];
            }
        }
        return types[types.Length - 1];
    }

    public void GenerateMap()
    {
        var noiseMap = Noise.GenerateNoise(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        var display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.HeightMap)
        {
            display.DrawTexture(TextureGenerator.CreateTextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.Texture2D)
        {
            Color[] colorMap = new Color[mapWidth * mapHeight];
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    colorMap[y * mapWidth + x] = MatchTerrainType(noiseMap[x, y]).color;
                }
            }
            display.DrawTexture(TextureGenerator.CreateTextureFromPixels(colorMap, mapWidth, mapHeight));
        } else if (drawMode == DrawMode.Mesh) {
            Color[] colorMap = new Color[mapWidth * mapHeight];
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    colorMap[y * mapWidth + x] = MatchTerrainType(noiseMap[x, y]).color;
                }
            }
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, heightMultiplier, meshHeightCurve), TextureGenerator.CreateTextureFromPixels(colorMap, mapWidth, mapHeight));
        }
            
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


[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}