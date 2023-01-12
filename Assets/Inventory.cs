using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DataSaver;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private List<GameObject> ships;
    private List<GameObject> pirates;

    void Awake()
    {
        this.pirates = new List<GameObject>();
        this.ships = new List<GameObject>();

        PlayerStatModel data = loadData<PlayerStatModel>("stats");

        Transform content = GameObject.Find("Content").transform;
        Transform itemBag = GameObject.Find("ItemBag").transform;

        GameObject shipsSlot = GameObject.Find("ShipSlots");

        //if (newPirate.TryGetComponent<ScrollItemView>(out ScrollItemView item))
        //item.ChangePirate(pirate);
        GameObject newPirate;
        List<PirateModel> shiplesses = data.pirates.Where(pirate => pirate.shipId == "").ToList();
        foreach (PirateModel pirate in shiplesses)
        {
            newPirate = Instantiate(prefab, content);
            newPirate.GetComponent<PirateScript>().Pirate = pirate;
        }

        foreach (GameObject ship in ships)
        {
            int slotNo = 0;
            string shipNumber = ship.GetComponent<ShipScript>().Ship.shipId;
            List<PirateModel> crewMembers = data.pirates.Where(pirate => pirate.shipId == shipNumber).ToList();
            

            foreach (PirateModel member in crewMembers)
            {
                newPirate = Instantiate(prefab, itemBag);

                Vector3 shipSlotPosition = GetSonByIndex(ship, slotNo).GetComponent<RectTransform>().transform.position;
                newPirate.transform.position = shipSlotPosition;
                // anchor ?
                newPirate.GetComponent<PirateScript>().Pirate = member;
                slotNo++;
            }
                        //pirateGameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                        //pirateGameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

        }
    }

    public void SaveData()
    {
        //From Content:
        GameObject piratesSlot = GameObject.Find("Content");
        for (int i = 0; i < piratesSlot.transform.childCount; i++)
            this.pirates.Add(piratesSlot.transform.GetChild(i).gameObject);
        //From ItemBag:
        piratesSlot = GameObject.Find("ItemBag");
        for (int i = 0; i < piratesSlot.transform.childCount; i++)
            this.pirates.Add(piratesSlot.transform.GetChild(i).gameObject);
        PlayerStatModel saveData = new PlayerStatModel();

        foreach (var i in pirates)
        {
            PirateScript pirateScript = i.GetComponent<PirateScript>();
            saveData.pirates.Add(pirateScript.Pirate);
        }
        foreach (var i in ships)
        {
            ShipScript shipScript = i.GetComponent<ShipScript>();
            saveData.ships.Add(shipScript.Ship);
        }

        DataSaver.saveData(saveData, "stats");
    }

    private GameObject GetSonByIndex(GameObject obj, int index)
    {
        int i = 0;
        foreach (Transform tfm in obj.transform)
        {
            if (i == index)
                return tfm.gameObject;
            i++;
        }
        return null;
    }
}
