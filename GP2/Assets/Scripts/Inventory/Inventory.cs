// Made by Martin M

using System;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour, IAutoSaved
{
	private static Inventory _instance;
	private static Inventory Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindFirstObjectByType<Inventory>();
			}

			return _instance;
		}
	}
	private static Dictionary<int, int> _lookupItemTable = new();
	
	[FormerlySerializedAs("database")]
	[SerializeField] private InventoryDatabase _database;
	[SerializeField] private List<int> _inventory = new();
	
	private BooSave _booSave;
	
	public static bool TryGetItem(Interactable interactable, out InventoryItem item)
	{
		
		if (_lookupItemTable.TryGetValue(interactable.name.GetHashCode() + 27, out int id))
		{
			return Instance._database.TryGetItem(id, out item);
		}

		item = null;
		return false;
	}

	public static bool TryCollectItem(Interactable interactable)
	{
		Debug.Log("[Inventory] Trying to collect item");
		if (_instance == null)
		{
			Debug.LogWarning("[Inventory] Inventory instance is null cannot collect item\n" +
			                 "Make sure the Inventory component is attached to a GameObject in the scene");
			return false;
		}
		Instance.CollectItem(interactable);
		return false;
	}

	private void Awake()
	{
		_lookupItemTable = _database.ItemLookup;
		foreach (var item in _database.Artifacts)
		{
			Debug.Log(item.id);
		}
		_booSave = BooSave.Create()
			.WithFileName("inventory.dat")
			.Build();
		LoadInventory();
	}

	private void OnEnable()
	{
		SaveManager.AddAutoSaved(this);
	}
	
	private void OnDisable()
	{
		SaveManager.RemoveAutoSaved(this);
	}


	public void Start()
	{
		
	}

	public void SaveInventory()
	{
		_booSave.Save(_inventory, "inventoryItems");
	}
	
	public bool CollectItem(Interactable interactable)
	{
		if (TryGetItem(interactable, out InventoryItem item))
		{
			Debug.Log("[Inventory] Found item in database");
			_inventory.Add(item.id);
			SaveInventory();
			return true;
		}

		Debug.LogWarning($"Failed to collect item with id {interactable.GetID()} item does not exist in database");
		return false;
	}

	public bool CollectSpellAttribute(InteractableSpellAttribute interactable)
	{
		if (interactable.GetInteractableData() is not InteractableSpellAttributeData spellAttributeData) return false;
		var spellAttribute = spellAttributeData.GetScriptableObject();
		if (spellAttribute == null) return false;
		Debug.Log("[Inventory] Found spell attribute in database");
		if (TryGetItem(interactable, out InventoryItem item))
		{
			Debug.Log("[Inventory] Found item in database");
			_inventory.Add(item.id);
			SaveInventory();
			return true;
		}

		Debug.LogWarning($"Failed to collect item with id {interactable.GetID()} item does not exist in database");
		return false;
	}
	
	public void RemoveItem(int id)
	{
		_inventory.Remove(id);
		SaveInventory();
	}
	
	public void LoadInventory()
	{
		if (!_booSave.TryLoad("inventoryItems", out List<int> inventory))
		{
			Debug.LogWarning("Failed to load inventory items");
			return;
		}
		// validate items
		var invalidItems = new List<int>();
		foreach (int id in inventory)
		{
			if (_database.TryGetItem(id, out InventoryItem _)) continue;
			Debug.LogWarning($"Failed to load item with id {id}");
			invalidItems.Add(id);
		}

		if (invalidItems.Count > 0)
		{
			foreach (int id in invalidItems)
			{
				inventory.Remove(id);
			}
			
			SaveInventory();
		}
		
		_inventory = inventory;
		
	}

	/// <inheritdoc />
	public bool LoadOnStart { get; set; }

	/// <inheritdoc />
	public void Save(bool force = false)
	{
		SaveInventory();
	}

	/// <inheritdoc />
	public void UpdateSave(bool force = false)
	{
		SaveInventory();
	}

	/// <inheritdoc />
	public void Load(bool force = false)
	{
		LoadInventory();
	}
}
