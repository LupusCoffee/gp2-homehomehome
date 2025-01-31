// Created by Martin M
using System;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : MonoBehaviour
{
	#region Singleton
	public static SaveManager Instance { get; private set; }
	
	public void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	#endregion
		
		
	private static readonly List<IAutoSaved> SavedTransforms = new();

	[Header("Events")]
	public UnityEvent Saving = new();
	public UnityEvent Saved = new();
		
	public UnityEvent Loading = new();
	public UnityEvent Loaded = new();
	
	[Header("Settings")]
	public float SaveIntervalMinutes = 5;
	
	private float _lastSaveTime;
	
	public static void AddAutoSaved(IAutoSaved autoSaved)
	{
		SavedTransforms.Add(autoSaved);
	}
	
	public static void RemoveAutoSaved(IAutoSaved autoSaved)
	{
		SavedTransforms.Remove(autoSaved);
	}
		
	public void SaveGame()
	{
		Saving.Invoke();
		NotifyWantSave();
		BooSave.Shared.Save();
		Saved.Invoke();
	}
	
	public void LoadGame(bool start = false)
	{
		Loading.Invoke();
		NotifyWantLoad(start);
		Loaded.Invoke();
	}

	private void Start()
	{
		_lastSaveTime = Time.deltaTime;
		LoadGame(true);
	}


	private static void NotifyWantSave()
	{
		foreach (IAutoSaved savedTransform in SavedTransforms)
		{
			savedTransform.UpdateSave();
		}
	}
	
	private static void NotifyWantLoad(bool start = false)
	{
		foreach (IAutoSaved savedTransform in SavedTransforms)
		{
			if (!savedTransform.LoadOnStart) continue;
			savedTransform.Load();
		}
	}
	
	private void Update()
	{
		if (Time.time - _lastSaveTime > SaveIntervalMinutes * 60)
		{
			_lastSaveTime = Time.time;
			SaveGame();
		}
	}
}