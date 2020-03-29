using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectList : MonoBehaviour
{
    public GameObject colorMapPrefab;
    public Transform parent;

    public RectTransform scroll;

    private void Start()
    {
        GenerateObjectList(FindObjectOfType<LevelGenerator>().colorMaps);
    }

    public void GenerateObjectList(ColorMap[] colorMaps)
    {
        foreach (ColorMap colorMap in colorMaps)
        {
            Instantiate(colorMapPrefab, parent)
            .GetComponent<ColorMapPrefab>()
            .SetColorMap(colorMap.color,
            colorMap.prefab.GetComponent<SpriteRenderer>().sprite,
            colorMap.prefab.name);
        }

        scroll.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 130f * colorMaps.Length);
    }
}
