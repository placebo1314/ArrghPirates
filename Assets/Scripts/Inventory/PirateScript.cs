using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateScript : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int armor;
    [SerializeField] private int watchDistance;
    [SerializeField] private int shootDistance;
    [SerializeField] private int shipNumber;
    
    
    public int GetDamage()
    {
        return this.damage;
    }
    public int GetArmor()
    {
        return this.armor;
    }
    public int GetWatchDistance()
    {
        return this.watchDistance;
    }
    public int GetShootDistance()
    {
        return this.shootDistance;
    }
    public void ChangeShipNumber(int num)
    {
        this.shipNumber = num;
    }
    public int GetShipNumber()
    {
        return this.shipNumber;
    }
}
