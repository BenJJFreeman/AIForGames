using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator {

    static int[] starTypeHeatArray = new int[]{
            10,
            9,
            8,
            7,
            6,
            5,
            4

        };
    static char[] starTypeArray = new char[]{
            'O',
            'B',
            'A',
            'F',
            'G',
            'K',
            'M'
        };
    public static GameObject GenerateStar()
    {
        GameObject star = new GameObject("Star");


        star.AddComponent<MeshRenderer>();
        star.AddComponent<MeshFilter>();
        int randomStar = Random.Range(0, starTypeArray.Length);
        star.AddComponent<StarInfo>().SetUpStar(starTypeArray[randomStar],starTypeHeatArray[randomStar]);

        star.GetComponent<MeshFilter>().mesh = MeshGenerator.CreateSphereMesh();
        star.GetComponent<MeshRenderer>().material.mainTexture = NoiseGeneration.GenerateStarTexture(124, 124, 3, 25, 25, Random.Range(0, 10000),StarColour.GetStarColour(starTypeArray[randomStar]));

        


        return star;
    }
    
}
