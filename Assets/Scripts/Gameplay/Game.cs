using System;
using System.Threading.Tasks;
using Core.Enums;
using Core.Services;
using Gameplay.LevelElements;
using Gameplay.Repositories.HighScoreRepository;
using Gameplay.UI.Windows;
using ScriptableObjects.Classes;
using ScriptableObjects.Classes.Gameplay;
using Zenject;

namespace Gameplay
{
	public class Game : IInitializable, IDisposable
	{
		private readonly SceneLoader       _sceneLoader;
		private readonly GameConfig        _gameConfig;
		private readonly IRecordRepository _recordRepository;
		private readonly GameSession       _gameSession;
		private readonly LevelIterator     _levelIterator;
		private readonly StageGenerator    _stageGenerator;
		private readonly KnifeThrower      _knifeThrower;
		private readonly GameUI            _gameUI;

		private LevelConfig _currentLevelConfig;
		private int         _currentStage;

		private bool _isDisposed;

		public Game(
			SceneLoader sceneLoader,
			GameConfig gameConfig,
			IRecordRepository recordRepository,
			LevelIterator levelIterator,
			GameSession gameSession,
			StageGenerator stageGenerator,
			KnifeThrower knifeThrower,
			GameUI gameUI)
		{
			_sceneLoader = sceneLoader;
			_gameConfig = gameConfig;
			_levelIterator = levelIterator;
			_recordRepository = recordRepository;
			_gameSession = gameSession;
			_stageGenerator = stageGenerator;
			_knifeThrower = knifeThrower;

			_gameUI = gameUI;
		}

		public void Initialize()
		{
			Subscribe();
			PrepareLevel();
		}

		private void PrepareLevel()
		{
			_currentStage = 0;

			_currentLevelConfig = _levelIterator.GetCurrentLevelConfig();
			var currentStageSettings = _currentLevelConfig.GetStageConfig(_currentStage);

			_gameUI.StartLevel(_currentLevelConfig);

			_stageGenerator.Generate(_currentLevelConfig.GetStageConfig(_currentStage));

			_knifeThrower.Initialize(currentStageSettings);
		}

		public void Dispose()
		{
			if (_isDisposed)
				return;

			_isDisposed = true;

			Unsubscribe();
		}

		private void Subscribe()
		{
			_knifeThrower.OnSuccessfulHit += HandleSuccessfulHit;
			_knifeThrower.OnFailedHit += HandleFailedHitStage;

			_gameUI.OnNextButtonClicked += LoadNextLevel;
			_gameUI.OnMenuButtonClicked += LoadMenu;
			_gameUI.OnRetryButtonClicked += RetryCurrentLevel;
		}

		private void Unsubscribe()
		{
			_knifeThrower.OnSuccessfulHit -= HandleSuccessfulHit;
			_knifeThrower.OnFailedHit -= HandleFailedHitStage;

			_gameUI.OnNextButtonClicked -= LoadNextLevel;
			_gameUI.OnMenuButtonClicked -= LoadMenu;
			_gameUI.OnRetryButtonClicked -= RetryCurrentLevel;
		}
		
		private void HandleSuccessfulHit()
		{
			if (_knifeThrower.HitAttemptsAmount == 0) HandleCompletedStage();
		}

		private async void HandleCompletedStage()
		{
			_currentStage++;

			_gameUI.UpdateCurrentStage(_currentStage);

			_stageGenerator.StageComplete();

			await Task.Delay(_gameConfig.MillisecondsDelayBetweenStages);

			if (_currentStage < _currentLevelConfig.StagesCount)
			{
				var currentStageSettings = _currentLevelConfig.GetStageConfig(_currentStage);

				_stageGenerator.Generate(currentStageSettings);
				_knifeThrower.Initialize(currentStageSettings);
			}
			else
			{
				_recordRepository.SaveRecord(_gameSession.CurrentScore);

				_gameUI.ShowVictoryWindow();
			}
		}

		private void HandleFailedHitStage()
		{
			_recordRepository.SaveRecord(_gameSession.CurrentScore);

			_gameUI.ShowLoseWindow();
		}

		private void LoadNextLevel()
		{
			_currentStage++;
			PrepareLevel();
		}

		private void RetryCurrentLevel()
		{
			PrepareLevel();
		}

		private void LoadMenu()
		{
			_sceneLoader.Load(SceneType.Menu);
		}
	}
}