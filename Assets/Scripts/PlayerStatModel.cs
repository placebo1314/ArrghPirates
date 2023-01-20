using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerStatModel
{
	public List<PirateModel> pirates = new();
	public List<ShipModel> ships = new();
	public int DockSize;
	public int Lvl;
}
