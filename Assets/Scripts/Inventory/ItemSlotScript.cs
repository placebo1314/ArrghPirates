using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropppppp ! ! ! ! !");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }

        GameObject ship = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
        PirateScript pirate = eventData.pointerDrag.GetComponent<PirateScript>();
        eventData.pointerDrag.GetComponent<RectTransform>().anchorMin =  new Vector2(0.5f, 0.5f);
        eventData.pointerDrag.GetComponent<RectTransform>().anchorMax =  new Vector2(0.5f, 0.5f);

        //ship.GetComponent<ShipScript>().SetValues(pirate.damage, pirate.armor, pirate.GetWatchDistance(), pirate.GetShootDistance());
        pirate.ChangeShipNumber(ship.GetComponent<ShipScript>().shipNumber);
    }
}
