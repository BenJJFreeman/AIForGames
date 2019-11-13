using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGeneration  {

    public static Texture2D GenerateStarTexture(int width, int height, float scale, float xOrg, float yOrg, int seed, Gradient colouring)
    {
        return GeneratePlanetTexture(width, height, scale, xOrg, yOrg, seed,colouring);
    }
    public static Texture2D GeneratePlanetTexture(int width, int height, float scale, float xOrg, float yOrg, int seed, Gradient colouring)
    {
        Texture2D texture = new Texture2D(width, height);
        Color[] pix = new Color[width * height];
        // For each pixel in the texture...
        float y = 0.0F;

        while (y < texture.height)
        {
            float x = 0.0F;
            while (x < texture.width)
            {
                float xCoord = xOrg + x / texture.width * scale;
                float yCoord = yOrg + y / texture.height * scale;
                //float sample = Mathf.PerlinNoise(xCoord, yCoord);
                float sample = SimplexNoise.SeamlessNoise(xCoord, yCoord, scale, scale, seed);
                //pix[(int)y * texture.width + (int)x] = new Color(sample, sample, sample);
                pix[(int)y * texture.width + (int)x] = colouring.Evaluate(sample);
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        texture.SetPixels(pix);
        texture.Apply();

        return texture;
    }


}
