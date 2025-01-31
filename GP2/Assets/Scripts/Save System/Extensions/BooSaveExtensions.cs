// Created by Martin M

using UnityEngine;

namespace SaveSystem.Extensions
{
	public static class BooSaveExtensions
	{
		public static void SaveTransform(this SavedTransform transform, TransformSaveKind kind = TransformSaveKind.Position)
		{
			BooSave.Shared.Update(new TransformSaveData(transform.transform, kind), 
				transform.Id);
			BooSave.Shared.Save();
		}
		
		public static void UpdateSaveTransform(this SavedTransform transform, TransformSaveKind kind = TransformSaveKind.Position)
		{
			BooSave.Shared.Update(new TransformSaveData(transform.transform), 
				transform.Id);
		}
		
		public static void LoadTransform(this SavedTransform transform)
		{
			if (BooSave.Shared.TryLoad(transform.Id, out TransformSaveData data))
			{
				transform.transform.position = data.Position;
			}
			else
			{
				Debug.LogWarning($@"Failed to load transform data for {transform.Id}
Got type:{(BooSave.Shared.ContainsKey(transform.Id) ? BooSave.Shared.GetType(transform.Id).Name : "null")}");
			}
		}
	}
}