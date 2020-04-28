using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Map Collection", menuName = "ScriptableObjects/Color Map Collection")]
public class ColorMapCollection : ScriptableObject
{
    public ColorMap[] colorMaps;
}
