using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State<Ship> {

    public override void Execute(Ship ship)
    {

        ship.velocity += SteeringForce(ship);


        if (ship.targetShip == null)
        {
            if (ship.colonyShip)
            {
                ship.SetShipState(new MoveToPoint());
            }
            else
            {
                ship.SetShipState(new OrbitPlanet());
            }
        }

        if (Vector3.Distance(ship.transform.position, ship.targetShip.transform.position) < 40  && ship.myTime > ship.nextFire)
        {
            ship.nextFire = ship.myTime + ship.fireDelta;
            ship.AttackEnemy();
            ship.nextFire = ship.nextFire - ship.myTime;
            ship.myTime = 0.0F;
        }



    }
    Vector3 SteeringForce(Ship ship)
    {
        Vector3 newSteeringForce = new Vector3();

        newSteeringForce += ship.SeekToPositionSteeringForce(ship.SeekToTargetEnemy())/10;
        newSteeringForce += ship.ArriveAtPositionSteeringForce();      
        
        newSteeringForce += ship.Seperation()/5;
        newSteeringForce += ship.Allignment()/5;
        newSteeringForce += ship.Cohesion()/5;        

        newSteeringForce = newSteeringForce / 5;
        

        return newSteeringForce;
    }
}
