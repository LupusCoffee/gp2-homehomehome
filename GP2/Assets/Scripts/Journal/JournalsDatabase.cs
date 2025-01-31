// Made by Martin M
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "JournalDatabase", menuName = "Scriptable Objects/Database/Journals Database", order = 0)]
public class JournalsDatabase : ScriptableObject
{
	public JournalItemData NpcJournal;
	public JournalItemData StoryJournal;
	public JournalItemData SongsJournal;
	
	#if UNITY_EDITOR

	public void AddJournalItemData()
	{
		if (NpcJournal != null && StoryJournal != null && SongsJournal != null) return;

		string assetPath = AssetDatabase.GetAssetPath(this);
		Object[] subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
		// Make sure there are only 3 sub assets
		if (subAssets.Length is not (3 or 0))
		{
			Debug.LogWarning("There should be 3 sub assets in the JournalsDatabase");
			return;
		}
		
		if (NpcJournal == null)
		{
			NpcJournal = CreateInstance<JournalItemData>();
			NpcJournal.name = "NpcJournal";
			AssetDatabase.AddObjectToAsset(NpcJournal, this);
			EditorUtility.SetDirty(NpcJournal);
		}
		
		if (StoryJournal == null)
		{
			StoryJournal = CreateInstance<JournalItemData>();
			StoryJournal.name = "StoryJournal";
			AssetDatabase.AddObjectToAsset(StoryJournal, this);
			EditorUtility.SetDirty(StoryJournal);
		}
		
		if (SongsJournal == null)
		{
			SongsJournal = CreateInstance<JournalItemData>();
			SongsJournal.name = "SongsJournal";
			AssetDatabase.AddObjectToAsset(SongsJournal, this);
			EditorUtility.SetDirty(SongsJournal);
		}
		
		EditorUtility.SetDirty(this);
		AssetDatabase.SaveAssets();
		
	}
		
	#endif
}