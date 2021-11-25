using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
	private ExecuteCommandsList _commandsList;
	private Color _invisibleSlotColor, _visibleSlotColor;

	private void Start() 
	{
		_invisibleSlotColor = new Color(255f, 255f, 255f, 0f);
		_visibleSlotColor = new Color(255f, 255f, 255f, 0.35f);
		_commandsList = transform.parent.parent.GetComponent<ExecuteCommandsList>();
	}

	public GameObject GetCommandOccupySlot()
	{
		if(transform.childCount > 0)
			return transform.GetChild(0).gameObject;
		return null;
	}

	public void UpdateList()
	{
		_commandsList.UpdateList();
	}

	public void SetSlotVisiable(bool isVisiable)
	{
		Color slotColor;
		if (isVisiable)
			slotColor = _visibleSlotColor;
		else
			slotColor = _invisibleSlotColor;
		transform.GetComponent<Image>().color = slotColor;
	}

	public void OnDrop (PointerEventData eventData)
	{	
		if (DragController.CommandBeingDragged) {
			GameObject dropedCommend = DragController.CommandBeingDragged;
			
			bool isChange = dropedCommend.tag == "CommandToExecute";
			bool isOccupied = !(GetCommandOccupySlot() is null);
			
			bool sameCommand = false;
			if (isOccupied)
				sameCommand = dropedCommend.name == GetCommandOccupySlot().name;

			if (sameCommand) 
			{
				dropedCommend.name += "_stay";
				return;
			}

			// if droped command was in process already
			if (isChange)
			{	
				Transform oldParent = dropedCommend.transform.parent;
				Slot oldParentSlot = oldParent.GetComponents<Slot>()[0];

				if (isOccupied)
					// if drop on an occupied slot then swap commands
					GetCommandOccupySlot().transform.SetParent(oldParent);
				else
				{
					// else change command slot
					oldParentSlot.SetSlotVisiable(true);
					SetSlotVisiable(false);
				}
				dropedCommend.transform.SetParent(transform);
				oldParentSlot.UpdateList();
			}
			else
			{	
				// if picked new command then create one and put to slot
				SetSlotVisiable(false);
				GameObject newCommand = Instantiate(dropedCommend);
				newCommand.name = dropedCommend.name;
				newCommand.transform.SetParent(transform);
				newCommand.tag = "CommandToExecute";
				newCommand.GetComponent<CanvasGroup>().blocksRaycasts = true;
				if (isOccupied) 
				{
					// if drop on an occupied slot then delete old command
					GetCommandOccupySlot().name = newCommand.name;
					Destroy(GetCommandOccupySlot());
				}
			}
			GameObject.Find("/Audio/putCommand").GetComponent<AudioSource>().Play();
			UpdateList();
		}
	}
}
