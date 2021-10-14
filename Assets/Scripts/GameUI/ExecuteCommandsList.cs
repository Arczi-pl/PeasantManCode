using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExecuteCommandsList : MonoBehaviour, IHasChanged {
	public Text inventoryText;

	// Use this for initialization
	void Start () {
		HasChanged ();
	}
	
	public void HasChanged ()
	{
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		foreach (Transform slotTransform in transform){
			GameObject item = slotTransform.GetComponent<Slot>().commandOccupySlot;
			if (item){
                string commend = item.name.ToLower();
				if (commend != "")
				{
					builder.Append (commend);
					builder.Append (";");
				}
				
			}
		}
		inventoryText.text = builder.ToString ();
	}
}


namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged();
	}
}