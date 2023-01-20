using UnityEngine;
using System;

using static Enums;

public class ThreeMastedShip : ShipModel
{
    public ThreeMastedShip()
    {
        shipType = ShipType.ThreeMastedShip;
        damage = "5";
        armor = "5";
        watchDistance = "6";
        shootDistance = "4";
        Speed = 2;
    }
}
