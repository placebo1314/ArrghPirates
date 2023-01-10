using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PirateScript : MonoBehaviour
{
    [SerializeField] public string pirateName; // { get; private set; }
    [SerializeField] public string special;
    [SerializeField] public string damage;
    [SerializeField] public string armor;
    [SerializeField] public string watchDistance;
    [SerializeField] public string shootDistance;
    [SerializeField] public string shipNumber;


    public void ChangeShipNumber(string num)
    {
        this.shipNumber = num;
    }
    public PirateModel GetPirate()
    {
        return new PirateModel()
        {
            pirateName = this.pirateName,
            special = this.special,
            damage = this.damage,
            armor = this.armor,
            watchDistance = this.watchDistance,
            shootDistance = this.shootDistance,
            shipNumber = this.shipNumber
        };
    }
    
}
