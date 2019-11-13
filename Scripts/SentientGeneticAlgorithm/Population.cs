using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Population
{
    public string targetString = "sentient";
    public string availableChars = "abcdefghijklmnopqrstuvwxyz1234567890 [];'#,./";

    public int idNumber;
    public int populationSize;
    public int totalGenerations;
    public List<DNA> pop = new List<DNA>();
    public float mutationRate;
    public float reproductionaRate;
    public float maxSentience;
    public float averageSentience;
    public bool sentient;
    public int evolvingPop;
    public int planetID;
    public void SetUpPopulation(int _idNumber,int populationSize, float _mutationRate, float _reproductionaRate,int _planetID)
    {
        idNumber = _idNumber;

        for (int i = 0; i < populationSize; i++)
        {

            pop.Add(new DNA(GetRandomGenes(),GetRandomStringValue()));

        }

        mutationRate = _mutationRate;
        reproductionaRate = _reproductionaRate;
        planetID = _planetID;
    }

    float[] GetRandomGenes()
    {
        float[] randomGenes = new float[6];

        for (int i = 0; i < randomGenes.Length; i++)
        {
            randomGenes[i] = Random.Range(0.0f, 0.6f);
        }

        return randomGenes;
    }
    string GetRandomStringValue()
    {

        string tempValue = "";

        for (int i = 0; i < targetString.Length; i++)
        {
            tempValue += availableChars[Random.Range(0, availableChars.Length)];
        }


        return tempValue;

    }

    public bool UpdatePopulation()
    {


        if (sentient)
            return true;
        if (CalculateSentience())
            return true;

        EvolvePopulation();

        if (IncreasePopulation())
        {
            pop.Add(new DNA(GetRandomGenes(),GetRandomStringValue()));
        }

        return false;
    }
    void EvolvePopulation()
    {

        for (int i = 0; i < pop.Count; i++)
        {
            pop[i] = Reproduce(GetMatingPair(i));

        }

    }
    DNA[] GetMatingPair(int p)
    {
        DNA[] pair = new DNA[2];

        pair[0] = pop[p];

        pair[1] = pop[GetMate(p)];
        return pair;

    }
    int GetMate(int p)
    {
        List<int> genePool = new List<int>();

        for (int i = 0; i < pop.Count; i++)
        {
            float tempS = 0;
            if (i != p)
            {
                tempS = GetStringValueSum(pop[i].currentStringValue);

                for (int j = 0; j < tempS; j++)
                {
                    genePool.Add(i);
                }
            }
        }

        return genePool[Random.Range(0, genePool.Count)];
    }
    float GetStringValueSum(string currentString)
    {
        float sum = 0;

        for(int i = 0; i < targetString.Length; i++)
        {
            if (currentString[i] == targetString[i])
            {
                sum += 1.0f;
            }
        }        
        return sum;
    }
    float GetGeneSum(float[] _genes)
    {
        float sum = 0;
        foreach (float item in _genes)
        {
            sum += item;
        }
        return sum;
    }
    DNA Reproduce(DNA[] parents)
    {
        string tempValue = "";
        float[] genes = new float[6];

        for (int i = 0; i < genes.Length; i++)
        {
            
            genes[i] = (Random.Range(0, 2) == 0 ? parents[0].genes[i] : parents[1].genes[i]);
            float mutation = GetMutation(genes[i]);
            if (mutation > 0)
            {
                genes[i] = mutation;
            }
        }
        for (int c = 0; c < targetString.Length; c++)
        {
            int mutationString = GetMutation();
            if (mutationString > 0)
            {
                tempValue += availableChars[mutationString];
            }
            else
            {
                tempValue += (Random.Range(0, 2) == 0 ? parents[0].currentStringValue[c] : parents[1].currentStringValue[c]);
            }

        }
        DNA child = new DNA(genes,tempValue);

        return child;
    }
    float GetMutation(float gene)
    {
        if (Random.Range(0.0f, 1.0f) < mutationRate)
        {
            return gene += Random.Range(-0.1f, 0.2f);
        }
        return 0;
    }
    int GetMutation()
    {
        if (Random.Range(0.0f, 1.0f) < mutationRate)
        {
            return Random.Range(0, availableChars.Length);
        }
        return 0;
    }
    bool CalculateSentience()
    {
        averageSentience = 0;
        maxSentience = 0;
        for (int i = 0; i < pop.Count; i++)
        {
            averageSentience += GetSentiencePercentage(GetStringValueSum(pop[i].currentStringValue));

            if (GetSentiencePercentage(GetStringValueSum(pop[i].currentStringValue)) > maxSentience)
            {
                
                maxSentience = GetSentiencePercentage(GetStringValueSum(pop[i].currentStringValue));

            }
            if (pop[i].currentStringValue == targetString)
            {
                Debug.Log("Gained Sentience After " + totalGenerations + " Generations With Total Sum Of " + GetGeneSum(pop[i].genes));
                evolvingPop = i;
                sentient = true;
                return true;

            }
        }


        averageSentience = averageSentience / pop.Count;

        return false;

    }
    float GetSentiencePercentage(float value)
    {
        return (value / targetString.Length) * 100;
    }
    bool IncreasePopulation()
    {
        if (Random.Range(0.0f, 10.0f) < reproductionaRate)
        {
            return true;
        }

        return false;
    }


    public string[] GetTopPops()
    {
        string[] top = new string[5];
        float[] topValues = new float[5];

        for (int i = 0; i < 5; i++)
        {
            top[i] = "";
            topValues[i] = 0;
        }


        for (int i = 0; i < pop.Count; i++)
        {
            for (int j = 0;j < 5; j++)
            {
                if (GetStringValueSum(pop[i].currentStringValue) > topValues[j])
                {
                    topValues[j] = GetStringValueSum(pop[i].currentStringValue);
                    top[j] = pop[i].currentStringValue;
                    break;
                }
            }
        }


        return top;
    }
}
