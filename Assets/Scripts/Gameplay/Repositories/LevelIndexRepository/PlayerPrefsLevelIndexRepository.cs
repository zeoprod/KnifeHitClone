using UnityEngine;

namespace Gameplay.Repositories.LevelIndexRepository
{
	public class PlayerPrefsLevelIndexRepository : ILevelIndexRepository
	{
		private const string LevelIndexKey = "LevelIndex";
		
		public int GetLevelIndex()
		{
			return PlayerPrefs.GetInt(LevelIndexKey, 0);
		}

		public void SaveLevelIndex(int levelIndex)
		{
			var currentLevelIndex = GetLevelIndex();
			if (levelIndex != currentLevelIndex)
			{
				PlayerPrefs.SetInt(LevelIndexKey, levelIndex);
			}
		}
	}
}