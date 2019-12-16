using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator
{
    public static Texture2D CreateTextureFromHeightMap(float[,] heightMap) {
        var width = heightMap.GetLength(0);
        var height = heightMap.GetLength(1);
        var pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                pixels[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return CreateTextureFromPixels(pixels, width, height);
    }

    public static Texture2D CreateTextureFromPixels(Color[] pixels, int width, int height) {
        var texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        return texture;
    }
}
