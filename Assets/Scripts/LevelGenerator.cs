﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorMap[] colorMaps;

    public Vector2 tileSize;

    public void GenerateLevel()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    private void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
            return;

        GameObject prefab = GetPrefabFromColor(pixelColor);

        if (prefab == null)
            Debug.LogWarning($"Color {pixelColor} at {x}, {y} does not map to any object");
        else
            Instantiate(prefab, new Vector2(x * tileSize.x, (y * tileSize.y)), Quaternion.identity).transform.parent = transform;
    }

    private GameObject GetPrefabFromColor(Color color)
    {
        foreach (ColorMap colorMap in colorMaps)
        {
            if (colorMap.color.Equals(color))
                return colorMap.prefab;
        }

        return null;
    }
}

[System.Serializable]
public class ColorMap
{
    public Color color = Color.black;
    public GameObject prefab;
}
