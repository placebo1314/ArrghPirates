using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private int shipNumber;
    [SerializeField] private int damage;
    [SerializeField] private int armor;
    [SerializeField] private int watchDistance;
    [SerializeField] private int shootDistance;

    public void SetValues(int damage, int armor, int watchDistance, int shootDistance)
    {
        this.damage += damage;
        this.armor += armor;
        this.watchDistance += watchDistance;
        this.shootDistance += shootDistance;
        // DebugConsole:
        Debug.Log("Ship stats updated ! ");
    }

    public void RestoreValues(int damage, int armor, int watchDistance, int shootDistance)
    {
        this.damage -= damage;
        this.armor -= armor;
        this.watchDistance -= watchDistance;
        this.shootDistance -= shootDistance;
        // DebugConsole:
        Debug.Log("Ship stats restored ! ");
    }

    public int GetShipNumber()
    {
        return this.shipNumber;
    }
}
