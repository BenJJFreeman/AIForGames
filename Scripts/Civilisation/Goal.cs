using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal  {

    public CivilisationBrain civilisationBrain;
    public Focus focus;
    public bool IsActive = false;
    public int age = 0;
    public abstract void Activate(CivilisationBrain _civilisationBrain);
    public abstract int Process();
    public abstract void Terminate();
    public abstract void AddSubgoal(Goal g);
    public abstract float GetMineralCost();
    public abstract float GetResearchCost();
    public abstract float GetLength();
    public abstract Result GetResult();
}
public class CompositeGoal : Goal
{
    protected Stack<Goal> subgoals = new Stack<Goal>();

    public override void Activate(CivilisationBrain _civilisationBrain)
    {
        IsActive = true;
        civilisationBrain = _civilisationBrain;
    }

    public override void AddSubgoal(Goal g)
    {
        subgoals.Push(g);
        g.Activate(civilisationBrain);
    }
    public override int Process()
    {
        int status = 0;
        if (subgoals.Count > 0)
            status = subgoals.Peek().Process();        

        if (status == -1)
        {
            Goal g = subgoals.Pop();
            g.Terminate();
        }

        if (subgoals.Count > 0)
        {
            status = 1;
        }else status = -1;


        return status;
    }

    public override void Terminate()
    {
        IsActive = false;
    }
    public override float GetMineralCost()
    {
        Goal[] goalArray = subgoals.ToArray();

        float c = 0;

        for(int i =0; i < goalArray.Length; i++)
        {
            c += goalArray[i].GetMineralCost();
        }
        return c;
    }
    public override float GetLength()
    {
        Goal[] goalArray = subgoals.ToArray();

        float l = 0;

        for (int i = 0; i < goalArray.Length; i++)
        {
            l += goalArray[i].GetLength();
        }

        return l;
    }
    public override float GetResearchCost()
    {
        Goal[] goalArray = subgoals.ToArray();

        float r = 0;

        for (int i = 0; i < goalArray.Length; i++)
        {
            r += goalArray[i].GetResearchCost();
        }

        return r;
    }
    public override Result GetResult()
    {
        Goal[] goalArray = subgoals.ToArray();

        Result r = new Result();

        for (int i = 0; i < goalArray.Length; i++)
        {
            Result tempR = goalArray[i].GetResult();
            if (tempR != null)
            {
                for (int j = 0; j < tempR.resultParts.Count; j++)
                {
                    r.resultParts.Add(tempR.resultParts[j]);

                }
            }
        }

        return r;
    }
}

public class AtomicGoal : Goal
{
    public Result result;
    public float mineralCost;
    public float researchCost;
    public float length;   

    public override void Activate(CivilisationBrain _civilisationBrain)
    {
        IsActive = true;
        civilisationBrain = _civilisationBrain;
    }

    public override void AddSubgoal(Goal g)
    {        
       
    }

    public override int Process()
    {
        int status = 1;     

        return status;
    }

