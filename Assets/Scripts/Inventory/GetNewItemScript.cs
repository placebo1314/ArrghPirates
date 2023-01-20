using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System.IO;

using static Enums;

public class GetNewItemScript : MonoBehaviour
{
    public void GetShip()
    {
        Transform tf = GameObject.Find("Inventory").GetComponent<Inventory>().shipBag.transform;
        GameObject newShip = Instantiate(GameObject.Find("Inventory").GetComponent<Inventory>().DocklessThreeMastedShipPrefab, tf);
        newShip.GetComponent<ShipScript>().Ship = new ThreeMastedShip();
        string newName = GetRandomName("Ship");

        newShip.GetComponent<ShipScript>().Ship.shipName = newName;
        newShip.GetComponent<ShipScript>().Ship.shipType = ShipType.ThreeMastedShip;

        newShip.name = newName;
    }

    public void GetPirate()
    {
        Transform tf = GameObject.Find("Content").transform;
        GameObject newPirate = Instantiate(GameObject.Find("Inventory").GetComponent<Inventory>().PiratePrefab, tf);
        string newName = GetRandomName("Sailor");
        newPirate.GetComponent<PirateScript>().Pirate.pirateName = newName;
        newPirate.name = newName;
        Debug.Log("New Pirate");
    }

    private string GetRandomName(string type = "Captain")
    {
        string file;
        switch (type)
        {
            case "Captain":
                file = "Assets/Names/CaptainNames.txt";
                break;
            case "Ship":
                file = "Assets/Names/ShipNames.txt";
                break;
            default:
                file = "Assets/Names/Names.txt";
                break;
        }
        string[] names = File.ReadAllLines(file);
        return names[Random.Range(0, names.Length)];
    }

}
