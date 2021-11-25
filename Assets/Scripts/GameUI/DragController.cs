using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject CommandBeingDragged;
	private Vector3 _startPosition;
	private Transform _startParent;
	private GameController _gameController;

	private void Awake() {
		_gameController = GameObject.Find("/GameController").GetComponent<GameController>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{	
		if (_gameController.GetIsLevelStart())
			return;
		CommandBeingDragged = gameObject;
		_startPosition = transform.position;
		_startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

	}

	public void OnDrag(PointerEventData eventData)
	{	
		if (_gameController.GetIsLevelStart())
			return;
		transform.position = eventData.position;
	
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (_gameController.GetIsLevelStart())
			return;
		bool stay = CommandBeingDragged.name.EndsWith("_stay");
		if (stay)
			CommandBeingDragged.name = CommandBeingDragged.name.Substring(0, CommandBeingDragged.name.Length-5);
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == _startParent)
		{
			if (tag == "CommandToPick" || stay) 
				transform.position = _startPosition;
			else {
				_startParent.GetComponent<Slot>().SetSlotVisiable(true);
				Destroy(gameObject);
				GameObject.Find("/Audio/deleteCommand").GetComponent<AudioSource>().Play();
			}
		}
		CommandBeingDragged = null;
	}
}
