// Created by Martin M
namespace SaveSystem
{
	public interface IAutoSaved
	{
		public bool LoadOnStart { get; set; }
		public void Save(bool force = false);
		public void UpdateSave(bool force = false);
		public void Load(bool force = false);
	}
}