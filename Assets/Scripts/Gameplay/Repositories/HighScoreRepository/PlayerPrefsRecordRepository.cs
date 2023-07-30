using UnityEngine;

namespace Gameplay.Repositories.HighScoreRepository
{
	public class PlayerPrefsRecordRepository : IRecordRepository
	{
		private const string RecordKey = "Record";

		public int GetRecord()
		{
			return PlayerPrefs.GetInt(RecordKey, 0);
		}

		public void SaveRecord(int score)
		{
			var currentHighScore = GetRecord();
			if (score > currentHighScore)
			{
				PlayerPrefs.SetInt(RecordKey, score);
			}
		}
	}
}