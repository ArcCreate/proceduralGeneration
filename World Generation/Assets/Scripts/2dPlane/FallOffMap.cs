using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class FallOffMap
{
    public static float[,] GenerateFallout(int size)
    {
        float[,] map = new float[size, size];

        for(int i = 0; i < size; ++i)
        {
            for(int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x) , Mathf.Abs(y));
                map[i, j] = Evalutate(value);
            }
        }
        return map;
    }

    static float Evalutate(float value)
    {
        float a = 2f;
        float b = 3f;
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
