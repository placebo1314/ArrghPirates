using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipScript : MonoBehaviour
{
    [SerializeField] public string shipNumber { get; private set; }
    [SerializeField] public string shipName { get; private set; }
    [SerializeField] public string damage { get; private set; }
    [SerializeField] public string armor { get; private set; }
    [SerializeField] public string watchDistance { get; private set; }
    [SerializeField] public string shootDistance { get; private set; }

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
        this.damage -= Int32.Parse(damage);
        this.armor -= Int32.Parse(armor);
        this.watchDistance -= Int32.Parse(watchDistance);
        this.shootDistance -= Int32.Parse(shootDistance);
        // DebugConsole:
        Debug.Log("Ship stats restored ! ");
    }

    public string GetShipNumber()
    {
        return this.shipNumber;
    }
}
