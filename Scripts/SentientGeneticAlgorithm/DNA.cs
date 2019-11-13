using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DNA {

    public string currentStringValue;
    public float[] genes;
    //public float sentience, intelligence,strength, speed, adaptability;

    public DNA(float[] _genes,string _currentStringValue)
    {
        genes = _genes;
        currentStringValue = _currentStringValue;
    }


}
