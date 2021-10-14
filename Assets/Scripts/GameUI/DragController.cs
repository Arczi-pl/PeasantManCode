using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject commandBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	public void OnBeginDrag (PointerEventData eventData)
	{	
		if(transform.childCount > 0 | tag == "CommandToPick") {
			commandBeingDragged = gameObject;
			startPosition = transform.position;
			startParent = transform.parent;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}



	public void OnDrag (PointerEventData eventData)
	{	
		if(transform.childCount > 0 | tag == "CommandToPick") {
			transform.position = eventData.position;
		}
	}



	public void OnEndDrag (PointerEventData eventData)
	{
		
		commandBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == startParent){
			transform.position = startPosition;
		}
		
	}



}
