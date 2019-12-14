using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoise(int width, int hight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        var map = new float[width, hight];

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		var prng = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			// Perlin noise constraints
			float offsetX = prng.Next(-100000, 100000);
			float offsetY = prng.Next(-100000, 100000);
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}
		
		float halfW = width / 2f;
		float halfH = hight / 2f;

        for (int y = 0; y < hight; y++)
        {
            for (int x = 0; x < width; x++)
            {
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfW) / scale * frequency + octaveOffsets[i].x + offset.x;
                    float sampleY = (y - halfH) / scale * frequency + octaveOffsets[i].y + offset.y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
                }

				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				} else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}
				map[x, y] = noiseHeight;
            }
        }

		for (int y = 0; y < hight; y++) {
			for (int x = 0; x < width; x++) {
				map[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, map[x, y]);
			}
		}

        return map;
    }
}
