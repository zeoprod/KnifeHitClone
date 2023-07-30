namespace Gameplay.Repositories.HighScoreRepository
{
	public interface IRecordRepository
	{
		int GetRecord();
		void SaveRecord(int score);
	}
}