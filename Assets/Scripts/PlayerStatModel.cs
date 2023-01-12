using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerStatModel
{
	public List<PirateModel> pirates = new List<PirateModel>();
	public List<ShipModel> ships = new List<ShipModel>();
}
