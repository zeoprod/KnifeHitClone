using Gameplay.Factories;
using Gameplay.Repositories.LevelIndexRepository;
using ScriptableObjects.Classes;
using Zenject;

namespace Gameplay
{
	public class LevelIterator : IInitializable
	{
		private readonly LevelConfigsFactory   _levelConfigsFactory;
		private readonly ILevelIndexRepository _levelIndexRepository;
		private          int                   _currentLevelIndex;
		private          int                   _maxLevelIndex;

		public LevelIterator(LevelConfigsFactory levelConfigsFactory, ILevelIndexRepository levelIndexRepository)
		{
			_levelIndexRepository = levelIndexRepository;
			_levelConfigsFactory = levelConfigsFactory;
		}

		public void Initialize()
		{
			Load();
			
			_maxLevelIndex = _levelConfigsFactory.Count;
		}

		public LevelConfig GetCurrentLevelConfig()
		{
			return _levelConfigsFactory.Get(_currentLevelIndex);
		}

		public void MoveToNextLevel()
		{
			if (_currentLevelIndex < _maxLevelIndex)
				_currentLevelIndex++;
			else
				Reset();
			
			Save();
		}

		private void Reset() => _currentLevelIndex = 0;

		private void Save() => _levelIndexRepository.SaveLevelIndex(_currentLevelIndex);

		private void Load()
		{
			_currentLevelIndex = _levelIndexRepository.GetLevelIndex();
			
			if (_currentLevelIndex > _levelConfigsFactory.Count - 1) Reset();
		}
	}
}