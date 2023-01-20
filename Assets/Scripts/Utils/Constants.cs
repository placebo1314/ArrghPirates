using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
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

}
