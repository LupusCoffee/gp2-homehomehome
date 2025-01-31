// Made by Martin M
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "InventoryDb", menuName = "Scriptable Objects/Database/Inventory Database", order = 5)]
public class InventoryDatabase : ScriptableObject
{
	[FormerlySerializedAs("Items")]
	[SerializeField]
	public List<InventoryItem> Artifacts = new();

	[SerializeField]
	public List<SongAttributeItem> Songs = new();
	
	public Dictionary<int, int> ItemLookup => Artifacts
		.Where(x => x.interactable != null)
		.ToDictionary(x => x.interactable.name.GetHashCode() + 27, x => x.id);
	
	public bool TryGetItem(int id, out InventoryItem item)
	{
		int index = id & 0xFFFFFF; // Masks out the last 8 bits (type) to get the index
		
		if (Artifacts.Count > index)
		{
			item = Artifacts[index];
			return item.id == id;
		}

		item = null;
		return false;
	}

	#if UNITY_EDITOR
	private void OnValidate()
	{
		ValidateArtifactIDs();
		ValidateSongIDs();
	}

	private void ValidateArtifactIDs()
	{
		// check if artifacts is dirty
		if (Artifacts.Count == 0) return;
		HashSet<int> ids = new();
		for (var i = 0; i < Artifacts.Count; i++)
		{
			Artifacts[i].id = CalculateID(i, InventoryInteractibleType.Artifact);
			if (Artifacts[i].interactable == null) continue;
			int hash = Artifacts[i].interactable.name.GetHashCode() + 27;
			if (ids.Add(hash)) continue;
			Debug.LogWarning($"[Inventory Database] Duplicate artifact found: {Artifacts[i].interactable.name} at index (ID) {i}");
			Artifacts[i].interactable = null;
		}
	}
	
	private void ValidateSongIDs()
	{
		// check if artifacts is dirty
		if (Songs.Count == 0) return;
		if (Songs.Count > 0xFF_FFF)
		{
			Debug.LogError($"[Inventory Database] Too many songs in the database. Maximum is {0xFF_FFF:N0}");;
			return;
		}
		HashSet<int> ids = new();
		for (var i = 0; i < Songs.Count; i++)
		{
			Songs[i].id = CalculateID(i, InventoryInteractibleType.Song);
			if (Songs[i].SpellAttribute == null) continue;
			int hash = Songs[i].SpellAttribute.name.GetHashCode() + 27;
			if (ids.Add(hash)) continue;
			Debug.LogWarning($"[Inventory Database] Duplicate song attribute found: {Songs[i].SpellAttribute.name} at index (ID) {i}");
			Songs[i].SpellAttribute = null;
		}
	}

	public static int CalculateID(int index, InventoryInteractibleType type)
	{
		return index | ((int)type << 24); // 24 bits for the index and 8 bits for the type
	}
	
	#endif
}