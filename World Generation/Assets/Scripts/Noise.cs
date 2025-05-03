using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * SCRIPT TERMINOLOGY CHEAT SHEET
 * 
 * WIDTH AND HEIGHT: Dimensions of the noise map
 * 
 * SCALE: Controls zooming into the perlin noise, lower scale means seeing tiny fomations;
 *        Higher scale means seeing large fomations.
 * 
 * OCTAVES: The number of layers of noise stacked on top of each other;
 *          More octaves mean more detail, each layered octave changes curves sligthly to feel
 *          more realistic terrair.
 *          Amplitude controls how much each octave has an influence on the map; Less influence as 
 *          actaves increase.
 *          Frequency controls how often features repear; High frequency equals more detail.
 *          
 * Persistence: In relation to Octaves, controls how the amplitude decreas with each octave;
 *              A persistence of 0.5, means the next octave is half as strong; 1.0 = same.
 *              
 * Lucanarity: In relation to octaves, controls how frequency increases per octave;
 *              A lacunarity of 2 means frequency doubles, features get 2x smaller and more detialed. 
 * 
 * SEED: random generator for different noise maps.
 * 
 * 
 * OFFSET: Shifting the noise map in different directions. 
 * */
public static class Noise
{
    public static float[,] GenerateNoise (int width, int height, float scale, int octaves, float persistance, float lucanarity, int seed, Vector2 offset)
    {
        //creates a noise map to store nosie values
        float[,] map = new float[width, height];

        //randome seed generator to reproduce maps with same seed
        System.Random rand = new System.Random(seed);

        //octave offset
        Vector2[] octaveOffset = new Vector2[octaves];
        for(int i = 0; i < octaves; ++i)
        {
            float offsetX = rand.Next(-100000, 100000) + offset.x;
            float offsetY = rand.Next(-100000, 100000) + offset.y;
            octaveOffset[i] = new Vector2(offsetX, offsetY);
        }

        //if scale is 0 because you don't want a divide by 0 error
        if(scale <= 0)
        {
            scale = 0.0001f;
        }

        //hold max and min height to later normalize the map [0, 1]
        float maxHeight = float.MinValue;
        float minHeight = float.MaxValue;
        
        //to make zooming into the middle
        float halfWdith = width / 2f;
        float halfheight = height / 2f;

        //loop through every point in map
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //initialization for layer 1
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                //for each octave
                for(int i = 0; i < octaves; ++i)
                {
                    // apply perlin sample space with frequency and offset
                    float sampleX = (x - halfWdith) / scale * frequency + octaveOffset[i].x;
                    float sampleY = (y - halfheight) / scale * frequency + octaveOffset[i].y;

                    //get perlin noise and restrict it to [-1, 1]
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    //chance amplitude and frquency for the next octave
                    amplitude *= persistance;
                    frequency *= lucanarity;
                }

                //update max and min
                if(noiseHeight > maxHeight)
                {
                    maxHeight = noiseHeight;
                }
                else if(noiseHeight < minHeight)
                {
                    minHeight = noiseHeight;
                }
                //store height
                map[x, y] = noiseHeight;
            }
        }

        //normalize map
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, y] = Mathf.InverseLerp(minHeight, maxHeight, map[x, y]);
            }
        }

        return map;
    }
}
