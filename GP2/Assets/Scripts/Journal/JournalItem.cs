// Made by Martin M
using System;
using Journal.Attributes;
using UnityEngine;


[Serializable]
public class JournalItem
{
	[SerializeField] public string Title = "Title";
	[SerializeField] public JournalItemType JournalType = JournalItemType.Story;
	
	public StoryJournalItem StoryData = new();
	public ArtifactJournalItem ArtifactData = new();
}