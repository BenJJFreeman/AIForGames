using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public bool colonyShip;
    public int health;
    public int damage;
    public Vector3 targetPoint;
    public Vector3 velocity;
    public Vector3 heading;
    public float maxSpeed;
    public float acceleration;
    public Vector3 movementVector;
    public int targetPlanet;
    public int shipID;
    public int civilisationID;
    public Vector3 targetHeading;
    public float targetSpeed;
    public Vector3 targetVelocity;

    public LayerMask shipLayerMask;

    public State<Ship> state;

    public Ship targetShip;

    public float fireDelta = 0.5F;
    public float nextFire = 0.5F;
    public float myTime = 0.0F;

    public float gameSpeed;
    private void Awake()
    {
        shipID = -1;
        targetPlanet = -1;
        health = 10;
    }
    public void DestroyShip()
    {
        Destroy(gameObject);
    }
    public void SetUp(int _civilisationID)
    {
        civilisationID = _civilisationID;
        SetShipState(new OrbitPlanet());
    }
    public void SetUpColonyShip(int _targetPlanet, int _civilisationID)
    {
        targetPlanet = _targetPlanet;
        SetShipState(new MoveToPoint());
        civilisationID = _civilisationID;
    }
    public void SetShipState(State<Ship> _state)
    {
        state = _state;
    }
    public bool UpdateColonyShip(float _gameSpeed, Vector3 _targetPoint, Vector3 _targetHeading, float _targetSpeed, Vector3 _targetVelocity) {

        UpdateShip(_gameSpeed,_targetPoint, _targetHeading, _targetSpeed, _targetVelocity);

        if(Vector3.Distance(transform.position,targetPoint) < 15)
        {
            return true;
        }
        return false;
    }    
    public void UpdateShip (float _gameSpeed,Vector3 _targetPoint,Vector3 _targetHeading,float _targetSpeed,Vector3 _targetVelocity) {

        gameSpeed = _gameSpeed;

        myTime = myTime + Time.deltaTime * gameSpeed;

        targetPoint = _targetPoint + (Vector3.up * 5);
        targetHeading = _targetHeading;
        targetSpeed = _targetSpeed;
        targetVelocity = _targetVelocity;

        if(health < 5)
        {
            SetShipState(new Flee());
        }
        if (GetEnemyNeighbours().Length > 0)
        {
            if (colonyShip)
            {
                SetShipState(new Flee());
            }
            else
            {
                SetShipState(new Attack());
            }
        }       
        

            state.Execute(this);
                

        transform.position += velocity * gameSpeed * Time.deltaTime;
        heading = velocity.normalized;

        transform.rotation = Quaternion.LookRotation(heading);     

    }
    Vector3 SteeringForce()
    {
        Vector3 newSteeringForce = new Vector3();

        newSteeringForce += SeekToPositionSteeringForce(FutureTargetPosition());
        newSteeringForce += ArriveAtPositionSteeringForce();

       // if (colonyShip == true)
        //{
            newSteeringForce += Seperation();
            newSteeringForce += Allignment();
            newSteeringForce += Cohesion();

            newSteeringForce = newSteeringForce / 5;
       // }else
       //     newSteeringForce = newSteeringForce / 2;

        return newSteeringForce;
    }
    public Vector3 SeekToPositionSteeringForce(Vector3 position)
    {
      return ((position - transform.position).normalized * maxSpeed) - velocity;
    }
    public Vector3 FleeFromPositionSteeringForce(Vector3 position)
    {
        return ((transform.position - position).normalized * maxSpeed) - velocity;
    }
    public Vector3 SeekToTargetEnemy()
    {
        targetShip = (GetTargetEnemyShip() != null ? GetTargetEnemyShip() : targetShip);
        if (targetShip != null)
            return SeekToPositionSteeringForce(targetShip.transform.position);
        return Vector3.zero;
    }
    public Vector3 FleeFromEnemies()
    {
        Vector3[] shipPos = GetNeighboursPos(GetEnemyNeighbours());
        Vector3 pos = new Vector3();

        for(int i = 0;i < shipPos.Length; i++)
        {
            pos += FleeFromPositionSteeringForce(shipPos[i]);
        }

        pos = pos / shipPos.Length;

        return pos;
    }
    public Vector3 ArriveAtPositionSteeringForce()
    {
        float distance = (targetPoint - transform.position).magnitude;
        Vector3 desiredVelocity = new Vector3();
        float threshold = 10;
        if(distance < threshold)
        {
            desiredVelocity = ((targetPoint - transform.position).normalized) *maxSpeed *(distance/ threshold);
        }
        return desiredVelocity - velocity;
    }
    public Vector3 ToTarget(Vector3 pos)
    {
        return pos - transform.position;
    }
    public float RelativeHeading()
    {
        return Vector3.Dot(heading.normalized, targetHeading.normalized);
    }
    public float LookAheadTime()
    {
        return ToTarget(targetPoint).magnitude / (maxSpeed + targetSpeed);
    }
    public Vector3 FutureTargetPosition()
    {
        return targetPoint + targetVelocity * LookAheadTime();
    }
    public Vector3 Seperation()
    {
        Vector3[] alliedNeighbours = GetNeighboursPos(GetFriendlyNeighbours());
        Vector3 steeringForce = Vector3.zero;
        for(int i = 0;i < alliedNeighbours.Length; i++)
        {
            steeringForce -= SeperationSteeringForce(alliedNeighbours[i]).normalized;
        }
        return steeringForce;
    }
    public Vector3 SeperationSteeringForce(Vector3 pos)
    {
        Vector3 h = ToTarget(pos);

        float d = Distance(h);
        if (d != 0) {
            h = h / d;
        }

        return h;
    }
    public float Distance(Vector3 toTarget)
    {
        return toTarget.magnitude;
    }
    public Vector3 Allignment()
    {
        Vector3 averageHeading = Vector3.zero;
        Vector3 steeringForce = Vector3.zero;

        Vector3[] alliedNeighbours = GetNeighboursHeading(GetFriendlyNeighbours());

        for (int i = 0; i < alliedNeighbours.Length; i++)
        {
            averageHeading += alliedNeighbours[i];
        }
        if(alliedNeighbours.Length > 0)
        {
            averageHeading = averageHeading / alliedNeighbours.Length;
            steeringForce = averageHeading - heading;

        }

        return steeringForce;
    }
    public Vector3 Cohesion()
    {
        Vector3 averagePosition = Vector3.zero;
        Vector3 steeringForce = Vector3.zero;

        Vector3[] alliedNeighbours = GetNeighboursPos(GetFriendlyNeighbours());

        for (int i = 0; i < alliedNeighbours.Length; i++)
        {
            averagePosition += alliedNeighbours[i];
        }

        if (alliedNeighbours.Length > 0)
        {
            averagePosition = averagePosition / alliedNeighbours.Length;
            steeringForce = SeekToPositionSteeringForce(averagePosition);

        }
        return steeringForce;
    }
    public Ship GetTargetEnemyShip()
    {
        Ship[] enemyShips = GetEnemyNeighbours();
        float maxThreat = 0;
        float curThreat = 0;
        Ship _targetShip = null;
        for(int i = 0; i < enemyShips.Length; i++)
        {
            curThreat = CalculateEnemyShipThreat(enemyShips[i]);
            if (curThreat > maxThreat)
            {
                maxThreat = curThreat;
                _targetShip = enemyShips[i];
            }
        }
        return _targetShip;
    }
    public float CalculateEnemyShipThreat(Ship _ship)
    {
        float dist = Vector3.Distance(transform.position, _ship.transform.position);
        dist = Mathf.Abs(dist);
        float he = _ship.health;
        float da = _ship.damage;

        return dist + he + da;
    }
    public Ship[] GetAllNeighbours()
    {
        Collider[] col;

        col = Physics.OverlapSphere(transform.position, 25, shipLayerMask);

        Ship[] ships = new Ship[col.Length];
        List<Ship> shipList = new List<Ship>();
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject.GetComponent<Ship>().shipID != shipID)
            {
                //if (col[i].gameObject.GetComponent<Ship>().targetPoint == targetPoint)
                //{

                    shipList.Add(col[i].gameObject.GetComponent<Ship>());
                    ships[i] = col[i].gameObject.GetComponent<Ship>();
                //}
            }
            else
            {
                if (col[i].gameObject.GetComponent<Ship>().civilisationID != civilisationID)
                {
                    //if (col[i].gameObject.GetComponent<Ship>().targetPoint == targetPoint)
                    //{

                    shipList.Add(col[i].gameObject.GetComponent<Ship>());
                    ships[i] = col[i].gameObject.GetComponent<Ship>();
                    //}
                }

            }
        }

        ships = shipList.ToArray();

        return ships;
    }
    public Ship[] GetFriendlyNeighbours()
    {
        Ship[] ships = GetAllNeighbours();
        List<Ship> shipList = new List<Ship>();

        for(int i = 0; i < ships.Length; i++)
        {
            if(ships[i].civilisationID == civilisationID)
            {
                shipList.Add(ships[i]);
            }
        }
        return shipList.ToArray();
    }
    public Ship[] GetEnemyNeighbours()
    {
        Ship[] ships = GetAllNeighbours();
        List<Ship> shipList = new List<Ship>();

        for (int i = 0; i < ships.Length; i++)
        {
            if (ships[i].civilisationID != civilisationID)
            {
                shipList.Add(ships[i]);
            }
        }

        return shipList.ToArray();
    }
    public Vector3[] GetNeighboursPos(Ship[] ships)
    {
        //Ship[] ships = GetAllNeighbours();
        Vector3[] pos = new Vector3[ships.Length];

        for(int i = 0; i < ships.Length; i++)
        {
            pos[i] = ships[i].transform.position;

        }

        return pos;
    }
    public Vector3[] GetNeighboursHeading(Ship[] ships)
    {
        //Ship[] ships = GetAllNeighbours();
        Vector3[] head = new Vector3[ships.Length];

        for (int i = 0; i < ships.Length; i++)
        {
            head[i] = ships[i].heading;

        }

        return head;
    }
    public void AttackEnemy()
    {
        GameObject laser = new GameObject("Laser");

        laser.AddComponent<LineRenderer>();
        LineRenderer l = laser.GetComponent<LineRenderer>();

        l.positionCount = 2;

        l.SetPosition(0, transform.position);
        l.SetPosition(1, targetShip.transform.position);

        targetShip.health -= damage;

        Destroy(laser, .1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(FutureTargetPosition(), .5f);
        
    }
}
