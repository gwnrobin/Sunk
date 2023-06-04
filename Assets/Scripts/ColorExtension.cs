using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension 
{
    public static bool IsSimilarTo(this Color c1, Color c2, float tolerance)
    {
        return Mathf.Abs(c1.r - c2.r) < tolerance &&
               Mathf.Abs(c1.g - c2.g) < tolerance &&
               Mathf.Abs(c1.b - c2.b) < tolerance;
    }
}
