using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfo : MonoBehaviour{

    public StarInfo starInfo;
    public List<PlanetInfo> planetInfo = new List<PlanetInfo>();

    //public List<Civilisation> civilisations = new List<Civilisation>();

    public List<int> connectedSystems = new List<int>();
    public bool connectedToZero;
    public int systemNumber;
}
