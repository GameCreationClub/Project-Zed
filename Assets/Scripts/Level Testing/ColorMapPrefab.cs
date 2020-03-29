using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMapPrefab : MonoBehaviour
{
    public Image colorImg, prefabImg;
    public Text nameText;

    public void SetColorMap(Color color, Sprite prefabSprite, string name)
    {
        colorImg.color = color;
        prefabImg.sprite = prefabSprite;
        nameText.text = name;
    }
}
