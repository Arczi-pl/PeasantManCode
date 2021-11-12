using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExecuteCommandsList : MonoBehaviour {
	public Text inventoryText;

	void Start () {
		UpdateList();
	}
	
	public void UpdateList ()
	{	
		string commandString = "";
		foreach (Transform lineTransform in transform){
			foreach (Transform slotTransform in lineTransform){
				Slot slot = slotTransform.GetComponent<Slot>();
				if (slot) {
					if (slot.commandOccupySlot) {
						string commend = slot.commandOccupySlot.name;
						if (commend != "")
						{
							commandString += commend;
							commandString += ";";
						}
					}
				}
			}
		}
		inventoryText.text = commandString;
	}
}