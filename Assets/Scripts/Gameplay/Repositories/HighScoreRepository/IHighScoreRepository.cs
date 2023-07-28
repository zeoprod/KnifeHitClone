namespace Gameplay.Repositories.HighScoreRepository
{
	public interface IHighScoreRepository
	{
		int GetHighScore();
		void SaveHighScore(int score);
	}
}