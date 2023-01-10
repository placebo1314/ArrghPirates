using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerStatModel
{
    //PRIVATE ! ! !
	public List<Dictionary<string, string>> pirates = new List<Dictionary<string, string>>();
	public List<Dictionary<string, string>> ships = new List<Dictionary<string, string>>();
    
        
    public PlayerStatModel(List<Dictionary<string, string>> listOfPirates, List<Dictionary<string, string>> inputShips) 
    {
		foreach(var ship in inputShips)
			this.ships.Add(ship);
		foreach(var pirate in listOfPirates)
			this.pirates.Add(pirate);
		
    }
    public PlayerStatModel()
    {
    }
    
}
