using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetType{
       Ice,
       ColdBarren,
       Rock,
       Ocean,
       Jungle,
       Continental,
       WetDesert,
       DryDesert,
       HotBarren,
       Lava,
    };
public class PlanetGenerator {

    
    public static GameObject GeneratePlanet(int planetNumber,int systemId,int starTemp,int distance,int size,float speed)
    {
        GameObject planet = new GameObject("Planet");


        planet.AddComponent<MeshRenderer>();
        planet.AddComponent<MeshFilter>();
        planet.AddComponent<SphereCollider>().radius = 2;

        PlanetType planetType = GetNewPlanetType(starTemp, distance);
        
        planet.AddComponent<PlanetInfo>().SetUpPlanet(planetNumber, systemId, planetType, CanPlanetSpawnLife(planetType),size,speed,GetNewPlanetResources(planetType));

        planet.GetComponent<MeshFilter>().mesh = MeshGenerator.CreateSphereMesh();
        planet.GetComponent<MeshRenderer>().material.mainTexture = NoiseGeneration.GeneratePlanetTexture(124, 124, 2, 25, 25,Random.Range(0,10000),PlanetColour.GetPlanetColour(planetType));


        

        return planet;
    }
    public static PlanetType GetNewPlanetType(int _starsTemp, int _distance)
    {

        int planetTemp = _starsTemp - _distance;

        planetTemp += Random.Range(-1, 2);

        if (planetTemp < 0)
            planetTemp = 0;

        if (planetTemp >= 9)
            planetTemp = 9;


        return (PlanetType)planetTemp;        
    }
    public static bool CanPlanetSpawnLife(PlanetType _planetType)
    {

        switch (_planetType)
        {
            case PlanetType.Ice:
                return Random.Range(0, 125) == 0;
            case PlanetType.ColdBarren:
                return Random.Range(0, 90) == 0;
            case PlanetType.Rock:
                return Random.Range(0, 25) == 0;
            case PlanetType.Ocean:
                return Random.Range(0, 15) == 0;
            case PlanetType.Jungle:
                return Random.Range(0, 2) == 0;
            case PlanetType.Continental:
                return Random.Range(0, 20) == 0;
            case PlanetType.WetDesert:
                return Random.Range(0, 75) == 0;
            case PlanetType.DryDesert:
                return Random.Range(0, 100) == 0;
            case PlanetType.HotBarren:
                return Random.Range(0, 150) == 0;
            case PlanetType.Lava:
                return Random.Range(0, 250) == 0;


            default: return false;
        }

    }
    public static int GetNewPlanetSize()
    {

        return Random.Range(1, 4);

    }
    public static Resources GetNewPlanetResources(PlanetType _planetType)
    {
        Resources resources = new Resources();

        resources.food = Random.Range(5, 10);
        resources.energy = Random.Range(5, 15);
        resources.minerals = Random.Range(5,15);
        resources.research = Random.Range(5, 10);

        return resources;
    }
}
