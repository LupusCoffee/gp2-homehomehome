// Created by Martin M

using System;
using SaveSystem.Extensions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
	[RequireComponent(typeof(UniqueTransformIdentifier))]
	public class SavedTransform : MonoBehaviour, IAutoSaved
	{
		internal string Id => _uniqueTransformIdentifier.Id;
		
		/// <inheritdoc />
		[Header("Settings")]
		[field: SerializeField]
		public bool LoadOnStart { get; set; }
		
		public TransformSaveKind SaveKind = TransformSaveKind.Position 
		                                    | TransformSaveKind.Rotation 
		                                    | TransformSaveKind.Scale;

		public bool SavePosition => (SaveKind & TransformSaveKind.Position) != 0;
		public bool SaveRotation => (SaveKind & TransformSaveKind.Rotation) != 0;
		public bool SaveScale    => (SaveKind & TransformSaveKind.Scale)    != 0;
		
		[Header("Events")]
		public UnityEvent Saving;
		public UnityEvent Saved;
		
		public UnityEvent Loading;
		public UnityEvent Loaded;
		
		
		public Func<SavedTransform, bool> CanSave = (autoSaveTransform) => true;
		public Func<SavedTransform, bool> CanLoad = (autoSaveTransform) => true;
		
		private UniqueTransformIdentifier _uniqueTransformIdentifier;
		private float _lastSaveTime;


		private void OnEnable()
		{
			SaveManager.AddAutoSaved(this);
		}

		private void OnDisable()
		{
			SaveManager.RemoveAutoSaved(this);
		}

		private void Awake()
		{
			_uniqueTransformIdentifier = GetComponent<UniqueTransformIdentifier>();
		}

		/// <inheritdoc />
		public void Save(bool force = false)
		{
			if (!force && !CanSave(this)) return;
			Saving.Invoke();
			this.SaveTransform(SaveKind);
			Saved.Invoke();
		}

		/// <inheritdoc />
		public void UpdateSave(bool force = false)
		{
			if (!force && !CanSave(this)) return;
			this.UpdateSaveTransform(SaveKind);
		}

		/// <inheritdoc />
		public void Load(bool force = false)
		{
			if (!force && !CanLoad(this)) return;
			Loading.Invoke();
			this.LoadTransform();
			Loaded.Invoke();
		}

		public void TriggerSave()
		{
			Save();
		}
		
		public void TriggerLoad()
		{
			this.LoadTransform();
		}
		
		#if UNITY_EDITOR
			
		private void OnValidate()
		{
			EditorApplication.delayCall += EnsureSaveManagerExists;
		}

		private static void EnsureSaveManagerExists()
		{
			SaveManager saveManager = FindFirstObjectByType<SaveManager>();
			if (saveManager == null)
			{
				new GameObject("Save Manager").AddComponent<SaveManager>();
				Debug.LogWarning("No Save Manager found, spawning a new one");
			}
		}

		[InitializeOnLoad]
		public static class SaveManagerSpawner
		{
			static SaveManagerSpawner()
			{
				EditorApplication.update += EnsureSaveManager;
			}

			private static void EnsureSaveManager()
			{
				EnsureSaveManagerExists();
				EditorApplication.update -= EnsureSaveManager;
			}
		}
	#endif
	}
}
