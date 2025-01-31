// Made by Martin M
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
	[SerializeField] public int id = -1;
	[SerializeField] public string name = "Empty";
	[SerializeField] public Sprite icon = null;
	[SerializeField] public Interactable interactable = null;
}