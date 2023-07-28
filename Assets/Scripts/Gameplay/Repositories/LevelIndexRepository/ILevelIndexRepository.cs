namespace Gameplay.Repositories.LevelIndexRepository
{
	public interface ILevelIndexRepository
	{
		int GetLevelIndex();
		void SaveLevelIndex(int levelIndex);
	}
}