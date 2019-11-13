using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarInfo : MonoBehaviour
{

    public char type;
    public int heat;

    public void SetUpStar(char _type,int _heat)
    {
        type = _type;
        heat = _heat;
    }
	
}
