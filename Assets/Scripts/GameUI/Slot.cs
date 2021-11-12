using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
	private ExecuteCommandsList commandsList;
	private Color invisibleSlotColor;
	private Color visibleSlotColor;

	public GameObject commandOccupySlot {
		get {
            // Kiedy zajęty slot
			if(transform.childCount>0){
				return transform.GetChild(0).gameObject;
			}
            // Kiedy pusty slot
			return null;
		}
	}
	private void Start() {
		invisibleSlotColor = new Color(255f, 255f, 255f, 0f);
		visibleSlotColor = new Color(255f, 255f, 255f, 0.35f);
		commandsList = transform.parent.parent.GetComponent<ExecuteCommandsList>();
	}

	public void UpdateList()
	{
		commandsList.UpdateList();
	}
	public void SetSlotVisiable(bool isVisiable)
	{
		Color slotColor;
		if (isVisiable) {
			slotColor = new Color(255f, 255f, 255f, 0.35f);
		} else {
			slotColor = new Color(255f, 255f, 255f, 0f);
		}
		transform.GetComponent<Image>().color = slotColor;
	}
	public void OnDrop (PointerEventData eventData)
	{	
		if (DragController.commandBeingDragged) {
			GameObject dropedCommend = DragController.commandBeingDragged;
			
			bool isChange = dropedCommend.tag == "CommandToExecute";
			bool isOccupied = commandOccupySlot;
			
			bool sameCommand = false;
			if (isOccupied)
				sameCommand = dropedCommend.name == commandOccupySlot.name;

			if (sameCommand) {
				dropedCommend.name += "_stay";
				return;
			}

			if (isChange)
			{	
				Transform oldParent = dropedCommend.transform.parent;
				Slot oldParentSlot = oldParent.GetComponents<Slot>()[0];

				if (isOccupied)
					commandOccupySlot.transform.SetParent(oldParent);
				else
				{
					oldParentSlot.SetSlotVisiable(true);
					SetSlotVisiable(false);
				}
				dropedCommend.transform.SetParent(transform);
				oldParentSlot.UpdateList();
			}
			else
			{
				transform.GetComponent<Image>().color = invisibleSlotColor;
				GameObject newCommand = Instantiate(dropedCommend);
				newCommand.name = dropedCommend.name;
				newCommand.transform.SetParent(transform);
				newCommand.tag = "CommandToExecute";
				newCommand.GetComponent<CanvasGroup>().blocksRaycasts = true;
				if (isOccupied) 
				{
					commandOccupySlot.name = newCommand.name;
					Destroy(commandOccupySlot);
				}
			}
			GameObject.Find("/Audio/putCommand").GetComponent<AudioSource>().Play();
			UpdateList();
			
		}
	}
}
