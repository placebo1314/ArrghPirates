using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerStatModel
{
    public int pirate1; //PRIVATE ! ! !
    public int pirate2;
    public int pirate3;
    public int pirate4;
    
        
    public PlayerStatModel(int pirate1, int pirate2, int pirate3, int pirate4)
    {
        this.pirate1 = pirate1;
        this.pirate2 = pirate2;
        this.pirate3 = pirate3;
        this.pirate4 = pirate4;
    }
    
}
