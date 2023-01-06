using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataSaver;
using static PlayerStatModel;


public class SaveBtnScript : MonoBehaviour
{

    [SerializeField] private GameObject pirate1; //------from parent GameObject
    [SerializeField] private GameObject pirate2;
    [SerializeField] private GameObject pirate3;
    [SerializeField] private GameObject pirate4;

    public void SaveData()
    {
        PlayerStatModel saveData = new PlayerStatModel(pirate1.GetComponent<PirateScript>().GetShipNumber(),
            pirate2.GetComponent<PirateScript>().GetShipNumber(), pirate3.GetComponent<PirateScript>().GetShipNumber(),
            pirate4.GetComponent<PirateScript>().GetShipNumber());

        // DebugConsole:
        Debug.Log(pirate1.GetComponent<PirateScript>().GetShipNumber());
        
        DataSaver.saveData(saveData, "stats");
    }
}
