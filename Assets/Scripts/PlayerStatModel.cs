using System.Collections.Generic;
using System;

[Serializable]
public class PlayerStatModel
{
	public List<PirateModel> pirates = new List<PirateModel>();
	public List<ShipModel> ships = new List<ShipModel>();
}
