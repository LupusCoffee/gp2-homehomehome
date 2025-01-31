// Created by Martin M
using System;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SaveSystem
{
	public sealed class UniqueTransformIdentifier : MonoBehaviour
	{
		public string Id => _useManualId ? _manualId : _generatedId;
		
		[SerializeField] private string _generatedId;
		[SerializeField] private bool _useManualId;
		[SerializeField] private string _manualId;
		
		
	#if UNITY_EDITOR
		private void OnValidate()
		{
			if (string.IsNullOrEmpty(_generatedId))
			{
				GenerateNewGuid();
			}
		}
		
		internal void GenerateNewGuid()
		{
			_generatedId = new StringBuilder()
				.Append(SceneManager.GetActiveScene().name)
				.Append(GetInstanceID().ToString("X2"))
				.ToString();
		}
		
		[CustomEditor(typeof(UniqueTransformIdentifier))]
	
		public class UniqueTransformIdentifierEditor : Editor
		{
			public override void OnInspectorGUI()
			{
				serializedObject.Update();
				var props = serializedObject.GetIterator();
				props.NextVisible(true);
				while (props.NextVisible(true))
				{
					if (props.propertyPath == nameof(_manualId)
					    && !((UniqueTransformIdentifier) target)._useManualId) continue;
					GUI.enabled = props.propertyPath != nameof(_generatedId);
					if (props.propertyPath == nameof(Id))
					{
						GUILayout.Space(10);
					}
					EditorGUILayout.PropertyField(props);
					GUI.enabled = true;
				}

				serializedObject.ApplyModifiedProperties();
				GUILayout.Space(10);
				if (GUILayout.Button("Generate New GUID"))
				{
					((UniqueTransformIdentifier) target).GenerateNewGuid();
					EditorUtility.SetDirty(target);
				}
			}
		}
	#endif
	}
}