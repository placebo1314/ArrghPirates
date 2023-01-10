using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static DataSaver;
using static PlayerStatModel;


public class SaveBtnScript : MonoBehaviour
{

//------from parent GameObject and list
	private List<GameObject> ships;
	private List<GameObject> pirates;

    void Start()
    {
		GameObject piratesSlot = GameObject.Find("Pirates");
		this.pirates = new List<GameObject>();
		for(int i = 0; i < piratesSlot.transform.childCount; i++)
			this.pirates.Add(piratesSlot.transform.GetChild(i).gameObject );
		GameObject shipsSlot = GameObject.Find("ShipSlots");
		this.ships = new List<GameObject>();
		for(int i = 0; i < shipsSlot.transform.childCount; i++)
			this.ships.Add(shipsSlot.transform.GetChild(i).gameObject );

	    PlayerStatModel loadedData = loadData<PlayerStatModel>("stats");
		foreach (GameObject ship in ships)
		{
			int slotNo = 0;
			foreach (PirateModel pirate in loadedData.pirates)
			{
                if (pirate.shipNumber != "")
				{
					var pirateGameObject = GameObject.Find(pirate.pirateName);
					if (pirate.shipNumber == ship.GetComponent<ShipScript>().shipNumber)
					{
						Vector3 shipSlotVec = GetSonByIndex(ship, slotNo).GetComponent<RectTransform>().transform.position;
						pirateGameObject.transform.position = shipSlotVec;
						slotNo++;
					}
				}
			}
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
	    PlayerStatModel saveData = new PlayerStatModel();
		
		foreach(var i in pirates)
		{
			PirateScript pirateScript = i.GetComponent<PirateScript>();
			saveData.pirates.Add(pirateScript.GetPirate());

		}
		foreach(var i in ships)
		{
			ShipScript shipScript = i.GetComponent<ShipScript>();
			saveData.ships.Add(shipScript.GetShip());
		}
		
		DataSaver.saveData(saveData, "stats");
    }
    
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
