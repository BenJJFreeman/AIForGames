using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyNavigationGrid : MonoBehaviour {

    public GalaxyControl galaxyControl;
    public static GalaxyNavigationGrid current;

    private void Awake()
    {
        current = this;
    }

    public int[] CalculateAStarPath(int pointA, int targetPoint)
    {
        if (pointA == targetPoint)
            return new int[0];


        List<Edge> pathEdgeQueue = new List<Edge>();
        List<Edge> traversedEdgeList = new List<Edge>();

        int[] route = new int[galaxyControl.systemInfo.Count];
        float[] cost = new float[route.Length];

        for (int i = 0; i < galaxyControl.systemInfo.Count; i++)
        {

            route[i] = pointA;
            cost[i] = 9999999;

        }
        cost[pointA] = 0;

        for (int i = 0; i < galaxyControl.systemInfo[pointA].connectedSystems.Count; i++)
        {
            pathEdgeQueue.Add(new Edge(pointA, galaxyControl.systemInfo[pointA].connectedSystems[i], CalculateFCost(pointA, galaxyControl.systemInfo[pointA].connectedSystems[i], targetPoint)));
        }

        SortEdgeList(pathEdgeQueue);
        bool reachedTargetPoint = false;
        while (pathEdgeQueue.Count > 0)
        {
            traversedEdgeList.Add(pathEdgeQueue[0]);

            if (cost[pathEdgeQueue[0].to] > cost[pathEdgeQueue[0].from] + pathEdgeQueue[0].cost)
            {
                route[pathEdgeQueue[0].to] = pathEdgeQueue[0].from;
                cost[pathEdgeQueue[0].to] = cost[pathEdgeQueue[0].from] + pathEdgeQueue[0].cost;

                if (pathEdgeQueue[0].to == targetPoint)
                {
                    reachedTargetPoint = true;
                }

                for (int i = 0; i < galaxyControl.systemInfo[pathEdgeQueue[0].to].connectedSystems.Count; i++)
                {
                    if (reachedTargetPoint)
                    {
                        break;
                    }
                    if (CheckEdgeContain(traversedEdgeList, pathEdgeQueue[0].to, galaxyControl.systemInfo[pathEdgeQueue[0].to].connectedSystems[i], targetPoint) && CheckEdgeContain(pathEdgeQueue, pathEdgeQueue[0].to, galaxyControl.systemInfo[pathEdgeQueue[0].to].connectedSystems[i], targetPoint))
                    {
                        pathEdgeQueue.Add(new Edge(pathEdgeQueue[0].to, galaxyControl.systemInfo[pathEdgeQueue[0].to].connectedSystems[i], CalculateFCost(pathEdgeQueue[0].to, galaxyControl.systemInfo[pathEdgeQueue[0].to].connectedSystems[i], targetPoint)));
                    }
                }
            }


            pathEdgeQueue.RemoveAt(0);
            SortEdgeList(pathEdgeQueue);


        }



        return GetPath(route, pointA, targetPoint);
    }
    public int[] GetPath(int[] route, int startPoint, int targetPoint)
    {
        List<int> f = new List<int>();

        int currentNum = targetPoint;
        f.Add(currentNum);
        while (f.Contains(startPoint) == false)
        {
            f.Add(route[currentNum]);

            currentNum = route[currentNum];
        }

        return f.ToArray();
    }
    public bool CheckEdgeContain(List<Edge> traversedEdgeList, int a, int b, int targetPoint)
    {
        Edge edge = new Edge(a, b, CalculateFCost(a, b, targetPoint));

        if (traversedEdgeList.Contains(edge))
        {

            return false;
        }

        edge = new Edge(b, a, CalculateFCost(a, b, targetPoint));
        if (traversedEdgeList.Contains(edge))
        {
            return false;
        }
        return true;
    }
    public void SortEdgeList(List<Edge> pathEdgeQueue)
    {
        if (pathEdgeQueue.Count > 0)
        {
            pathEdgeQueue.Sort(delegate (Edge a, Edge b) {
                return (a.cost).CompareTo(b.cost);
            });

        }

    }
    public float CalculateFCost(int pointA, int pointB, int targetPoint)
    {
        return CalculateGCost(pointA, pointB) + CalculateHCost(pointB, targetPoint);
    }
    public float CalculateGCost(int pointA, int pointB)
    {
        return Vector3.Distance(galaxyControl.systemInfo[pointA].transform.position, galaxyControl.systemInfo[pointB].transform.position);
    }
    public float CalculateHCost(int pointB, int targetPoint)
    {
        return Vector3.Distance(galaxyControl.systemInfo[pointB].transform.position, galaxyControl.systemInfo[targetPoint].transform.position);
    }


}
public class Edge
{
    public int from, to;
    public float cost;
    public Edge(int _from, int _to, float _cost)
    {
        from = _from;
        to = _to;
        cost = _cost;
    }
}