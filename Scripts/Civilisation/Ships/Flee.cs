using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : State<Ship>
{
    public override void Execute(Ship ship)
    {

        ship.velocity += SteeringForce(ship);

    }
    Vector3 SteeringForce(Ship ship)
    {
        Vector3 newSteeringForce = new Vector3();

        newSteeringForce += ship.SeekToPositionSteeringForce(ship.FleeFromEnemies());
        newSteeringForce += ship.ArriveAtPositionSteeringForce();

        newSteeringForce = newSteeringForce / 2;
      
        return newSteeringForce;
    }
}