    public override void Terminate()
    {
        IsActive = false;
    }
    public override float GetMineralCost()
    {
        return mineralCost;
    }
    public override float GetLength()
    {
        return length;
    }
    public override float GetResearchCost()
    {
        return researchCost;
    }
    public override Result GetResult()
    {

        return result;
    }
}
public class NoGoal : AtomicGoal {}
// Age 0 - Primitive
// composite goals
public class DevelopCivilisation : CompositeGoal
{
    public DevelopCivilisation(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.prosperity;
        age = 0;

        AddSubgoal(new CompleteTask(civilisationBrain, 25,new Result()));

        //AddSubgoal(new BuildObject(civilisationBrain, new Settlement()));
    }
}
public class EstablishCulture : CompositeGoal
{
    public EstablishCulture(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.harmony;
        age = 0;

        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));

        //AddSubgoal(new ResearchTechnology(civilisationBrain, new Philosophy()));
    }
}
public class StrengthenPeople : CompositeGoal
{
    public StrengthenPeople(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.dominace;
        age = 0;

        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));

        //AddSubgoal(new ResearchTechnology(civilisationBrain, new MetalWepons()));
        //AddSubgoal(new ResearchTechnology(civilisationBrain, new Metal()));
       
    }
}
public class ExploreLands : CompositeGoal
{
    public ExploreLands(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 0;

        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));


        //AddSubgoal(new SendScoutToExplore(civilisationBrain));
       // AddSubgoal(new CompleteTask(civilisationBrain,10, new Result()));
        //AddSubgoal(new MakeScout(civilisationBrain));
       // AddSubgoal(new BuildObject(civilisationBrain,new Scout()));


    }
}
public class UnitePeople : CompositeGoal
{
    public UnitePeople(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.diplomacy;
        age = 0;

        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));

        //AddSubgoal(new BuildObject(civilisationBrain, new GatheringPoint()));

    }
}
public class EstablishBorders : CompositeGoal
{
    public EstablishBorders(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.expansion;
        age = 0;

        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));


        //AddSubgoal(new SendScoutToExplore(civilisationBrain));
       // AddSubgoal(new CompleteTask(civilisationBrain, 10, new Result()));
        //AddSubgoal(new MakeScout(civilisationBrain));
       // AddSubgoal(new BuildObject(civilisationBrain, new Scout()));

    }
}
/*
public class BuildUpMilita : CompositeGoal
{
    public BuildUpMilita(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        int diffMP = civilisationBrain.civilisationInfo.targetMilitaryPower - civilisationBrain.civilisationInfo.militaryPower;
        int num = (diffMP / new Militia().result.militaryPower);

        for (int i = 0; i < num; i++)
        {
            AddSubgoal(new BuildObject(_civilisationBrain, new Militia()));
        }


    }
}
*/
// atomic goals
public class BuildObject : AtomicGoal
{
    BuildableObject buildableObject;
    bool started;
    public BuildObject(CivilisationBrain _civilisationBrain, BuildableObject _buildableObject)
    {
        civilisationBrain = _civilisationBrain;
        buildableObject = _buildableObject;

        if (buildableObject.unique)
        {
            if (civilisationBrain.civilisation.buildings.Contains(buildableObject.building.name))
            {
                result = buildableObject.building.result;
                length = buildableObject.mineralCost / civilisationBrain.civilisationInfo.buildSpeed;
                researchCost = 0;
                mineralCost = buildableObject.mineralCost;
            }
        }
    }
    public override int Process()
    {
        int status = base.Process();
        //
        if (buildableObject.unique)
        {
            if (civilisationBrain.civilisation.buildings.Contains(buildableObject.building.name))
            {
                status = -1;
                return status;
            }
        }

        if (civilisationBrain.currentBuildingObject.ToString() == buildableObject.ToString())
        {
            
        }
        else
        {
            if (started)
            {
                status = -1;
            }
            else
            {
                civilisationBrain.currentBuildingObject = buildableObject;
                started = true;
            }
        }
        //status = -1;

        return status;
    }
}
public class ResearchTechnology : AtomicGoal
{
    Technology technology;
    public ResearchTechnology(CivilisationBrain _civilisationBrain, Technology _technology)
    {
        civilisationBrain = _civilisationBrain;
        technology = _technology;

        result = technology.result;
        length = technology.researchCost / civilisationBrain.civilisationInfo.researchSpeed;
        researchCost = technology.researchCost;
        mineralCost = 0;
    }
    public override int Process()
    {
        int status = base.Process();
        //

        if (civilisationBrain.researchedTechnology.Contains(technology.ToString()))
        {
            status = -1;
        }

        if (civilisationBrain.researchingTechnology.ToString() == technology.ToString())
        {

        }
        else
        {
            civilisationBrain.researchingTechnology = technology;
        }
        //status = -1;

        return status;
    }
}
public class CompleteTask : AtomicGoal
{
    public CompleteTask(CivilisationBrain _civilisationBrain,int _taskLength, Result _result)
    {
        civilisationBrain = _civilisationBrain;

        result = _result;
        length = _taskLength;
        researchCost = 0;
        mineralCost = 0;
    }
    public override int Process()
    {
        int status = base.Process();
        //


        if (civilisationBrain.civilisationInfo.taskComplete)
        {
            status = -1;
            civilisationBrain.civilisationInfo.taskComplete = false;
        }
        else
        {
            if (civilisationBrain.civilisationInfo.taskCurrentProgress <= 0)
            {
                civilisationBrain.StartNewTask(length);
            }
            else if (civilisationBrain.civilisationInfo.taskCurrentProgress > 0)
            {

            }
        }

        //status = -1;

        return status;
    }
}
//public class MakeSettlements : BuildObject { public MakeSettlements(CivilisationBrain _civilisationBrain) : base(_civilisationBrain, new Settlement()) { } }
//public class MakeScout : BuildObject { public MakeScout(CivilisationBrain _civilisationBrain) : base(_civilisationBrain, new Scout()) { } }
//public class SendScoutToExplore : AtomicGoal
//{
//    public SendScoutToExplore(CivilisationBrain _civilisationBrain)
//    {
//        civilisationBrain = _civilisationBrain;

