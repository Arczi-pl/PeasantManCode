using UnityEngine;
using UnityEngine.UI;

public class ExecuteCommandsList : MonoBehaviour
{
	private GameObject _inventory;
	private Text _inventoryText;

	private void Start ()
	{	
		_inventory = GameObject.Find("/UI/Commands" + gameObject.name.Remove(gameObject.name.Length - 4, 4));
		_inventoryText = _inventory.GetComponent<Text>();
		_inventory.SetActive(false);
		UpdateList();
	}
	
	public void UpdateList ()
	{	
		string commandString = "";
		foreach (Transform lineTransform in transform)
		{
			foreach (Transform slotTransform in lineTransform)
			{
				Slot slot = slotTransform.GetComponent<Slot>();
				if (slot)
				{
					if (slot.GetCommandOccupySlot())
					{
						string commend = slot.GetCommandOccupySlot().name;
						if (commend != "")
						{
							commandString += commend;
							commandString += ";";
						}
					}
				}
			}
		}
		_inventoryText.text = commandString;
	}
}