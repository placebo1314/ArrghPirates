using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    //public PirateScript pirateScript;
    public CanvasGroup canvasGroup;
    private RectTransform rectTransform;


    private void Awake()
    {
        //pirateScript = GetComponent<PirateScript>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.pointerCurrentRaycast.gameObject.GetComponent<PirateScript>().ChangeShipNumber("");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        eventData.pointerCurrentRaycast.gameObject.transform.SetParent(canvas.GetComponent<Transform>());
        Debug.Log("OnBeginDrag Object");
        Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        Debug.Log("Object Parent");
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.parent);
        Debug.Log("eventData");
        
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