//        result = new Result(0, new int[4], 0, 0);
//        length = 10;
//        researchCost = 0;
//        mineralCost = 0;
//    }
//    public override int Process()
//    {
//        int status = base.Process();
//        //


//        if (civilisationBrain.civilisationInfo.taskComplete)
//        {
//            status = -1;
//        }
//        else
//        {
//            if (civilisationBrain.civilisationInfo.taskCurrentProgress <= 0)
//            {
//                civilisationBrain.StartNewTask(10);
//            }
//            else if (civilisationBrain.civilisationInfo.taskCurrentProgress > 0)
//            {

//            }
//        }



//        //status = -1;

//        return status;
//    }
//}


//public class ResearchLiterature : ResearchTechnology { public ResearchLiterature(CivilisationBrain _civilisationBrain) : base(_civilisationBrain, new Literature()) { } }
//public class ResearchMathematics : ResearchTechnology { public ResearchMathematics(CivilisationBrain _civilisationBrain) : base(_civilisationBrain, new Mathematics()) { } }

// Age 1 - Industrial

public class SetUpGlobalCommunity : CompositeGoal
{
    public SetUpGlobalCommunity(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.diplomacy;
        age = 1;
        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));
    }
}
public class BirthAnEmpire : CompositeGoal
{
    public BirthAnEmpire(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.dominace;
        age = 1;
        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));
    }
}
public class Colonisation : CompositeGoal
{
    public Colonisation(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.expansion;
        age = 1;
        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));
    }
}public class ReligousSprouting : CompositeGoal
{
    public ReligousSprouting(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.harmony;
        age = 1;
        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));
    }
}public class TradePriority : CompositeGoal
{
    public TradePriority(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.prosperity;
        age = 1;
        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));
    }
}
public class FreeThought : CompositeGoal
{
    public FreeThought(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 1;
        AddSubgoal(new CompleteTask(civilisationBrain, 25, new Result()));
    }
}

// Age 2 - SpaceAge

