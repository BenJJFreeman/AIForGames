using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CivilisationBrain  {

    public Civilisation civilisation;

    public List<string> researchedTechnology = new List<string>();
   // public List<string> builtObjects = new List<string>();

    public Technology researchingTechnology;

    public Goal goal;

    public BuildableObject currentBuildingObject;

    public CivilisationInfo civilisationInfo;

    public float[] goalDesire;

    //public PlanetInfo colonisationTarget;
    


    public void UpdateBrain()
    {

        if (goal.ToString() == "NoGoal")
            GetNewGoal();

        int status = goal.Process();

        if (status < 0)
        {
            CurrentGoalReached();
        }
        else
        {

            if (researchingTechnology != null)
            {
                if (researchingTechnology.ToString() != "NoResearch")
                    if (civilisationInfo.resources.research >= civilisationInfo.researchSpeed)
                    {
                        civilisationInfo.resources.research -= civilisationInfo.researchSpeed;

                        if (researchingTechnology.Process(civilisationInfo.researchSpeed))
                        {
                            ResearchingTechComplete();
                        }
                    }
            }

            if (currentBuildingObject != null)
            {
                if (currentBuildingObject.ToString() != "NoBuilding")
                    if (civilisationInfo.resources.minerals >= civilisationInfo.buildSpeed)
                    {
                        civilisationInfo.resources.minerals -= civilisationInfo.buildSpeed;

                        if (currentBuildingObject.Process(civilisationInfo.buildSpeed))
                        {
                            CurrentBuildingObjectComplete();
                        }
                    }
            }

            if (civilisationInfo.taskCurrentProgress > 0)
            {
                civilisationInfo.taskCurrentProgress -= 1;
                if (civilisationInfo.taskCurrentProgress <= 0)
                {
                    civilisationInfo.taskComplete = true;
                }
                Debug.Log(civilisationInfo.taskCurrentProgress);
            }
            else
            {
                civilisationInfo.taskComplete = true;
            }

        }


    }

    void GetNewGoal()
    {
        goal = GetHighestDesiredGoal();
    }
    void GetNewTechnology()
    {

    }
    void CurrentGoalReached()
    {
        Debug.Log("Goal Complete");

        if(civilisationInfo.civilisationAge < 2)
        {
            civilisationInfo.civilisationAge++;
        }

        GetNewGoal();
    }

    void ResearchingTechComplete()
    {
        Debug.Log(researchingTechnology.ToString());
        researchedTechnology.Add(researchingTechnology.ToString());


        AddResultToCivilisationInfo(researchingTechnology.result);

        Debug.Log("Tech research");
        researchingTechnology = new NoResearch();

    }
    void CurrentBuildingObjectComplete()
    {
        AddResultToCivilisationInfo(currentBuildingObject.building.result);

        //builtObjects.Add(currentBuildingObject.ToString());

        civilisation.UpdateBuildingList(currentBuildingObject.building);
        //civilisation.buildings.Add(currentBuildingObject.building);

        Debug.Log("Building Complete");
        currentBuildingObject = new NoBuilding();

    }    
    void AddResultToCivilisationInfo(Result result)
    {

        for(int i = 0;i< result.resultParts.Count; i++)
        {
            switch (result.resultParts[i].resultType)
            {
                case ResultType.militaryPower:
                    civilisationInfo.militaryPower += result.resultParts[i].amount;
                    break;
                case ResultType.energyProduction:
                    civilisationInfo.energyGatheringSpeed += result.resultParts[i].amount;
                    break;
                case ResultType.foodProduction:
                    civilisationInfo.foodGatheringSpeed += result.resultParts[i].amount;
                    break;
                case ResultType.researchProduction:
                    civilisationInfo.researchGatheringSpeed += result.resultParts[i].amount;
                    break;
                case ResultType.mineralProduction:
                    civilisationInfo.mineralGatheringSpeed += result.resultParts[i].amount;
                    break;
                case ResultType.researchSpeed:
                    civilisationInfo.researchSpeed += result.resultParts[i].amount;
                    break;
                case ResultType.buildSpeed:
                    civilisationInfo.buildSpeed += result.resultParts[i].amount;
                    break;
            }
        }


        /*
        civilisationInfo.buildSpeed += result.buildSpeed;
        civilisationInfo.researchSpeed += result.researchSpeed;       

        for(int i = 0; i < result.resourceGatheringSpeed.Length; i++)
        {
            civilisationInfo.resourceGatheringSpeed[i] += result.resourceGatheringSpeed[i];
        }
        */

    }
    public void StartNewTask(float max)
    {
        //if(civilisationInfo.taskCurrentProgress <= 0)
        //{
            civilisationInfo.taskCurrentProgress = max;
            civilisationInfo.taskComplete = false;
          
        //}



 
    }
    
    public float GetDesire(Goal g)
    {
        float d = 0;
        d += MineralCostValue(g);
        d += ResearchCostValue(g);
        d += TimeValue(g);
        d += OutComeValue(g);
        d += NeedValue(g);
        d += FocusAllignment(g);
        d = d / 6;
        return d;
    }
    public float MineralCostValue(Goal g)
    {

        float c = g.GetMineralCost();
        float mid = 0;

        switch (civilisationInfo.civilisationAge)
        {
            case 0:
                mid = 50;
                break;
            case 1:
                mid = 200;
                break;
            case 2:
                mid = 500;
                break;
        }
        c = Mathf.InverseLerp(mid * 2, 0, c);
        return c;
    }
    public float ResearchCostValue(Goal g)
    {
        float c = g.GetResearchCost();
        float mid = 0;
        switch (civilisationInfo.civilisationAge)
        {
            case 0:
                mid = 100;
                break;
            case 1:
                mid = 250;
                break;
            case 2:
                mid = 500;
                break;
        }
        c = Mathf.InverseLerp(mid * 2, 0, c);
        return c;
    }
    public float TimeValue(Goal g)
    {
        float t = g.GetLength();
        float mid = 0;

        switch (civilisationInfo.civilisationAge)
        {
            case 0:
                mid = 5;
                break;
            case 1:
                mid = 20;
                break;
            case 2:
                mid = 40;
                break;
        }
        t = Mathf.InverseLerp(mid *2, 0, t); 
        return t;
    }
    public float OutComeValue(Goal g)
    {
        Result r = g.GetResult();
        int value = 0;
        for(int i = 0; i < r.resultParts.Count; i++)
        {
            value += r.resultParts[i].amount;
        }
        float t = Mathf.InverseLerp(150, 0, value);
        return t;
    }
    public float NeedValue(Goal g)
    {

        switch (g.ToString())
        {
            case "EstablishColony":
                return Mathf.InverseLerp(0,civilisationInfo.maxPop,civilisationInfo.populationCount);
            case "ImproveEnergyProduction":
                return Mathf.InverseLerp(100, 0, civilisationInfo.energyChange);
            case "ImproveFoodProduction":
                return Mathf.InverseLerp(100, 0, civilisationInfo.foodChange);
            case "ImproveMineralProduction":
                return Mathf.InverseLerp(100, 0, civilisationInfo.mineralChange);
            case "ImproveResearchProduction":
                return Mathf.InverseLerp(100, 0, civilisationInfo.researchChange);

            default:
                return .5f;
        }
    }
    public float FocusAllignment(Goal g)
    {
        int f = (int)g.focus;


        List<int> focusOrder =  new List<int>();
        List<float> focusValueOrder =  new List<float>();

        for (int j = 0; j < civilisationInfo.focusValues.Length; j++)
        {
            focusValueOrder.Add(civilisationInfo.focusValues[j]);
        }


        for (int j = 0; j < focusValueOrder.Count; j++)
        {
            float highestValue = 0;
            int fI = 0;
            for (int i = 0; i < focusValueOrder.Count; i++)
            {
                if (focusValueOrder[i] > highestValue)
                {
                    if (!focusOrder.Contains(i))
                    {
                        fI = i;

                        highestValue = civilisationInfo.focusValues[i];
                    }
                }
            }
            focusOrder.Add(fI);
        }
        float v = 0;
        for(int i = 0; i < focusOrder.Count; i++)
        {
            if(f == focusOrder[i])
            {
                v = i;
                break;
            }
        }

        float d = Mathf.Lerp(focusOrder.Count, 0, v);

        Debug.Log(v / focusOrder.Count);
        return v/focusOrder.Count;
    }
    public Goal GetHighestDesiredGoal()
    {
        float highest = 0.025f;

        List<Goal> potentialGoals = GetPotentialGoals();
        int highestGoal = 0;

        goalDesire = new float[potentialGoals.Count];

        for(int i = 0; i < potentialGoals.Count;i++)
        {
            float d = GetDesire(potentialGoals[i]);
            goalDesire[i] = d;
            if(d > highest)
            {
                highest = d;
                highestGoal = i;
            }
            else
            {

            }
        }

        if (potentialGoals.Count == 0)
            return new NoGoal();
        

        return potentialGoals[highestGoal];
    }
    public List<Goal> GetPotentialGoals()
    {

        List<Goal> allGoals = GetAllGoals();
        List<Goal> g = new List<Goal>();


        for (int i = 0; i < allGoals.Count; i++)
        {
            if (allGoals[i].age == civilisationInfo.civilisationAge)
                g.Add(allGoals[i]);
        }


        return g;

    }
    public List<Goal> GetAllGoals()
    {

        List<Goal> allGoals = new List<Goal>();

        // age 0 - Primitive
        
        allGoals.Add(new DevelopCivilisation(this));
        allGoals.Add(new EstablishCulture(this));
        allGoals.Add(new StrengthenPeople(this));
        allGoals.Add(new ExploreLands(this));
        allGoals.Add(new UnitePeople(this));
        allGoals.Add(new EstablishBorders(this));



        //allGoals.Add(new MakeSettlements(this));
        //allGoals.Add(new BuildUpMilita(this));
        // age 1 - Industrial

        allGoals.Add(new SetUpGlobalCommunity(this));
        allGoals.Add(new BirthAnEmpire(this));
        allGoals.Add(new Colonisation(this));
        allGoals.Add(new ReligousSprouting(this));
        allGoals.Add(new TradePriority(this));
        allGoals.Add(new FreeThought(this));

        // age 2 - Space Age
        
        if (CheckColonisationPossible())
        {
            allGoals.Add(new EstablishColony(this));
        }
        
      //  allGoals.Add(new ImproveEconomy(this));
        if (!civilisation.buildings.Contains(BuildingName.PowerPlant))
        {
            allGoals.Add(new ImproveEnergyProduction(this));
        }
        if (!civilisation.buildings.Contains(BuildingName.Hydroponics))
        {
            allGoals.Add(new ImproveFoodProduction(this));
        }
        if (!civilisation.buildings.Contains(BuildingName.Mine))
        {
            allGoals.Add(new ImproveMineralProduction(this));
        }
        if (!civilisation.buildings.Contains(BuildingName.Laboratory))
        {
            allGoals.Add(new ImproveResearchProduction(this));
        }
        if (CheckConquerPossible())
        {
            allGoals.Add(new ConquerPlanet(this));
        }
        //allGoals.Add(new FormFederation(this));
        if (!civilisation.buildings.Contains(BuildingName.Satellilte))
        {
            allGoals.Add(new BuildSatelliltes(this));
        }
        if (civilisation.buildings.Contains(BuildingName.Satellilte))
        {
            allGoals.Add(new ImproveSatelliltes(this));
        }
        if (!civilisation.buildings.Contains(BuildingName.MegaCity))
        {
            allGoals.Add(new BuildMegaCities(this));
        }
        if (!civilisation.buildings.Contains(BuildingName.GalacticMarket))
        {
            allGoals.Add(new BuildGalacticMarket(this));
        }
        if (!civilisation.buildings.Contains(BuildingName.Embassy))
        {
            allGoals.Add(new BuildEmbassies(this));
        }

        // allGoals.Add(new BionicDevelopment(this));
        // allGoals.Add(new Ascend(this));
        if (!civilisation.buildings.Contains(BuildingName.PlanetShields))
        {
            allGoals.Add(new PlanetaryDefence(this));
        }
        
        allGoals.Add(new BuildUpBattleShips(this));
        
        return allGoals;

    }
    public bool CheckConquerPossible()
    {
        return false;
    }
    public bool CheckColonisationPossible()
    {
        SetKnownPlanets();
        int tempId = GetBestColonisationPlanet();
        if (tempId == -1)
        {
            return false;
        }

        return true;
    }
    public bool GetColonisationTarget()
    {
        SetKnownPlanets();
        int tempId = GetBestColonisationPlanet();
        if (tempId == -1)
        {
            return false;
        }
        else
            civilisation.colonisationTargets.Add(tempId);
            return true;
    }
    /*
    PlanetInfo GetNewPlanet(SystemInfo systemInfo,int count)
    {
        int randomNumber = Random.Range(0, systemInfo.planetInfo.Count);
        if (systemInfo.planetInfo[randomNumber].canSpawnLife == false && systemInfo.planetInfo[randomNumber].civilisations.Count == 0)
        {
            return systemInfo.planetInfo[randomNumber];
        }
        count++;

        if(count >= systemInfo.planetInfo.Count)
        {
            return null;
        }
        return GetNewPlanet(systemInfo,count);
    }
    */
    int GetBestColonisationPlanet()
    {
        int element = -1;
        float highestHab = 40;
        float v = 0;
        for (int i = 0; i < civilisation.knownPlanets.Count; i++)
        {
            if (civilisation.main.GalaxyControl.planetInfos[civilisation.knownPlanets[i]].controllingCivilsation == -1 /*&& not being colonised by another civ */)
            {
                if (!civilisation.colonisationTargets.Contains(civilisation.main.GalaxyControl.planetInfos[civilisation.knownPlanets[i]].planetId))
                {
                    v = civilisation.main.GalaxyControl.planetInfos[civilisation.knownPlanets[i]].CalculateHabilitability(civilisationInfo.type);
                    v += civilisation.civilisationInfo.habitabilityBonus;
                    if (v > highestHab)
                    {
                        highestHab = v;
                        element = civilisation.knownPlanets[i];
                    }
                }
            }           

        }
        return element;
    }
    int GetNewPlanet()
    {   


        for (int i = 0; i < civilisation.main.GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < civilisation.main.GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if (civilisation.knownPlanets.Contains(civilisation.main.GalaxyControl.systemInfo[i].planetInfo[j].planetId))
                {
                    if (civilisation.main.GalaxyControl.systemInfo[i].planetInfo[j].controllingCivilsation == -1 /*&& not being colonised by another civ */)
                    {
                        if (!civilisation.colonisationTargets.Contains(civilisation.main.GalaxyControl.systemInfo[i].planetInfo[j].planetId))
                        {
                            return civilisation.main.GalaxyControl.systemInfo[i].planetInfo[j].planetId;
                        }
                    }
                }
            }
        }
        return -1;
    }   
    public void SetKnownPlanets()
    {
        for (int i = 0; i < civilisation.main.GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < civilisation.main.GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if(civilisation.knownSystems.Contains(civilisation.main.GalaxyControl.systemInfo[i].planetInfo[j].systemId))
                {
                    if (!civilisation.knownPlanets.Contains(civilisation.main.GalaxyControl.systemInfo[i].planetInfo[j].planetId))
                    {
                        civilisation.knownPlanets.Add(civilisation.main.GalaxyControl.systemInfo[i].planetInfo[j].planetId);
                    }
                }
            }
        }
    }
    public void GetNewColonisationTarget()
    {
        GetColonisationTarget();
    }
}
