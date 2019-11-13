using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Civilisation {

    public Main main;
    public int civilisationId;
    public CivilisationBrain civilisationBrain;

    public List<int> planetsUndeControl = new List<int>();
    public List<int> knownPlanets = new List<int>();
    public List<int> knownSystems = new List<int>();


    //public List<SystemInfo> systemLists = new List<SystemInfo>();
    //public List<PlanetInfo> planetLists = new List<PlanetInfo>();

    public CivilisationInfo civilisationInfo;

    public List<BuildingName> buildings = new List<BuildingName>();

    public List<GameObject> colonyShips = new List<GameObject>();
    public List<GameObject> ships = new List<GameObject>();

    public List<int> colonisationTargets = new List<int>();

    int shipCount;
    int[] path = new int[0];
    public Civilisation(Main _main,int _civilisationId,PlanetInfo _planetInfo,Population _population)
    {
        main = _main;
        civilisationId = _civilisationId;
        knownSystems.Add(_planetInfo.systemId);
        civilisationInfo = new CivilisationInfo();

        civilisationInfo.researchSpeed = 5;
        civilisationInfo.buildSpeed = 5;       

        civilisationInfo.energyGatheringSpeed = 5;
        civilisationInfo.researchGatheringSpeed = 5;
        civilisationInfo.mineralGatheringSpeed = 5;
        civilisationInfo.foodGatheringSpeed = 5;


        civilisationInfo.resources = new Resources();

        civilisationInfo.resources.food = 100;
        civilisationInfo.resources.minerals = 100;
        civilisationInfo.resources.energy = 100;
        civilisationInfo.resources.research = 100;


        civilisationInfo.populationCount = (_population.populationSize / 10);
        civilisationInfo.maxPop = _planetInfo.size * 10;
        civilisationInfo.type = _planetInfo.type;
        //civilisationInfo.traits = _population.pop[_population.evolvingPop].genes;

        civilisationInfo.focusValues = _population.pop[_population.evolvingPop].genes;

        _planetInfo.controllingCivilsation = civilisationId;
        planetsUndeControl.Add(_planetInfo.planetId);
        //systemLists.Add(_systemInfo);
       // planetLists.Add(_planetInfo);

       // planetLists[0].civilisations.Add(this);

        civilisationBrain = new CivilisationBrain();
        civilisationBrain.civilisation = this;
        civilisationBrain.goal = new NoGoal();
        civilisationBrain.goal.Activate(civilisationBrain);
        civilisationBrain.researchingTechnology = new NoResearch();
        civilisationBrain.currentBuildingObject = new NoBuilding();
        civilisationBrain.civilisationInfo = civilisationInfo;

        UpdateKnownPlanets();
    }

    public void UpdateCivilisation(bool gather)
    {
        CalculateResourceChange();
        civilisationBrain.UpdateBrain();

        if (gather)
        {
            Resources res = GetAvailableResources();

            civilisationInfo.resources.energy += res.energy * civilisationInfo.energyGatheringSpeed;
            civilisationInfo.resources.minerals += res.minerals * civilisationInfo.mineralGatheringSpeed;
            civilisationInfo.resources.food += res.food * civilisationInfo.foodGatheringSpeed;
            civilisationInfo.resources.research += res.research * civilisationInfo.researchGatheringSpeed;
        }

        civilisationInfo.resources.food -= civilisationInfo.populationCount / 10;
        if (civilisationInfo.resources.food < 0)
        {
            civilisationInfo.resources.food = 0;
        }
        else if(civilisationInfo.resources.food > 500)
        {
            if (civilisationInfo.populationCount < civilisationInfo.maxPop)
            {
                civilisationInfo.populationCount++;
                civilisationInfo.resources.food -= 500;
            }
        }




    }
    Resources GetAvailableResources()
    {
        Resources res = new Resources();


        for(int i = 0;i < main.GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < main.GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if (main.GalaxyControl.systemInfo[i].planetInfo[j].controllingCivilsation == civilisationId)
                {
                    res.energy += main.GalaxyControl.systemInfo[i].planetInfo[j].resources.energy;
                    res.minerals += main.GalaxyControl.systemInfo[i].planetInfo[j].resources.minerals;
                    res.food += main.GalaxyControl.systemInfo[i].planetInfo[j].resources.food;
                    res.research += main.GalaxyControl.systemInfo[i].planetInfo[j].resources.research;

                }
            }

        }

       


        return res;
    }
    public void CalculateResourceChange()
    {

        Resources res = GetAvailableResources();


        civilisationInfo.energyChange = res.energy * civilisationInfo.energyGatheringSpeed;
        civilisationInfo.mineralChange = res.minerals * civilisationInfo.mineralGatheringSpeed;
        civilisationInfo.foodChange = res.food * civilisationInfo.foodGatheringSpeed;
        civilisationInfo.researchChange = res.research * civilisationInfo.researchGatheringSpeed;


        civilisationInfo.energyChange -= (0)*5;
        if (civilisationBrain.currentBuildingObject.ToString() != new NoBuilding().ToString()) {
            civilisationInfo.mineralChange -= (civilisationInfo.buildSpeed)*5;
        }
        civilisationInfo.foodChange -= (civilisationInfo.populationCount / 10) * 5;

        if (civilisationBrain.researchingTechnology.ToString() != new NoResearch().ToString())
        {
            civilisationInfo.researchChange -= (civilisationInfo.researchSpeed) *5;
        }
    }
    void ImproveViewRange()
    {
        civilisationInfo.viewRange += 1;
        UpdateKnownPlanets();
    }
    void UpdateKnownPlanets()
    {
        switch (civilisationInfo.viewRange)
        {
            case 0:
                for (int i = 0; i < main.GalaxyControl.systemInfo[knownSystems[0]].planetInfo.Count; i++)
                {
                    knownPlanets.Add(main.GalaxyControl.systemInfo[knownSystems[0]].planetInfo[i].planetId);
                }
                break;
            case 1:
                for (int i = 0; i < planetsUndeControl.Count; i++)
                {
                    for (int s = 0; s < main.GalaxyControl.systemInfo[main.GalaxyControl.planetInfos[planetsUndeControl[i]].systemId].connectedSystems.Count; s++)
                    {
                        knownSystems.Add(main.GalaxyControl.systemInfo[main.GalaxyControl.planetInfos[planetsUndeControl[i]].systemId].connectedSystems[s]);

                        for (int p = 0; p < main.GalaxyControl.systemInfo[main.GalaxyControl.systemInfo[main.GalaxyControl.planetInfos[planetsUndeControl[i]].systemId].connectedSystems[s]].planetInfo.Count; p++)
                        {
                            knownPlanets.Add(main.GalaxyControl.systemInfo[main.GalaxyControl.systemInfo[main.GalaxyControl.planetInfos[planetsUndeControl[i]].systemId].connectedSystems[s]].planetInfo[p].planetId);
                        }
                    }                
                }
                break;
            case 2:
                for(int i = 0; i < main.GalaxyControl.systemInfo.Count; i++)
                {
                    if (!knownSystems.Contains(i))
                    {
                        knownSystems.Add(i);
                    }
                }
                for (int i = 0; i < main.GalaxyControl.planetInfos.Count; i++)
                {
                    if (!knownPlanets.Contains(i))
                    {
                        knownPlanets.Add(i);
                    }
                }
                break;
        }
    }    
    public void UpdateBuildingList(Building building)
    {
        if (building.buildingType == BuildingType.building)
        {
            buildings.Add(building.name);

            switch (building.name)
            {
                case BuildingName.Satellilte:
                    ImproveViewRange();
                    break;
                case BuildingName.ImprovedSatellite:
                    ImproveViewRange();
                    break;
            }
        }
        else
        {
            GameObject ship = main.CreateShip((building.buildingType == BuildingType.ship ? false : true));

            ship.GetComponent<Ship>().shipID = shipCount;
            shipCount++;
            if (building.buildingType == BuildingType.ship)
            {
                ships.Add(ship);
                ship.GetComponent<Ship>().SetUp(civilisationId);
            }
            else
            {
                colonyShips.Add(ship);
                ship.GetComponent<Ship>().SetUpColonyShip(planetsUndeControl[0], civilisationId);
            }
            ship.transform.position = PlanetPos(planetsUndeControl[0]) + (Vector3.up * 5);
        }     
    }
    public int GetNearestSystem(Vector3 _location)
    {
        float dist = 999999999;
        int nearSyst = 0;
        for(int i =0;i< main.GalaxyControl.systemInfo.Count;i++)
        {
            if (Vector3.Distance(_location, main.GalaxyControl.systemInfo[i].transform.position) < dist)
            {
                dist = Vector3.Distance(_location, main.GalaxyControl.systemInfo[i].transform.position);
                nearSyst = i;
            }
        }
        return nearSyst;
    }
    public void UpdateShips(float gameSpeed)
    {
       
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].GetComponent<Ship>().UpdateShip(gameSpeed, PlanetPos(planetsUndeControl[0]),GetPlanetInfo(planetsUndeControl[0]).heading, GetPlanetInfo(planetsUndeControl[0]).speed, GetPlanetInfo(planetsUndeControl[0]).velocity);
        }
        for (int i = 0; i < colonyShips.Count; i++)
        {
            if (colonisationTargets.Count > i)
            {

                path = GalaxyNavigationGrid.current.CalculateAStarPath(GetNearestSystem(colonyShips[i].transform.position), main.GalaxyControl.planetInfos[colonisationTargets[i]].systemId);

                if (path.Length > 0)
                {
                    if (colonyShips[i].GetComponent<Ship>().UpdateColonyShip(gameSpeed, main.GalaxyControl.systemInfo[path[0]].transform.position, Vector3.zero, 0, Vector3.zero))
                    {
                        ColoniseTargetPlanet(i);
                    }
                }
                else
                {
                    if (colonyShips[i].GetComponent<Ship>().UpdateColonyShip(gameSpeed, PlanetPos(colonisationTargets[i]), GetPlanetInfo(colonisationTargets[i]).heading, GetPlanetInfo(colonisationTargets[i]).speed, GetPlanetInfo(colonisationTargets[i]).velocity))
                    {
                        ColoniseTargetPlanet(i);
                    }
                }
            }
            else
            {
                colonyShips[i].GetComponent<Ship>().UpdateShip(gameSpeed, PlanetPos(planetsUndeControl[0]), GetPlanetInfo(planetsUndeControl[0]).heading, GetPlanetInfo(planetsUndeControl[0]).speed, GetPlanetInfo(planetsUndeControl[0]).velocity);
            }
        }
    }
    public void ColoniseTargetPlanet(int element)
    {
        GetPlanetInfo(colonisationTargets[element]).controllingCivilsation = civilisationId;
        planetsUndeControl.Add(colonisationTargets[element]);
        civilisationInfo.maxPop += GetPlanetInfo(colonisationTargets[element]).size * 10;

        colonisationTargets.RemoveAt(element);
        ShipDestroyed(colonyShips[element].GetComponent<Ship>().shipID);
    }
    public void ShipDestroyed(int shipID)
    {
        for (int i = 0; i < ships.Count; i++)
        {
            if(ships[i].GetComponent<Ship>().shipID == shipID)
            {
                ships[i].GetComponent<Ship>().DestroyShip();
                ships.Remove(ships[i]);
            }
        }
        for (int i = 0; i < colonyShips.Count; i++)
        {
            if (colonyShips[i].GetComponent<Ship>().shipID == shipID)
            {
                colonyShips[i].GetComponent<Ship>().DestroyShip();
                colonyShips.Remove(colonyShips[i]);
            }
        }
    }
    public Vector3 PlanetPos(int planetId)
    {
        Vector3 pos = new Vector3();
        for (int i = 0; i < main.GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < main.GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if (main.GalaxyControl.systemInfo[i].planetInfo[j].planetId == planetId)
                {
                    return main.GalaxyControl.systemInfo[i].planetInfo[j].transform.position;
                }
            }
        }
        return pos;
    }
    public PlanetInfo GetPlanetInfo(int planetId)
    {
        for (int i = 0; i < main.GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < main.GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if (main.GalaxyControl.systemInfo[i].planetInfo[j].planetId == planetId)
                {
                    return main.GalaxyControl.systemInfo[i].planetInfo[j];
                }
            }
        }
        return null;
    }

}
[System.Serializable]
public class CivilisationInfo
{
    public int civilisationAge;

    public int maxPop;
    public int populationCount;

    public PlanetType type;

    //public float[] traits;

    public int targetMilitaryPower;
    public int militaryPower;

    public Resources resources;

    public int energyGatheringSpeed;
    public int foodGatheringSpeed;
    public int mineralGatheringSpeed;
    public int researchGatheringSpeed;

    public int researchSpeed;
    public int buildSpeed;


    public bool taskComplete;
    public float taskCurrentProgress;

    public float[] focusValues;

    public int viewRange;


    public int energyChange;
    public int foodChange;
    public int mineralChange;
    public int researchChange;

    public List<int> civilisationOpinion;

    public float habitabilityBonus;
}
[System.Serializable]
public class Resources
{
    public int food;
    public int minerals;
    public int energy;
    public int research;

}
[System.Serializable]
public enum Focus { expansion, dominace, prosperity, harmony, diplomacy, discovery }