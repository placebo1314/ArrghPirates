using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    [SerializeField] public GameObject mainParentObject;
    private TopTextScript textScript;

    void Awake()
    {
        textScript =  GameObject.Find("TopText").GetComponent<TopTextScript>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var targetSlot = gameObject;
        if (eventData.pointerDrag != null && targetSlot.transform.childCount == 0)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
//Why better the var?
        var pirate = eventData.pointerDrag.GetComponent<PirateScript>();
            eventData.pointerDrag.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        
            pirate.ChangeShipNumber(mainParentObject.GetComponent<ShipScript>().Ship.shipId);
        }
        else
        {
            StartCoroutine(textScript.ChangeTextWithTime("Can't do this !", 3));
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("Content").GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.transform.SetParent(GameObject.Find("Content").transform);
        }
    }
}
