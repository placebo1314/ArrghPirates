using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipScript : MonoBehaviour
{
    public int position;
    public void DisplayShipBag(bool change)
    {
        var inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        inventory.shipBag.SetActive(true);
        inventory.targetToChange = position;
        if(change)
            inventory.changeShipBag = this.GetComponent<ShipScript>();
    }
}
