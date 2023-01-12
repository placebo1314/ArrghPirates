using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random=UnityEngine.Random;

public class GetPirateScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void GetPirate()
    {
        Transform tf = GameObject.Find("Content").transform;
        GameObject newPirate = Instantiate(prefab, tf);
        string newName = GetPirateName();
        newPirate.GetComponent<PirateScript>().Pirate.pirateName = newName;
        newPirate.name = newName;
        
    }

    private string GetPirateName(string type = "Captain")
    {
        string file;
        switch (type)
        {
            case "Captain":
                file = "Assets/Names/CaptainNames.txt";
                break;
            default:
                file = "Assets/Names/Names.txt";
                break;
        }
        string[] names = File.ReadAllLines(file);
        return names[Random.Range(0, names.Length)];
    }
}
