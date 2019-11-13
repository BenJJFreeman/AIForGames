using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneticAlgorithm
{
    

    public int populationSize;
    [Range(0, 1)]
    public float mutationRate;
    [Range(0, 1)]
    public float reproductionaRate;
   

    //public int totalGenerations;    
    public List<Population> population = new List<Population>();


    public void StartGeneticAlgorithm(int numberOfPops,int[] planetIDs)
    {
        populationSize = 75;
        mutationRate = 0.025f;
        reproductionaRate = 0.05f;
        //population = new Population[numberOfPops];

        for (int i = 0; i < numberOfPops; i++)
        {
            population.Add(new Population());
            //population[i] = new Population();
            population[i].SetUpPopulation(i, populationSize, mutationRate, reproductionaRate, planetIDs[i]);
        }


    }

    public void UpdateGeneticAlgorithm(Main main)
    {
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].UpdatePopulation() == false)
            {
                //totalGenerations++;
                population[i].totalGenerations++;
                population[i].populationSize = population[i].pop.Count;
            }
            else
            {
                main.CreateNewCivilisation(population[i]);
                population.Remove(population[i]);
                
            }
        }

    }





}
