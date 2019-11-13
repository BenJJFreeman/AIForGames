using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building  {

    public BuildingName name;
    public Result result;
    public BuildingType buildingType;

    public Building(BuildingName _name,Result _result, BuildingType _buildingType)
    {
        name = _name;
        result = _result;
        buildingType = _buildingType;
    }
	
}
