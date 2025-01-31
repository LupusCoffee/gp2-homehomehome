using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


[CustomEditor(typeof(JournalsDatabase))]
public class JournalsDatabaseEditor : Editor
{
	[FormerlySerializedAs("_journalItemDataEditor")]
	[SerializeField] private VisualTreeAsset _journalsDatabaseEditor;
	
	public override VisualElement CreateInspectorGUI()
	{
		VisualElement root = new();
		if (_journalsDatabaseEditor == null)
		{
			root.Add(new Label("JournalsDatabaseEditor visual tree asset is not set"));
			return root;
		}
		TemplateContainer editor = _journalsDatabaseEditor.CloneTree();
		SerializedObject serializedTarget = new(target);
		editor.Bind(serializedTarget);
		root.Add(editor);
		return root;
	}
}