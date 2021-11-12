using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject commandBeingDragged;
	private Vector3 startPosition;
	private Transform startParent;
	private GameController gc;

	private  void Awake() {
		gc = GameObject.Find("/GameController").GetComponent<GameController>();
	}
	public void OnBeginDrag (PointerEventData eventData)
	{	
		if (gc.isLevelStart)
			return;
		commandBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

	}

	public void OnDrag (PointerEventData eventData)
	{	
		if (gc.isLevelStart)
			return;
		transform.position = eventData.position;
	
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		if (gc.isLevelStart)
			return;
		bool stay = commandBeingDragged.name.EndsWith("_stay");
		if (stay)
			commandBeingDragged.name = commandBeingDragged.name.Substring(0, commandBeingDragged.name.Length-5);
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == startParent)
		{
			if (tag == "CommandToPick" || stay) 
				transform.position = startPosition;
			else {
				startParent.GetComponent<Slot>().SetSlotVisiable(true);
				Destroy(gameObject);
				GameObject.Find("/Audio/deleteCommand").GetComponent<AudioSource>().Play();
			}
		}
		commandBeingDragged = null;
	}
}
