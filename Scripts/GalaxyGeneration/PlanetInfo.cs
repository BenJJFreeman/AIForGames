using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfo : MonoBehaviour
{
    //public Vector3 position;
    public PlanetType type;
    public bool canSpawnLife;


    public int size;
    public float speed;
    public Vector3 heading;
    public Vector3 velocity;

    public int systemId;
    public Population geneticAlgorithmAssigned;
    public int planetId;
    public int controllingCivilsation;
    //public List<Population> populations = new List<Population>();
   // public List<Civilisation> civilisations = new List<Civilisation>();
    public Resources resources;

    public GameObject planetPopUp;
    public Vector3 OldPos;

    

    public void SetUpPlanet(int _planetId,int _systemId, PlanetType _type,bool _canSpawnLife,int _size,float _speed,Resources _resources)
    {
        //geneticAlgorithmAssigned = -1;
        planetId = _planetId;
        systemId = _systemId;
        controllingCivilsation = -1;
        type = _type;
        canSpawnLife = _canSpawnLife;
        size = _size;
        speed = _speed;
        resources = _resources;
        
    }
    public void UpdatePlanet()
    {
        velocity = (transform.position - OldPos) * speed;
        OldPos = transform.position;

        heading = velocity.normalized;

        if (planetPopUp == null)
            return;
        /*
        if(controllingCivilsation > -1)
        {
            Destroy(planetPopUp);
        }
        if (controllingCivilsation == -1)
        {
            */
            planetPopUp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ShowEvolutionProgress();
            planetPopUp.transform.position = transform.position;
       // }
    }
    string ShowEvolutionProgress()
    {
        string s = "";
        if (controllingCivilsation == -1)
        {
            if (geneticAlgorithmAssigned != null)
            {
                string[] topPops = geneticAlgorithmAssigned.GetTopPops();


                for (int i = 0; i < topPops.Length; i++)
                {
                    s += topPops[i] + "\n";
                }
            }
        }

        //return (controllingCivilsation > -1 ? "C" : (canSpawnLife ? Main.current.geneticAlgorithm.population[geneticAlgorithmAssigned].pop[0].currentStringValue /*+ "E"*/ : "N"));
        return (controllingCivilsation > -1 ? "" : (canSpawnLife ? s /*+ "E"*/ : "N"));
    }
    public float CalculateHabilitability(PlanetType _type)
    {      

        int diff = Mathf.Abs((int)type - (int)_type);

        return 100 - (diff * 10);

    }
   
    /*
    public void AddCivilisationToPlanet(Civilisation _civilisation)
    {
        civilisations.Add(_civilisation);
    }
    */

}
