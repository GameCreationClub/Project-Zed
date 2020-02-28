using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMapPrefab : MonoBehaviour
{
    public Image colorImg, prefabImg;

    public void SetColorMap(Color color, Sprite prefabSprite)
    {
        colorImg.color = color;
        prefabImg.sprite = prefabSprite;
    }
}
