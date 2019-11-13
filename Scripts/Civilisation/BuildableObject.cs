using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildableObject  {

    public Building building;
    //public Result result;
    public int mineralCost;
    float m;
    public bool unique;
    public BuildableObject(int _mineralCost, Building _building, bool _unique)
    {
        mineralCost = _mineralCost;
        building = _building;
        unique = _unique;
    }
    public bool Process(int _mineralTick)
    {
        m += _mineralTick;

        if (m >= mineralCost)
        {
            return true;
        }

        return false;
    }
}
public class NoBuilding : BuildableObject { public NoBuilding() : base(999999, new Building(BuildingName.NoBuilding, new Result(),BuildingType.building),false) { } };
public enum BuildingType { building, ship,colonyShip }
// Age 0 - Primitive
/*
public class Settlement : BuildableObject { public Settlement() : base(50, new Result(0, new int[]{1,1,1,1}, 0, 0), true) { } };
public class Militia : BuildableObject { public Militia() : base(50, new Result(0, new int[4], 0, 0), false) { } };
public class Scout : BuildableObject { public Scout() : base(50, new Result(0, new int[4], 0, 0), false) { } };
public class Diplomat : BuildableObject { public Diplomat() : base(50, new Result(0, new int[4], 0, 0), false) { } };
public class Market : BuildableObject { public Market() : base(50, new Result(0, new int[] { 0, 0, 0, 0 }, 0, 0), true) { } };
public class GatheringPoint : BuildableObject { public GatheringPoint() : base(50, new Result(0, new int[] { 0, 0, 0, 0 }, 0, 0), true) { } };
*/

public enum BuildingName { NoBuilding, ColonyShip, BattleShip, Satellilte, ImprovedSatellite, Hydroponics, Mine, Factory, PowerPlant, Laboratory, University, MegaCity, GalacticMarket, Embassy, PlanetShields, PlanetOrbitalGuns }

// Age 1 - Industrial


// Age 2 - SpaceAge
public class ColonyShip : BuildableObject { public ColonyShip() : base(1000, new Building(BuildingName.ColonyShip, new Result(), BuildingType.colonyShip), false) { } };
public class BattleShip : BuildableObject { public BattleShip() : base(2000, new Building(BuildingName.BattleShip, new Result(new List<ResultPart>{ new ResultPart(50, ResultType.militaryPower) }), BuildingType.ship), false) { } };
public class Satellilte : BuildableObject { public Satellilte() : base(1000, new Building(BuildingName.Satellilte, new Result(new List<ResultPart> { new ResultPart(2, ResultType.researchSpeed) }), BuildingType.building), true) { } };
public class ImprovedSatellite : BuildableObject { public ImprovedSatellite() : base(3000, new Building(BuildingName.ImprovedSatellite, new Result(new List<ResultPart> { new ResultPart(2, ResultType.researchProduction) }), BuildingType.building), true) { } };
public class Hydroponics : BuildableObject { public Hydroponics() : base(500, new Building(BuildingName.Hydroponics, new Result(new List<ResultPart> { new ResultPart(2, ResultType.foodProduction) }), BuildingType.building), true) { } };
public class Mine : BuildableObject { public Mine() : base(500, new Building(BuildingName.Mine, new Result(new List<ResultPart> { new ResultPart(2, ResultType.mineralProduction) }), BuildingType.building), true) { } };
public class Factory : BuildableObject { public Factory() : base(800, new Building(BuildingName.Factory, new Result(new List<ResultPart> { new ResultPart(2, ResultType.buildSpeed) }), BuildingType.building), true) { } };
public class PowerPlant : BuildableObject { public PowerPlant() : base(600, new Building(BuildingName.PowerPlant, new Result(new List<ResultPart> { new ResultPart(2, ResultType.energyProduction) }), BuildingType.building), true) { } };
public class Laboratory : BuildableObject { public Laboratory() : base(600, new Building(BuildingName.Laboratory, new Result(new List<ResultPart> { new ResultPart(2, ResultType.researchProduction) }), BuildingType.building), true) { } };
public class University : BuildableObject { public University() : base(1000, new Building(BuildingName.University, new Result(new List<ResultPart> { new ResultPart(2, ResultType.researchSpeed) }), BuildingType.building), true) { } };
public class MegaCity : BuildableObject { public MegaCity() : base(4000, new Building(BuildingName.MegaCity, new Result(new List<ResultPart> { new ResultPart(-2, ResultType.foodProduction), new ResultPart(-2, ResultType.energyProduction), new ResultPart(2, ResultType.researchProduction), new ResultPart(2, ResultType.buildSpeed) }), BuildingType.building), true) { } };
public class GalacticMarket : BuildableObject { public GalacticMarket() : base(2000, new Building(BuildingName.GalacticMarket, new Result(new List<ResultPart> { new ResultPart(5, ResultType.foodProduction), new ResultPart(3, ResultType.mineralProduction), new ResultPart(3, ResultType.researchProduction), new ResultPart(1, ResultType.researchSpeed), new ResultPart(4, ResultType.energyProduction) }), BuildingType.building), true) { } };
public class Embassy : BuildableObject { public Embassy() : base(1500, new Building(BuildingName.Embassy, new Result(new List<ResultPart> { new ResultPart(4, ResultType.researchProduction) }), BuildingType.building), true) { } };
public class PlanetShields : BuildableObject { public PlanetShields() : base(6000, new Building(BuildingName.PlanetShields, new Result(new List<ResultPart> { new ResultPart(20, ResultType.militaryPower) }), BuildingType.building), true) { } };
public class PlanetOrbitalGuns : BuildableObject { public PlanetOrbitalGuns() : base(5000, new Building(BuildingName.PlanetOrbitalGuns, new Result(new List<ResultPart> { new ResultPart(40, ResultType.militaryPower) }), BuildingType.building), true) { } };