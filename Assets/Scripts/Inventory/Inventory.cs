using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DataSaver;
using UnityEngine.UI;
using System;
using System.IO;

using static Enums;

public class Inventory : MonoBehaviour
{
	[SerializeField] public GameObject PiratePrefab;// = Resources.Load("Prefabs/Pirates/Pirate") as GameObject;
    [SerializeField] public GameObject ThreeMastedShipPrefab;// = Resources.Load("Prefabs/Pirates/ThreeMastedDockSlot") as GameObject;
    [SerializeField] public GameObject SingleMastedShipPrefab;// = Resources.Load("Prefabs/Pirates/SingleMastedDockSlot") as GameObject;
    [SerializeField] public GameObject EmptyShipPrefab;// = Resources.Load("Prefabs/Pirates/EmptyDockSlot") as GameObject;
    [SerializeField] public GameObject DocklessSingleMastedShipPrefab;// = Resources.Load("Prefabs/Ships/DocklessSingleMastedShip") as GameObject;
    [SerializeField] public GameObject DocklessThreeMastedShipPrefab;// =
        //Resources.Load("Prefabs/Ships/DocklessThreeMastedShip") as GameObject;
    [SerializeField] public GameObject shipBag;// = GameObject.Find("ShipBag");
    [SerializeField] public GameObject pirateBag;// = GameObject.Find("PirateBag");
    [SerializeField] public Transform pirateBagTransform;// = GameObject.Find("PirateBag").transform;
    [SerializeField] public Transform contentTransform;// = GameObject.Find("Content").transform;
    [SerializeField] public GameObject Dock;// = GameObject.Find("Dock");

    private List<GameObject> ships { get; set; }
    private List<GameObject> pirates { get; set; }
    public ShipScript changeShipBag { get; set; }
    public int targetToChange { get; set; }

    void Awake()
    {
        changeShipBag = null;
        shipBag.SetActive(false);

        this.ships = new List<GameObject>();

        PlayerStatModel data = LoadData<PlayerStatModel>("stats");
        //TODO: Pass only the necessary data
        SetupDocks(data);
        SetupShipBag(data);
        SetupShiplessPirates(data);
        SetupOnBoardPirates(data);
    }

    private void SetupDocks(PlayerStatModel data)
    {
        GameObject prefab;
        for (int i = 0; i < data.DockSize; i++)
        {
            var target = data.ships.Where(x => x.dockNumber == i ).ToList();
            if (target.Count == 1)
            {
                if (target[0].shipType == ShipType.ThreeMastedShip)
                    prefab = ThreeMastedShipPrefab;
                else if (target[0].shipType == ShipType.SingleMastedShip)
                    prefab = SingleMastedShipPrefab;
                else
                    prefab = EmptyShipPrefab;

                GameObject newShip = Instantiate(prefab, Dock.transform);
                newShip.GetComponent<ShipScript>().Ship = target[0];
                newShip.name = target[0].shipName;
                ships.Add(newShip);
            }
            else
                newShip = Instantiate(EmptyShipPrefab, Dock.transform);
            newShip.GetComponent<ChangeShipScript>().position = i;
        }
    }

    private void SetupShipBag(PlayerStatModel data)
    {
        GameObject prefab;
        var shipsInBag = data.ships.Where(x => x.dockNumber == -1).ToList();
        
        foreach(ShipModel ship in shipsInBag)
        {
            if(ship.shipType == ShipType.ThreeMastedShip)
                prefab = DocklessThreeMastedShipPrefab;
            else //if(ship.shipType == "SingleMasted")
                prefab = DocklessSingleMastedShipPrefab;
            GameObject newShip = Instantiate(prefab, shipBag.transform);
            newShip.GetComponent<ShipScript>().Ship = ship;
            newShip.name = ship.shipName;
            ships.Add(newShip);
            //fill slots
            //for(int _ = 0; n < ship.crew; _++)
            //GameObject slot = gun.transform.Find("magazine/ammo");
            //string filename = "Assets/Images/image_001_0000-removebg";
            //this.ships.Add(newShip);
            //newShip.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load(filename) as Sprite;
        }
        shipBag.SetActive(false);
    }

    private void SetupShiplessPirates(PlayerStatModel data)
    {
        GameObject newPirate;
        List<PirateModel> shiplesses = data.pirates.Where(pirate => pirate.shipId == "" || pirate.shipId == null).ToList();
        foreach (PirateModel pirate in shiplesses)
        {
            newPirate = Instantiate(PiratePrefab, contentTransform);
            newPirate.GetComponent<PirateScript>().Pirate = pirate;
            newPirate.name = newPirate.GetComponent<PirateScript>().Pirate.pirateName;
        }
    }

    private void SetupOnBoardPirates(PlayerStatModel data)
    {
        var c = 1;
    }

    public void SaveData()
    {
        this.pirates = new List<GameObject>();

        GameObject piratesSlot = contentTransform.gameObject;
        for (int i = 0; i < piratesSlot.transform.childCount; i++)
            this.pirates.Add(piratesSlot.transform.GetChild(i).gameObject);

        piratesSlot = pirateBag;
        for (int i = 0; i < piratesSlot.transform.childCount; i++)
            this.pirates.Add(piratesSlot.transform.GetChild(i).gameObject);
        
        //CollectShips : 
        this.ships = new List<GameObject>();
        GameObject ShipSlot = shipBag;
        for (int i = 0; i < ShipSlot.transform.childCount; i++)
            this.ships.Add(ShipSlot.transform.GetChild(i).gameObject);
        
        ShipSlot = Dock;
        for (int i = 0; i < ShipSlot.transform.childCount; i++)
            if(!ShipSlot.transform.GetChild(i).gameObject.name.Contains("EmptyDock"))
                this.ships.Add(ShipSlot.transform.GetChild(i).gameObject);
        
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

        saveData.DockSize = 2;
        saveData.Lvl = 1;

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
