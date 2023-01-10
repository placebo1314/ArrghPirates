using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static DataSaver;
using static PlayerStatModel;


public class SaveBtnScript : MonoBehaviour
{

//------from parent GameObject and list
	[SerializeField] private List<GameObject> ships;
	private List<GameObject> pirates;

    void Start()
    {
		GameObject piratesSlot = GameObject.Find("Pirates");
		this.pirates = new List<GameObject>();
		for(int i = 0; i < piratesSlot.transform.childCount; i++)
			this.pirates.Add(piratesSlot.transform.GetChild(i).gameObject );
		
// DebugConsole:
	    Debug.Log("Start SaveBtnScript");
	    PlayerStatModel loadedData = loadData<PlayerStatModel>("stats");
	    int slotNo = 0;
	    //for(var i = 0; i < loadedData.ships.Length; i++)
	    for(var j = 0; j < loadedData.pirates.Count; j++)
		    if (loadedData.pirates[j]["shipNumber"] == "1")
		    {
			    //Vector3 shipSlotVec = GetSonByIndex(ship, 0).GetComponent<RectTransform>().transform.position;
		    }
	    //if(loadedData != null)
	    //{
	    //foreach(var pirate in loadedData.pirates)
	    //GameObject ship = null;
	    //if(loadedData.pirate1 != 0)
	    //{
	    //if(loadedData.pirate1 == 1)
	    //ship = ship1;
	    //else
	    //ship = ship2;
	    //Vector3 shipSlotVec = GetSonByIndex(ship, 0).GetComponent<RectTransform>().transform.position;
	    //pirate1.transform.position = shipSlotVec;
	    //}
	    //}
	    //Debug.Log(shipSlotVec);

	    //if(loadedData.pirate2 != 0)
	    //if(loadedData.pirate3 != 0)
	    //if(loadedData.pirate4 != 0)
	    //Debug.Log(GetSonByIndex(ship1, 1).transform);
    }

    public void SaveData()
    {
	//Get pirates List<dict>
		

		List<Dictionary<string, string>> listOfPirates = new List<Dictionary<string, string>>();
		
		foreach(var i in pirates)
		{
			Dictionary<string, string> subResult = new Dictionary<string, string>();
			PirateScript pirateScript = i.GetComponent<PirateScript>();
			subResult["pirateName"] = pirateScript.pirateName;
			subResult["shipNumber"] = pirateScript.GetShipNumber();
			subResult["special"] = pirateScript.GetSpecial();
			subResult["damage"] = pirateScript.GetDamage();
			subResult["armor"] = pirateScript.GetArmor();
			subResult["watchDistance"] = pirateScript.GetWatchDistance();
			subResult["shootDistance"] = pirateScript.GetShootDistance();
			listOfPirates.Add(subResult);
		}
		foreach(var i in ships)
		{
			Dictionary<string, string> subResult = new Dictionary<string, string>();
			ShipScript shipScript = i.GetComponent<ShipScript>();
			subResult["shipNumber"] = shipScript.GetShipNumber();
			subResult["shipNumber"] = pirateScript.GetShipNumber();
			subResult["special"] = pirateScript.GetSpecial();
			subResult["damage"] = pirateScript.GetDamage();
			subResult["armor"] = pirateScript.GetArmor();
			subResult["watchDistance"] = pirateScript.GetWatchDistance();
			listOfPirates.Add(subResult);
		}
		PlayerStatModel saveData = new PlayerStatModel(listOfPirates);
		DataSaver.saveData(saveData, "stats");
    }

      //Needed ? V
	private GameObject GetSonByIndex(GameObject obj, int index)
	{
  		int i = 0;
  		foreach(Transform tfm in obj.transform)
  		{
    		if(i == index)
      			return tfm.gameObject;
    		i++;
  		}
  		return null;
	}
	
}
