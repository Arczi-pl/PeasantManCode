using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject commandBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	public void OnBeginDrag (PointerEventData eventData)
	{	
		commandBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

	}



	public void OnDrag (PointerEventData eventData)
	{	
		transform.position = eventData.position;
	
	}



	public void OnEndDrag (PointerEventData eventData)
	{
		bool stay = commandBeingDragged.name.EndsWith("_stay");
		if (stay) {
			commandBeingDragged.name = commandBeingDragged.name.Substring(0, commandBeingDragged.name.Length-5);
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == startParent)
		{
			if (tag == "CommandToPick" || stay) {
				
				transform.position = startPosition;
			} else {
				startParent.GetComponent<Slot>().SetSlotVisiable(true);
				Destroy(gameObject);
				GameObject.Find("/Audio/deleteCommand").GetComponent<AudioSource>().Play();
			}
			
		}
		commandBeingDragged = null;
	}



}
