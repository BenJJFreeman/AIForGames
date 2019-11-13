using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Technology {

    public Result result;
    public int researchCost;
    float r;
    public Technology(int _researchCost, Result _result)
    {
        researchCost = _researchCost;
        result = _result;
    }
    public bool Process(int researchTick)
    { 
        r += researchTick;

        if(r >= researchCost)
        {
            return true;
        }

        return false;
    }
    public int GetCost()
    {
        return researchCost;
    }

}
public class Result
{

    public List<ResultPart> resultParts = new List<ResultPart>();

   // public int militaryPower;    
   
   // public int[] resourceGatheringSpeed;

   // public int researchSpeed;
   // public int buildSpeed;

    public Result()
    {
        //militaryPower = 0;
        // resourceGatheringSpeed = new int[4];
        // researchSpeed = 0;
        // buildSpeed = 0;
        resultParts = new List<ResultPart>();
    }
    public Result(List<ResultPart> _resultParts)
    {
        
        resultParts = _resultParts;
    }
    /*
    public Result(int _militaryPower, int[] _resourceGatheringSpeed, int _researchSpeed, int _buildSpeed)
    {
        militaryPower = _militaryPower;
        resourceGatheringSpeed = _resourceGatheringSpeed;
        researchSpeed = _researchSpeed;
        buildSpeed = _buildSpeed;
    }
    */


}
public enum ResultType {militaryPower,foodProduction, mineralProduction, energyProduction , researchProduction,buildSpeed,researchSpeed,habitability }
public class ResultPart{ public int amount;public ResultType resultType;public ResultPart(int _amount, ResultType _resultType) { amount = _amount;resultType = _resultType; } }

public class NoResearch : Technology { public NoResearch() : base(999999, new Result()) { } }
// Age 0 - Primitive
/*
public class Literature : Technology { public Literature() : base(100, new Result()) { } }
public class Mathematics : Technology { public Mathematics() : base(100, new Result()) { } }
public class Stonework : Technology { public Stonework() : base(100, new Result()) { } }
public class Art : Technology { public Art() : base(100, new Result()) { } }
public class Metal : Technology { public Metal() : base(100, new Result()) { } }
public class MetalWepons : Technology { public MetalWepons() : base(100, new Result()) { } }
public class RangedWeapons : Technology { public RangedWeapons() : base(100, new Result()) { } }
public class Sailing : Technology { public Sailing() : base(100, new Result()) { } }
public class Philosophy : Technology { public Philosophy() : base(100, new Result()) { } }
public class Economics : Technology { public Economics() : base(100, new Result()) { } }
public class Theology : Technology { public Theology() : base(100, new Result()) { } }
*/
// Age 1 - Industrial
/*
public class Gunpowder : Technology { public Gunpowder() : base(100, new Result()) { } }
public class Archelogy : Technology { public Archelogy() : base(100, new Result()) { } }
public class SteamPower : Technology { public SteamPower() : base(100, new Result()) { } }
public class Railroad : Technology { public Railroad() : base(100, new Result()) { } }
public class Electricity : Technology { public Electricity() : base(100, new Result()) { } }
public class Flight : Technology { public Flight() : base(100, new Result()) { } }
public class Combustion : Technology { public Combustion() : base(100, new Result()) { } }
public class Radio : Technology { public Radio() : base(100, new Result()) { } }
public class MassProduction : Technology { public MassProduction() : base(100, new Result()) { } }
public class Radar : Technology { public Radar() : base(100, new Result()) { } }
public class Computers : Technology { public Computers() : base(100, new Result()) { } }
public class AtomicDevelopment : Technology { public AtomicDevelopment() : base(100, new Result()) { } }

public class Rockets : Technology { public Rockets() : base(100, new Result()) { } }
public class MassMedia : Technology { public MassMedia() : base(100, new Result()) { } }
public class Medicine : Technology { public Medicine() : base(100, new Result()) { } }
*/

// Age 2 - SpaceAge
/*

public class UnmannedExploration : Technology { public UnmannedExploration() : base(100, new Result()) { } }
public class Lasers : Technology { public Lasers() : base(100, new Result()) { } }
public class NuclearFision : Technology { public NuclearFision() : base(100, new Result()) { } }
public class AsteroidMining : Technology { public AsteroidMining() : base(100, new Result()) { } }
*/

public class Satellites : Technology { public Satellites() : base(1000, new Result()) { } }
public class Robotics : Technology { public Robotics() : base(1000, new Result()) { } }
public class Spaceships : Technology { public Spaceships() : base(1000, new Result()) { } }
public class BattleShips : Technology { public BattleShips() : base(1500, new Result()) { } }
public class ColonyShips : Technology { public ColonyShips() : base(2000, new Result()) { } }
public class Habitability : Technology { public Habitability() : base(1500, new Result(new List<ResultPart> { new ResultPart(10, ResultType.habitability) })) { } }
public class ImprovedSatellites : Technology { public ImprovedSatellites() : base(3000, new Result()) { } }
public class GalacticEconomy : Technology { public GalacticEconomy() : base(4000, new Result(new List<ResultPart> { new ResultPart(3, ResultType.energyProduction) })) { } }
public class CityPlanning : Technology { public CityPlanning() : base(2500, new Result(new List<ResultPart> { new ResultPart(1, ResultType.buildSpeed) })) { } }
public class Communication : Technology { public Communication() : base(2000, new Result(new List<ResultPart> { new ResultPart(2, ResultType.researchSpeed) })) { } }
public class Bionics : Technology { public Bionics() : base(3000, new Result()) { } }
public class Ascendency : Technology { public Ascendency() : base(8000, new Result()) { } }
public class PlanetaryShield : Technology { public PlanetaryShield() : base(4000, new Result()) { } }
public class PlanetaryGunPlatforms : Technology { public PlanetaryGunPlatforms() : base(3500, new Result()) { } }