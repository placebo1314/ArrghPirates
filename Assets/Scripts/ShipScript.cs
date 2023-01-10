using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipScript : MonoBehaviour
{
    [SerializeField] public string shipNumber; // { get; private set; }
    [SerializeField] public string shipName;
    [SerializeField] public string damage;
    [SerializeField] public string armor;
    [SerializeField] public string watchDistance;
    [SerializeField] public string shootDistance;

    public ShipModel GetShip()
    {
        return new ShipModel()
        {
            shipName = this.shipName,
            damage = this.damage,
            armor = this.armor,
            watchDistance = this.watchDistance,
            shootDistance = this.shootDistance,
            shipNumber = this.shipNumber
        };
    }

    public void SetValues(string damage, string armor, string watchDistance, string shootDistance)
    {
        this.damage += Int32.Parse(damage);
        this.armor += Int32.Parse(armor);
        this.watchDistance += Int32.Parse(watchDistance);
        this.shootDistance += Int32.Parse(shootDistance);
        // DebugConsole:
        Debug.Log("Ship stats updated ! ");
    }

    public void RestoreValues(string damage, string armor, string watchDistance, string shootDistance)
    {
        this.damage = (Int32.Parse(this.damage) - Int32.Parse(damage)).ToString();
        this.armor = (Int32.Parse(this.armor) - Int32.Parse(armor)).ToString();
        this.watchDistance = (Int32.Parse(this.watchDistance) - Int32.Parse(watchDistance)).ToString();
        this.shootDistance = (Int32.Parse(this.shootDistance) - Int32.Parse(shootDistance)).ToString();
        // DebugConsole:
        Debug.Log("Ship stats restored ! ");
    }

    public string GetShipNumber()
    {
        return this.shipNumber;
    }
}
