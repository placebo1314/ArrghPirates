using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    private RectTransform _rectTransform;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //this.gameObject.GetComponent<PirateScript>().Pirate.shipId = "";
        //eventData.pointerCurrentRaycast.gameObject.GetComponent<PirateScript>().ChangeShipNumber("");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        eventData.pointerCurrentRaycast.gameObject.transform.SetParent(GameObject.Find("PirateBag").GetComponent<Transform>());
        
//DebugLog : 
        Debug.Log("OnBeginDrag Object");
        Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        Debug.Log("Object Parent");
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.parent);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        if(eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlotScript>() == null)
            eventData.pointerDrag.transform.SetParent(GameObject.Find("Content").transform);
    }
}
