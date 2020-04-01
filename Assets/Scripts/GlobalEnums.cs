using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnums : MonoBehaviour
{
    public enum PowerupType
    {
        Dash, DoubleJump, GravityFlip
    };

    public static readonly Vector2 Vector2Nan = new Vector2(float.NaN, float.NaN);
}
