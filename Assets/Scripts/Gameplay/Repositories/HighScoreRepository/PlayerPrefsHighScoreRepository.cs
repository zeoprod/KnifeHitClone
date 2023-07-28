using UnityEngine;

namespace Gameplay.Repositories.HighScoreRepository
{
	public class PlayerPrefsHighScoreRepository : IHighScoreRepository
	{
		private const string HighScoreKey = "HighScore";

		public int GetHighScore()
		{
			return PlayerPrefs.GetInt(HighScoreKey, 0);
		}

		public void SaveHighScore(int score)
		{
			var currentHighScore = GetHighScore();
			if (score > currentHighScore)
			{
				PlayerPrefs.SetInt(HighScoreKey, score);
			}
		}
	}
}