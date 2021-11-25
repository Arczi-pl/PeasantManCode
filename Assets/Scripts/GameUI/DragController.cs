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
		// cant drag commands after start
		if (_gameController.GetIsLevelStart())
			return;
		CommandBeingDragged = gameObject;
		_startPosition = transform.position;
		_startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

	}

	public void OnDrag(PointerEventData eventData)
	{	
		// cant drag commands after start
		if (_gameController.GetIsLevelStart())
			return;
		transform.position = eventData.position;
	
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// cant drag commands after start
		if (_gameController.GetIsLevelStart())
			return;
		bool stay = CommandBeingDragged.name.EndsWith("_stay");
		if (stay)
			// if command end with _stay then delete _stay
			CommandBeingDragged.name = CommandBeingDragged.name.Substring(0, CommandBeingDragged.name.Length-5);
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		// if same parent as before drag
		if(transform.parent == _startParent)
		{
			// if command to pick or with _stay
			if (tag == "CommandToPick" || stay)
				// move command to start possition
				transform.position = _startPosition;
			else {
				// else delete command
				_startParent.GetComponent<Slot>().SetSlotVisiable(true);
				Destroy(gameObject);
				GameObject.Find("/Audio/deleteCommand").GetComponent<AudioSource>().Play();
			}
		}
		CommandBeingDragged = null;
	}
}
