using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyGenerator {


    public static GameObject CreateGalaxy(int seed, int numberOfSystems)
    {

        GameObject galaxy = new GameObject("Galaxy");
        galaxy.AddComponent<GalaxyControl>();
        galaxy.AddComponent<GalaxyNavigationGrid>();
        galaxy.GetComponent<GalaxyNavigationGrid>().galaxyControl = galaxy.GetComponent<GalaxyControl>();

        int planetNumber = 0;
        // systems
        Vector3[] points = GetRandomNonOverlappingPoints(numberOfSystems, 500, 200);

        for (int i = 0; i < numberOfSystems; i++)
        {
            
            GameObject starSystem = StarSystemGenerator.CreateStarSystem(planetNumber,i,out planetNumber,seed);

            starSystem.transform.position = points[i];
            starSystem.transform.parent = galaxy.transform;
            galaxy.GetComponent<GalaxyControl>().systemInfo.Add(starSystem.GetComponent<SystemInfo>());
        }



        SystemInfo[] systemInfos = new SystemInfo[numberOfSystems];


        for(int i = 0;i< systemInfos.Length; i++)
        {
            systemInfos[i] = galaxy.transform.GetChild(i).GetComponent<SystemInfo>();
        }

        for (int i = 0; i < systemInfos.Length; i++)
        {
            for (int j = 0; j < systemInfos[i].planetInfo.Count; j++)
            {
                galaxy.GetComponent<GalaxyControl>().planetInfos.Add(systemInfos[i].planetInfo[j]);
            }
        }


        ConnectSystems(systemInfos);


        return galaxy;
    }
    public static void ConnectSystems(SystemInfo[] systemInfos)
    {
        systemInfos[0].connectedToZero = true;
        for (int i = 0; i < systemInfos.Length; i++)
        {
            if (systemInfos[i].connectedToZero)
            {
                for (int j = 0; j < systemInfos.Length; j++)
                {
                    if (i != j)
                    {
                        if (systemInfos[j].connectedToZero != true)
                        {
                            if (Vector3.Distance(systemInfos[i].transform.position, systemInfos[j].transform.position) < 250)
                            {
                                systemInfos[i].connectedSystems.Add(j);
                                systemInfos[j].connectedSystems.Add(i);
                                systemInfos[j].connectedToZero = true;
                            }
                        }
                    }
                }
            }
        }

        
        for (int i = 0; i < systemInfos.Length; i++)
        {
            if (systemInfos[i].connectedToZero == false)
            {
                float closest = 99999999;
                int nearest = 0;
                for (int j = 0; j < systemInfos.Length; j++)
                {
                    if (i != j)
                    {
                        if (systemInfos[j].connectedToZero == true)
                        {
                            if (Vector3.Distance(systemInfos[i].transform.position, systemInfos[j].transform.position) < closest)
                            {
                                nearest = j;
                                closest = Vector3.Distance(systemInfos[i].transform.position, systemInfos[j].transform.position);
                            }
                        }
                    }
                }
                systemInfos[i].connectedSystems.Add(nearest);
                systemInfos[nearest].connectedSystems.Add(i);
                systemInfos[i].connectedToZero = true;
            }
        }


        for (int i = 0; i < systemInfos.Length; i++)
        {
            for (int j = 0; j < systemInfos.Length; j++)
            {
                if (i != j)
                {
                    if (systemInfos[i].connectedSystems.Contains(j) == false)
                    {
                        if (Vector3.Distance(systemInfos[i].transform.position, systemInfos[j].transform.position) < 400)
                        {

                            systemInfos[i].connectedSystems.Add(j);
                            systemInfos[j].connectedSystems.Add( i);

                        }
                    }
                }
            }
        }



    }
    public static Vector3[] GetRandomNonOverlappingPoints(int numberOfPoints,int radius,int overlapRadius)
    {
        Vector3[] points = new Vector3[numberOfPoints];
        points[0] = Random.insideUnitCircle * radius;
        points[0].z = points[0].y;
        points[0].y = 0;

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = GetNewRandomPoint(points, radius, overlapRadius);
        }

        return points;
    }
    public static bool CheckOverlap(Vector3[] points, Vector3 tempPoint,int overlapRadius)
    {
        for (int i = 0; i < points.Length; i++)
        {
            if(Vector3.Distance(points[i],tempPoint) <= overlapRadius)
            {
                return false;
            }
        }
        return true;
    }
    public static Vector3 GetNewRandomPoint(Vector3[] points, int radius, int overlapRadius)
    {
        Vector3 tempPoint = Random.insideUnitCircle * radius;
        tempPoint.z = tempPoint.y;
        tempPoint.y = 0;

        if (CheckOverlap(points, tempPoint, overlapRadius)){
            return tempPoint;
        }
        else
        {
            return GetNewRandomPoint(points, radius, overlapRadius);
        }        
    }
    

}
