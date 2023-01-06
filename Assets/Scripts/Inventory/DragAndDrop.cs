using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private PirateScript pirateScript;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if has Ship kimínuszolni ! ! ! ...???
        eventData.pointerCurrentRaycast.gameObject.GetComponent<PirateScript>().ChangeShipNumber(0);
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
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