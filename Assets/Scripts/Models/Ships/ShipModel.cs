using Random=UnityEngine.Random;
using System.IO;
using System;

using static Enums;

public abstract class ShipModel
{
    public string shipId;
    public string shipName;
    public string damage;
    public string armor;
    public string watchDistance;
    public string shootDistance;
    public ShipType shipType;
    public int Speed;
    public int dockNumber;

    public ShipModel()
    {
        shipId = Guid.NewGuid().ToString();
        shipName = GetShipName();
        dockNumber = -1;
    }
    
    protected virtual string GetShipName()
    {
        string file = "Assets/Names/ShipNames.txt";
        string[] names = File.ReadAllLines(file);
        return names[Random.Range(0, names.Length)];
    }
    
}
