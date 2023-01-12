using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel
{
    public static int ID = 0;
    [SerializeField] public string shipId;
    public string shipName;
    public string damage;
    public string armor;
    public string watchDistance;
    public string shootDistance;
    public ShipModel()
    {
        shipName = "";
        damage = "";
        armor = "";
        watchDistance = "";
        shootDistance = "";
        shipId = (++ID).ToString();
    }
}
