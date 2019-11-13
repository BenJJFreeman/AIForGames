using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemGenerator
{


    public static GameObject CreateStarSystem(int planetNumber,int systemId,out int newPlanetNumber,int seed)
    {
        GameObject starSystem = new GameObject("Star System");

        starSystem.AddComponent<SystemInfo>();

        // star

        GameObject star = StarGenerator.GenerateStar();
        star.transform.parent = starSystem.transform;
        float starSize = Random.Range(10, 15);
        star.transform.localScale = new Vector3(starSize, starSize, starSize);

        starSystem.GetComponent<SystemInfo>().starInfo = star.GetComponent<StarInfo>();
        starSystem.GetComponent<SystemInfo>().systemNumber = systemId;

        int starTemp = star.GetComponent<StarInfo>().heat;

        // planets


        int numberOfPlanets = Random.Range(5, 8);
        GameObject planetContainer = new GameObject("PlanetContainer");

        for (int i = 0; i < numberOfPlanets; i++)
        {
            int size = PlanetGenerator.GetNewPlanetSize();
            float speed = Random.Range(-.5f, -.1f);

            if (Random.Range(0, 2) == 0)
            {
                speed = Mathf.Abs(speed);
            }
            
            GameObject planet = PlanetGenerator.GeneratePlanet(planetNumber, systemId, starTemp, i,size, speed);
            planetNumber += 1;
            planet.transform.localScale = new Vector3(size, size, size);
            planet.transform.parent = planetContainer.transform;
            planet.transform.localPosition = GetDirection(i + 2);
            starSystem.GetComponent<SystemInfo>().planetInfo.Add(planet.GetComponent<PlanetInfo>());
            //planet.GetComponent<PlanetInfo>().position = planet.transform.position;
        }
        newPlanetNumber = planetNumber;

        planetContainer.transform.parent = star.transform;
        planetContainer.transform.localPosition = star.transform.position;

        



        return starSystem;
    }
    public static Vector3 GetDirection(int strength)
    {

        Vector3 heading = Random.insideUnitCircle.normalized * (10 * strength);
        heading.z = heading.y;
        heading.y = 0;


        return heading;
    }
   
}
