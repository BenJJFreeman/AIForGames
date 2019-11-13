using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour
{
    public static Main current;

    int ts = 0;
    public Text ticks;
    public Slider speedSlider;
    public Text[] CivilisationTexts;
    public Text planetText;
    public GalaxyControl GalaxyControl;

    public GeneticAlgorithm geneticAlgorithm;
    public int numberOfPops;

    public List<Civilisation> civilisations = new List<Civilisation>();
    float t = 0;

    [Range(0,25)]
    public float gameSpeed;

    float resourceT = 0;


    public GameObject planetPopUpPrefab;
    public GameObject canvas3D;
    //public List<GameObject> planetPopUpList = new List<GameObject>();

    PlanetInfo tempInfo;

    public GameObject shipPrefab,colonyShipPrefab;

    public int[] path;

    public int currentNumber;

    GameObject pathLine;

    private void Awake()
    {
        current = this;
    }
    public void ResetSim()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    void Start()
    {
       

        GameObject galaxy = GalaxyGenerator.CreateGalaxy(Random.Range(0, 10000), 10);
        galaxy.transform.parent = transform;
        GalaxyControl = galaxy.GetComponent<GalaxyControl>();

        

        int[] planetIDs = CheckForPotentialLife();
        geneticAlgorithm = new GeneticAlgorithm();
        geneticAlgorithm.StartGeneticAlgorithm(numberOfPops, planetIDs);

        SetPlanetsPopulations();
        StartInvolvedPlanetsUI();
        //AssignLifeToPlanet();

        path = GalaxyNavigationGrid.current.CalculateAStarPath(0, 6);

        ShowAll();

    }
    int[] CheckForPotentialLife()
    {
        List<int> planetIDs = new List<int>();
        for(int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if (GalaxyControl.systemInfo[i].planetInfo[j].canSpawnLife)
                {
                    //GalaxyControl.systemInfo[i].planetInfo[j].geneticAlgorithmAssigned = geneticAlgorithm.population[numberOfPops];
                   // GalaxyControl.systemInfo[i].planetInfo[j].geneticAlgorithmAssigned = numberOfPops;
                    numberOfPops++;
                    planetIDs.Add(GalaxyControl.systemInfo[i].planetInfo[j].planetId);
                }
            }
        }
        return planetIDs.ToArray();
    }
    void SetPlanetsPopulations()
    {
        for(int i = 0; i < geneticAlgorithm.population.Count; i++)
        {
            GalaxyControl.planetInfos[geneticAlgorithm.population[i].planetID].geneticAlgorithmAssigned = geneticAlgorithm.population[i];
        }
    }
    //void AssignLifeToPlanet()
    //{
    //    int n = 0;
    //    for (int i = 0; i < GalaxyControl.systemInfo.Count; i++)
    //    {
    //        for (int j = 0; j < GalaxyControl.systemInfo[i].planetInfo.Count; j++)
    //        {
    //            if (GalaxyControl.systemInfo[i].planetInfo[j].canSpawnLife)
    //            {
    //                GalaxyControl.systemInfo[i].planetInfo[j].populations.Add(geneticAlgorithm.population[n]);
    //                n++;
    //            }
    //        }
    //    }       
    //}
    public void CreateNewCivilisation(Population population)
    {/*
        List<SystemInfo> tempS = new List<SystemInfo>();
        List<PlanetInfo> tempP = new List<PlanetInfo>();

        for (int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            for (int p = 0; p< GalaxyControl.systemInfo[i].planetInfo.Count; p++)
            {
                if (GalaxyControl.systemInfo[i].planetInfo[p].canSpawnLife)
                {
                    tempS.Add(GalaxyControl.systemInfo[i]);
                    tempP.Add(GalaxyControl.systemInfo[i].planetInfo[p]);
                }
            }
        }
        */
        civilisations.Add(new Civilisation(this,civilisations.Count,GalaxyControl.planetInfos[population.planetID], population));
    }
    public void UpdateSpeed()
    {
        gameSpeed = speedSlider.value;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name.Contains("Planet"))
            {
                MouseOverPlanet(hit.transform.gameObject);
            }

            Debug.DrawLine(ray.origin, hit.point);
        }

        if (t >= 1)
        {
            UpdateTick();
            ts++;
            ticks.text = ts.ToString();
            t = 0;
        }
        else
        {
            t += gameSpeed * Time.deltaTime;
           
        }

        if (gameSpeed > 0)
        {
            UpdateShips(gameSpeed);
            UpdatePlanetOrbits(gameSpeed);

            UpdateCivilisationText();

        }
        // UpdatePlanetorbits();
        // UpdateInvolvedPlanetsUI();


        if (Input.GetButtonDown("Cycle"))
        {
            int horizontal = (int)Input.GetAxisRaw("Cycle");
            currentNumber += horizontal;
            if (currentNumber >= numberOfPops)
            {
                currentNumber = 0;
            }
            else if (currentNumber < 0)
            {
                currentNumber = numberOfPops - 1;
            }

            if (geneticAlgorithm.population.Count <= currentNumber)
            {
                SetPlanetfocus(GalaxyControl.planetInfos[civilisations[currentNumber - geneticAlgorithm.population.Count].planetsUndeControl[0]]);
            }
            else if (geneticAlgorithm.population.Count > currentNumber)
            {
                SetPlanetfocus(GalaxyControl.planetInfos[geneticAlgorithm.population[currentNumber].planetID]);
            }

        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowAll();
        }

    }
    void UpdateCivilisationText()
    {
        if (tempInfo == null)
            return;
        if (tempInfo.controllingCivilsation > -1)
        {
            CivilisationTexts[0].text = (tempInfo.canSpawnLife ? (tempInfo.controllingCivilsation > -1 ? "Civilisation" : tempInfo.geneticAlgorithmAssigned.averageSentience.ToString()) : "None");
            CivilisationTexts[1].text = "Civilisation Age: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.civilisationAge.ToString();
            CivilisationTexts[2].text = "Population Count: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.populationCount.ToString();

            CivilisationTexts[3].text = "Minerals: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.resources.minerals.ToString();
            CivilisationTexts[4].text = "Energy: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.resources.energy.ToString();
            CivilisationTexts[5].text = "Food: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.resources.food.ToString();
            CivilisationTexts[6].text = "Research: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.resources.research.ToString();

            CivilisationTexts[7].text = "Current Tech: " + civilisations[tempInfo.controllingCivilsation].civilisationBrain.researchingTechnology.ToString();

            CivilisationTexts[8].text = "Building: " + civilisations[tempInfo.controllingCivilsation].civilisationBrain.currentBuildingObject.ToString();

            CivilisationTexts[9].text = "Military Power: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.militaryPower.ToString();

            CivilisationTexts[10].text = "Goal: " + civilisations[tempInfo.controllingCivilsation].civilisationBrain.goal.ToString();

            CivilisationTexts[11].text = "Goal Desires:";

            CivilisationTexts[12].text = "Change: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.mineralChange;
            CivilisationTexts[13].text = "Change: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.energyChange;
            CivilisationTexts[14].text = "Change: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.foodChange;
            CivilisationTexts[15].text = "Change: " + civilisations[tempInfo.controllingCivilsation].civilisationInfo.researchChange;


            List<Goal> potentialGoals = civilisations[tempInfo.controllingCivilsation].civilisationBrain.GetPotentialGoals();

            for (int i = 0; i < civilisations[tempInfo.controllingCivilsation].civilisationBrain.goalDesire.Length; i++)
            {
                CivilisationTexts[11].text += "\n" + potentialGoals[i].ToString() + ": " + civilisations[tempInfo.controllingCivilsation].civilisationBrain.goalDesire[i].ToString();
            }

        }
        else if(tempInfo.canSpawnLife)
        {
            CivilisationTexts[0].text = "Evolving";
            CivilisationTexts[1].text = "Civilisation Age: ";
            CivilisationTexts[2].text = "Population Count: ";

            CivilisationTexts[3].text = "Minerals: ";
            CivilisationTexts[4].text = "Energy: ";
            CivilisationTexts[5].text = "Food: ";
            CivilisationTexts[6].text = "Research: ";

            CivilisationTexts[7].text = "Current Tech: ";

            CivilisationTexts[8].text = "Building: ";

            CivilisationTexts[9].text = "Military Power: ";

            CivilisationTexts[10].text = "Goal: ";

            CivilisationTexts[11].text = "Goal Desires:";

            CivilisationTexts[12].text = "Change: ";
            CivilisationTexts[13].text = "Change: ";
            CivilisationTexts[14].text = "Change: ";
            CivilisationTexts[15].text = "Change: ";           
        }
    }
    void MouseOverPlanet(GameObject planet)
    {
        // tempInfo = planet.GetComponent<PlanetInfo>();

        if (pathLine != null)
        {
            Destroy(pathLine);
        }
        pathLine = CreatePathLine(GalaxyNavigationGrid.current.CalculateAStarPath(0, GetNearestSystem(planet.transform.position)));



        //Debug.Log("Max Sentience: " + geneticAlgorithm.population[planet.GetComponent<PlanetInfo>().planetNumber].maxSentience + " Average Sentience: " + geneticAlgorithm.population[planet.GetComponent<PlanetInfo>().planetNumber].averageSentience);
        if (Input.GetMouseButtonDown(0))
        {
            SetPlanetfocus(planet.GetComponent<PlanetInfo>());
        }

    }
    void SetPlanetfocus(PlanetInfo planetInfo)
    {
        tempInfo = planetInfo;

        path = GalaxyNavigationGrid.current.CalculateAStarPath(0, tempInfo.systemId);

        // planetText.text = ("Planet Type: " + tempInfo.type + "\n" + "Planet Size: " + tempInfo.size + "\n" + "Life " + (tempInfo.canSpawnLife ? (tempInfo.controllingCivilsation > -1 ? "Civilisation" : geneticAlgorithm.population[tempInfo.geneticAlgorithmAssigned].averageSentience.ToString()) : "None"));

        Camera.main.transform.position = tempInfo.transform.position - new Vector3(0,-100,100);

        UpdateCivilisationText();

        ShowKnownPlanets();
    }
    void ShowAll()
    {
        if (transform.childCount > 1)
        {
            Destroy(transform.GetChild(1).gameObject);
        }

        GameObject lineRendererMain = new GameObject("lineRendererMain");
        lineRendererMain.transform.parent = transform;

        for (int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            GalaxyControl.systemInfo[i].gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
           
            for (int j = 0; j < GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                GalaxyControl.systemInfo[i].planetInfo[j].gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
            for (int j = 0; j < GalaxyControl.systemInfo[i].connectedSystems.Count; j++)
            {
                CreateSystemLineConnection(GalaxyControl.systemInfo[i].systemNumber, GalaxyControl.systemInfo[i].connectedSystems[j]).transform.parent = lineRendererMain.transform;

            }
        }

    }

    void ShowKnownPlanets()
    {
        for (int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            GalaxyControl.systemInfo[i].gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
        for (int i = 0; i < GalaxyControl.planetInfos.Count; i++)
        {
            GalaxyControl.planetInfos[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (tempInfo.controllingCivilsation > -1)
        {
            for (int i = 0; i < civilisations[tempInfo.controllingCivilsation].knownPlanets.Count; i++)
            {
                GalaxyControl.planetInfos[civilisations[tempInfo.controllingCivilsation].knownPlanets[i]].gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            for (int i = 0; i < civilisations[tempInfo.controllingCivilsation].knownSystems.Count; i++)
            {
                GalaxyControl.systemInfo[civilisations[tempInfo.controllingCivilsation].knownSystems[i]].gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            }

            ShowKnownConnections();
        }
        else
        {
            GalaxyControl.planetInfos[tempInfo.planetId].gameObject.GetComponent<MeshRenderer>().enabled = true;
            // GalaxyControl.systemInfo[tempInfo.systemId].gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            if (transform.childCount > 1)
            {
                Destroy(transform.GetChild(1).gameObject);
            }
        }



    }
    void ShowKnownConnections()
    {
        if (transform.childCount > 1)
        {
            Destroy(transform.GetChild(1).gameObject);
        }

        GameObject lineRendererMain = new GameObject("lineRendererMain");
        lineRendererMain.transform.parent = transform;
        for (int i = 0; i < civilisations[tempInfo.controllingCivilsation].knownSystems.Count; i++)
        {
            for (int j = 0; j < GalaxyControl.systemInfo[civilisations[tempInfo.controllingCivilsation].knownSystems[i]].connectedSystems.Count; j++)
            {
                CreateSystemLineConnection(civilisations[tempInfo.controllingCivilsation].knownSystems[i], GalaxyControl.systemInfo[civilisations[tempInfo.controllingCivilsation].knownSystems[i]].connectedSystems[j]).transform.parent = lineRendererMain.transform;
            }
        }
        
    }
    GameObject CreateSystemLineConnection(int systemA,int systemB)
    {
        GameObject line = new GameObject("line");

        line.AddComponent<LineRenderer>();
        LineRenderer l = line.GetComponent<LineRenderer>();
        l.material.color = Color.white;

        l.positionCount = 2;

        Vector3 heading = GalaxyControl.systemInfo[systemB].transform.position - GalaxyControl.systemInfo[systemA].transform.position;
        float dist = heading.magnitude;
        Vector3 dir = heading/dist;
        l.SetPosition(0, GalaxyControl.systemInfo[systemA].transform.position + (dir * 80));

        heading = GalaxyControl.systemInfo[systemA].transform.position - GalaxyControl.systemInfo[systemB].transform.position;
        dist = heading.magnitude;
        dir = heading / dist;
        l.SetPosition(1, GalaxyControl.systemInfo[systemB].transform.position + (dir * 80));


        return line;
    }
    public int GetNearestSystem(Vector3 _location)
    {
        float dist = 999999999;
        int nearSyst = 0;
        for (int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            if (Vector3.Distance(_location, GalaxyControl.systemInfo[i].transform.position) < dist)
            {
                dist = Vector3.Distance(_location, GalaxyControl.systemInfo[i].transform.position);
                nearSyst = i;
            }
        }
        return nearSyst;
    }
    GameObject CreatePathLine(int[] _path)
    {
        GameObject line = new GameObject("line");

        line.AddComponent<LineRenderer>();
        LineRenderer l = line.GetComponent<LineRenderer>();
        l.material.color = Color.yellow;

        l.positionCount = _path.Length;

        for (int i = 0; i < _path.Length; i++)
        {
            l.SetPosition(i, GalaxyControl.systemInfo[_path[i]].transform.position + Vector3.up);
        }


        return line;

    }
    void UpdateTick()
    {
        geneticAlgorithm.UpdateGeneticAlgorithm(this);

        resourceT += 1;
        bool tempG = false;
        if(resourceT >= 5)
        {
            tempG = true;
            resourceT = 0;
        }

        for (int i = 0; i < civilisations.Count; i++)
        {
            civilisations[i].UpdateCivilisation(tempG);
        }
    }
    void UpdateShips(float gameSpeed)
    {
        for(int i = 0; i < civilisations.Count; i++)
        {
            civilisations[i].UpdateShips(gameSpeed);
        }
    }
    void StartInvolvedPlanetsUI()
    {
        for (int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if (GalaxyControl.systemInfo[i].planetInfo[j].canSpawnLife)
                {
                    GameObject newPopUp = Instantiate(planetPopUpPrefab, GalaxyControl.systemInfo[i].planetInfo[j].transform.position, Quaternion.identity);
                    newPopUp.transform.parent = canvas3D.transform;
                    GalaxyControl.systemInfo[i].planetInfo[j].planetPopUp = newPopUp;
                    //planetPopUpList.Add(newPopUp);
                }
                else
                {
                    
                }
            }
        }
    }
    void UpdatePlanets()
    {
        for (int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                GalaxyControl.systemInfo[i].planetInfo[j].UpdatePlanet();                
            }
        }
    }
    void UpdatePlanetOrbits(float gameSpeed)
    {

        foreach (Transform starSytem in transform.GetChild(0))
        {
            foreach (Transform planet in starSytem.GetChild(0).GetChild(0))
            {
                planet.RotateAround(planet.parent.position,new Vector3(0,1,0),planet.GetComponent<PlanetInfo>().speed * gameSpeed * Time.deltaTime);

            }
        }
        UpdatePlanets();
    }
    private void OnDrawGizmos()
    {
        if (GalaxyControl == null)
            return;
        for(int i = 0; i < GalaxyControl.systemInfo.Count; i++)
        {
            for (int j = 0; j < GalaxyControl.systemInfo[i].planetInfo.Count; j++)
            {
                if (GalaxyControl.systemInfo[i].planetInfo[j].canSpawnLife)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(GalaxyControl.systemInfo[i].planetInfo[j].transform.position, GalaxyControl.systemInfo[i].planetInfo[j].transform.localScale.x * 1.5f);
                }
                else if(GalaxyControl.systemInfo[i].planetInfo[j].controllingCivilsation >-1)
                {                    
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(GalaxyControl.systemInfo[i].planetInfo[j].transform.position, GalaxyControl.systemInfo[i].planetInfo[j].transform.localScale.x * 1.5f);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(GalaxyControl.systemInfo[i].planetInfo[j].transform.position, GalaxyControl.systemInfo[i].planetInfo[j].transform.localScale.x * 1.5f);
                }
            }


            for (int j = 0; j < GalaxyControl.systemInfo[i].connectedSystems.Count; j++)
            {
                Gizmos.DrawLine(GalaxyControl.systemInfo[i].transform.position, GalaxyControl.systemInfo[GalaxyControl.systemInfo[i].connectedSystems[j]].transform.position);
            }

        }


        for(int i = 0; i < path.Length-1; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(GalaxyControl.systemInfo[path[i]].transform.position, GalaxyControl.systemInfo[path[i+1]].transform.position);
        }


    }
    public GameObject CreateShip(bool colonyShip)
    {
        if(colonyShip)
            return  Instantiate(colonyShipPrefab);

        return Instantiate(shipPrefab);
    }
}