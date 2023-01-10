using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateScript : MonoBehaviour
{
    
    [SerializeField] public string pirateName { get; private set; }
    [SerializeField] public string special { get; private set; }
    [SerializeField] public string damage { get; private set; }
    [SerializeField] public string armor { get; private set; }
    [SerializeField] public string watchDistance { get; private set; }
    [SerializeField] public string shootDistance { get; private set; }
    [SerializeField] public string shipNumber { get; private set; }


    public void ChangeShipNumber(int num)
    {
        this.shipNumber = num.ToString();
    }
}
