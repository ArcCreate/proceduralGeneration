using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxHeight
{
    public float Min { get; private set; }
    public float Max { get; private set; }

    public MinMaxHeight()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
    }

    public void AddValue(float v)
    {
        if (v > Max)
        {
            Max = v;
        }
        if (v < Min)
        {
            Min = v;
        }
    }
}
