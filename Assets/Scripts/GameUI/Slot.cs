using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
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

	public void OnDrop (PointerEventData eventData)
	{	
		bool isChange = DragController.commandBeingDragged.tag == "CommandToExecute";
		bool isOccupied = commandOccupySlot;
		
		if (isChange)
		{
			DragController.commandBeingDragged.transform.GetChild(0).gameObject.transform.SetParent(transform);
			if (isOccupied)
			{
				commandOccupySlot.transform.SetParent(DragController.commandBeingDragged.transform);
			}
		} 
		else
		{
			GameObject newCommandToExecute = Instantiate(DragController.commandBeingDragged);
			newCommandToExecute.name = DragController.commandBeingDragged.name;
			newCommandToExecute.transform.SetParent(transform);
			if (isOccupied) 
			{
				commandOccupySlot.name = newCommandToExecute.name;
				Destroy(commandOccupySlot);
			}
		} 

		ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged());
	}
}
