// Made by Martin M
using UnityEngine;

public static class InteractableExtensions
{
	public static void InitializeIDFromDatabase(this Interactable interactable)
	{
		if (Inventory.TryGetItem(interactable, out InventoryItem item))
		{
			Debug.Log($"Set ID {item.id}");
			interactable.SetID(item.id);
			return;
		}
		
		Debug.LogWarning($"[Inventory System (Extensions)] Could not find item for {interactable.name}\n" +
		                 "Please make sure the item is in the inventory database (and is a prefab)");
	}
}