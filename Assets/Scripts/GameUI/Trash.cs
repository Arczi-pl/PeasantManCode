using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IDropHandler
{
	public void OnDrop (PointerEventData eventData)
	{	
		
		if (DragController.commandBeingDragged.tag == "CommandToExecute")
        {
            Debug.Log("Trash");
            DragController.commandBeingDragged.transform.GetChild(0).gameObject.name = "";
            Destroy(DragController.commandBeingDragged.transform.GetChild(0).gameObject);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(DragController.commandBeingDragged, null, (x,y) => x.HasChanged());
        }
		
	}
}