public class EstablishColony : CompositeGoal
{
    public EstablishColony(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;
        
        AddSubgoal(new BuildColonyShip(civilisationBrain));
        AddSubgoal(new GetNewColonisationTarget(civilisationBrain));
        if (civilisationBrain.researchedTechnology.Contains(new Spaceships().ToString()) == false)
        {
            AddSubgoal(new ResearchSpaceShip(civilisationBrain));
        }
    }
}
public class ResearchSpaceShip : CompositeGoal
{
    public ResearchSpaceShip(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;
        AddSubgoal(new ResearchTechnology(civilisationBrain, new Spaceships()));
    }
}
public class BuildColonyShip : CompositeGoal
{
    public BuildColonyShip(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.expansion;
        age = 2;
        AddSubgoal(new BuildObject(civilisationBrain,new ColonyShip()));
    }
}
public class SendColonyShipToPlanet : AtomicGoal
{
    int colonyShipID;
    int targetPlanet;
    public SendColonyShipToPlanet(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.expansion;
        age = 2;
        result = new Result();
        colonyShipID = -1;
    }
    public override int Process()
    {
        int status = base.Process();
        //
        status = -1;    

        return status;
    }
}
public class GetNewColonisationTarget : AtomicGoal
{
    public GetNewColonisationTarget(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.expansion;
        age = 2;
    }
    public override int Process()
    {
        int status = base.Process();
        //

        civilisationBrain.GetNewColonisationTarget();

        status = -1;

        return status;
    }
}
public class BuildBattleShip : CompositeGoal
{
    public BuildBattleShip(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.dominace;
        age = 2;
        AddSubgoal(new BuildObject(civilisationBrain, new BattleShip()));
    }
}
/*
public class ColonisePlanet : AtomicGoal
{
    public ColonisePlanet(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.expansion;
        age = 2;
        result = new Result(new List<ResultPart>() {new ResultPart(0, ResultType.researchProduction) });
    }
    public override int Process()
    {
        int status = base.Process();
        //

        civilisationBrain.ColoniseTargetPlanet();


        status = -1;

        return status;
    }
}
*/
public class ImproveFoodProduction : CompositeGoal
{
    public ImproveFoodProduction(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;

        AddSubgoal(new BuildObject(civilisationBrain, new Hydroponics()));
    }
}
public class ImproveMineralProduction : CompositeGoal
{
    public ImproveMineralProduction(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;

        AddSubgoal(new BuildObject(civilisationBrain, new Mine()));
    }
}
public class ImproveEnergyProduction : CompositeGoal
{
    public ImproveEnergyProduction(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;

        AddSubgoal(new BuildObject(civilisationBrain, new PowerPlant()));
    }
}
public class ImproveResearchProduction : CompositeGoal
{
    public ImproveResearchProduction(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;

        AddSubgoal(new BuildObject(civilisationBrain, new Laboratory()));
    }
}
public class ImproveEconomy : CompositeGoal
{
    public ImproveEconomy(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;

        AddSubgoal(new ImproveEnergyProduction(civilisationBrain));
        AddSubgoal(new ImproveMineralProduction(civilisationBrain));
        AddSubgoal(new ImproveFoodProduction(civilisationBrain));
        AddSubgoal(new ImproveResearchProduction(civilisationBrain));

    }
}
public class ConquerPlanet : CompositeGoal
{
    public ConquerPlanet(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.dominace;
        age = 2;
     //   AddSubgoal(new BuildObject(civilisationBrain, new Laboratory()));
    }
}
public class SiegePlanet : CompositeGoal
{
    public SiegePlanet(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.dominace;
        age = 2;
        //   AddSubgoal(new BuildObject(civilisationBrain, new Laboratory()));
    }
}
public class FormFederation : CompositeGoal
{
    public FormFederation(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.diplomacy;
        age = 2;
        //   AddSubgoal(new BuildObject(civilisationBrain, new Laboratory()));
    }
}
public class MoveToLocation : AtomicGoal
{
    public MoveToLocation(CivilisationBrain _civilisationBrain)
    {


    }
}
public class BuildSatelliltes : CompositeGoal
{
    public BuildSatelliltes(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;
       
        AddSubgoal(new BuildObject(civilisationBrain, new Satellilte()));
        AddSubgoal(new ResearchTechnology(civilisationBrain, new Satellites()));
    }
}
public class ImproveSatelliltes : CompositeGoal
{
    public ImproveSatelliltes(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.discovery;
        age = 2;

       
        AddSubgoal(new BuildObject(civilisationBrain, new ImprovedSatellite()));
        AddSubgoal(new ResearchTechnology(civilisationBrain, new ImprovedSatellites()));
    }
}
public class BuildMegaCities : CompositeGoal
{
    public BuildMegaCities(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.prosperity;
        age = 2;

       
        AddSubgoal(new BuildObject(civilisationBrain, new MegaCity()));
        AddSubgoal(new ResearchTechnology(civilisationBrain, new CityPlanning()));
    }
}
public class BuildGalacticMarket : CompositeGoal
{
    public BuildGalacticMarket(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.prosperity;
        age = 2;

       
        AddSubgoal(new BuildObject(civilisationBrain, new GalacticMarket()));
        AddSubgoal(new ResearchTechnology(civilisationBrain, new GalacticEconomy()));
    }
}
public class BuildEmbassies : CompositeGoal
{
    public BuildEmbassies(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.diplomacy;
        age = 2;

        AddSubgoal(new BuildObject(civilisationBrain, new Embassy()));
    }
}
public class BionicDevelopment : CompositeGoal
{
    public BionicDevelopment(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.dominace;
        age = 2;

        AddSubgoal(new ResearchTechnology(civilisationBrain, new Bionics()));
    }
}
public class Ascend : CompositeGoal
{
    public Ascend(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.harmony;
        age = 2;

        AddSubgoal(new ResearchTechnology(civilisationBrain, new Ascendency()));
    }
}
public class PlanetaryDefence : CompositeGoal
{
    public PlanetaryDefence(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.prosperity;
        age = 2;
        AddSubgoal(new BuildObject(civilisationBrain, new PlanetShields()));        
        AddSubgoal(new ResearchTechnology(civilisationBrain, new PlanetaryShield()));
        AddSubgoal(new BuildObject(civilisationBrain, new PlanetOrbitalGuns()));
        AddSubgoal(new ResearchTechnology(civilisationBrain, new PlanetaryGunPlatforms()));
    }
}
public class BuildUpBattleShips : CompositeGoal
{
    public BuildUpBattleShips(CivilisationBrain _civilisationBrain)
    {
        civilisationBrain = _civilisationBrain;
        focus = Focus.prosperity;
        age = 2;

        for (int i = 0; i < 4; i++)
        {
            AddSubgoal(new BuildBattleShip(civilisationBrain));
        }
        if (!civilisationBrain.researchedTechnology.Contains("BattleShips"))
        {
            AddSubgoal(new ResearchTechnology(civilisationBrain, new BattleShips()));
        }
    }
}