using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPlanet : State<Ship>
{
    public override void Execute(Ship ship)
    {

        ship.velocity += SteeringForce(ship);

    }
    Vector3 SteeringForce(Ship ship)
    {
        Vector3 newSteeringForce = new Vector3();

        newSteeringForce += ship.SeekToPositionSteeringForce(ship.FutureTargetPosition()) / 10;
        newSteeringForce += ship.ArriveAtPositionSteeringForce();
        
        newSteeringForce += ship.Seperation() /2;
        
        newSteeringForce = newSteeringForce / 3;
        
        return newSteeringForce;
    }
}
