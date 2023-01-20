using UnityEngine;


public class ShipScript : MonoBehaviour
{
    public ShipModel Ship;

    public void ShipToDock()
    { 
//DebugLog :
        Debug.Log("ShipToDock : ");

        var inventory =GameObject.Find("Inventory").GetComponent<Inventory>();
        var targetDock = inventory.targetToChange;
        var dock = GameObject.Find("Dock");
        GameObject prefab;

        if (Ship.shipType == Enums.ShipType.SingleMastedShip)
            prefab = Resources.Load("Prefabs/Ships/SingleMastedDockSlot") as GameObject;
        else //if(.Ship.shipType == Enums.ShipType.ThreeMastedShip)
            prefab = Resources.Load("Prefabs/Ships/ThreeMastedDockSlot") as GameObject;

        var newShip = Instantiate(prefab, dock.transform);
        newShip.GetComponent<ShipScript>().Ship = this.Ship;
        newShip.name = Ship.shipName;
        newShip.GetComponent<ShipScript>().Ship.dockNumber = targetDock;
        
        Destroy(gameObject);
        
        //delete empty prefab and positioning the new ship in hierarchy
        Destroy(dock.transform.GetChild(targetDock).gameObject);
        newShip.transform.SetSiblingIndex(targetDock);
        inventory.shipBag.SetActive(false);
        
        //If change, exchange ships
        if (inventory.changeShipBag != null)
        {
            if(inventory.changeShipBag.GetComponent<ShipScript>().Ship.shipType == Enums.ShipType.SingleMastedShip)
                prefab = Resources.Load("Prefabs/Ships/DocklessSingleMastedShip") as GameObject;
            else //if 
                prefab = Resources.Load("Prefabs/Ships/DocklessThreeMastedShip") as GameObject;
            newShip = Instantiate(prefab, inventory.shipBag.transform);
            newShip.GetComponent<ShipScript>().Ship = inventory.changeShipBag.Ship;
            newShip.GetComponent<ShipScript>().Ship.dockNumber = -1;
            newShip.name = inventory.changeShipBag.Ship.shipName;
        }
        inventory.changeShipBag = null;
    }
}
