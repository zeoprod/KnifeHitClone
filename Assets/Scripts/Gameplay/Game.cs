using System;
using System.Threading.Tasks;
using Core.Enums;
using Core.Services;
using Gameplay.LevelElements;
using Gameplay.Repositories.HighScoreRepository;
using Gameplay.UI;
using Gameplay.UI.Windows;
using ScriptableObjects.Classes;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public class Game : IInitializable, IDisposable
	{
		private readonly SceneLoader       _sceneLoader;
		private readonly IRecordRepository _recordRepository;
		private readonly GameSession       _gameSession;
		private readonly LevelIterator     _levelIterator;
		private readonly StageGenerator    _stageGenerator;
		private readonly KnifeThrower      _knifeThrower;
		private readonly LevelProgressBar  _levelProgressBar;

		private readonly GameWindow    _gameWindow;
		private readonly VictoryWindow _victoryWindow;
		private readonly LoseWindow    _loseWindow;

		private LevelConfig _currentLevelConfig;
		private int         _currentStage;

		private bool _isDisposed;

		public Game(
			SceneLoader sceneLoader,
			IRecordRepository recordRepository,
			LevelIterator levelIterator,
			GameSession gameSession,
			LevelProgressBar levelProgressBar,
			StageGenerator stageGenerator,
			KnifeThrower knifeThrower,
			GameWindow gameWindow,
			VictoryWindow victoryWindow,
			LoseWindow loseWindow)
		{
			_sceneLoader = sceneLoader;
			_levelIterator = levelIterator;
			_recordRepository = recordRepository;
			_gameSession = gameSession;
			_levelProgressBar = levelProgressBar;
			_stageGenerator = stageGenerator;
			_knifeThrower = knifeThrower;

			_gameWindow = gameWindow;
			_victoryWindow = victoryWindow;
			_loseWindow = loseWindow;
		}

		public void Initialize()
		{
			Subscribe();
			PrepareLevel();
		}

		private void PrepareLevel()
		{
			_currentStage = 0;
			
			_gameWindow.Show();
			_victoryWindow.Hide();
			_loseWindow.Hide();

			_currentLevelConfig = _levelIterator.GetCurrentLevelConfig();
			var currentStageSettings = _currentLevelConfig.GetStageConfig(_currentStage);

			_levelProgressBar.Initialize(_currentLevelConfig);
			_levelProgressBar.SetCurrent(_currentStage);

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

			_victoryWindow.OnNextButtonClicked += LoadNextLevel;
			_victoryWindow.OnMenuButtonClicked += LoadMenu;

			_loseWindow.OnRetryButtonClicked += RetryCurrentLevel;
			_loseWindow.OnMenuButtonClicked += LoadMenu;
		}

		private void Unsubscribe()
		{
			_knifeThrower.OnSuccessfulHit -= HandleSuccessfulHit;
			_knifeThrower.OnFailedHit -= HandleFailedHitStage;

			_victoryWindow.OnNextButtonClicked -= LoadNextLevel;
			_victoryWindow.OnMenuButtonClicked -= LoadMenu;

			_loseWindow.OnRetryButtonClicked -= RetryCurrentLevel;
			_loseWindow.OnMenuButtonClicked -= LoadMenu;
		}


		private void HandleSuccessfulHit()
		{
			_gameSession.IncreaseScore();

			if (_knifeThrower.HitAttemptsAmount == 0) HandleCompletedStage();
		}

		private async void HandleCompletedStage()
		{
			_currentStage++;

			_levelProgressBar.SetCurrent(_currentStage);

			Debug.Log("Stage Completed");

			_stageGenerator.Log.StopRotation();
			_stageGenerator.Log.Throw();

			const int MillisecondsDelayBetweenStages = 500;
			await Task.Delay(MillisecondsDelayBetweenStages);
			
			if (_currentStage < _currentLevelConfig.StagesCount)
			{
				var currentStageSettings = _currentLevelConfig.GetStageConfig(_currentStage);

				_stageGenerator.Generate(currentStageSettings);
				_knifeThrower.Initialize(currentStageSettings);
			}
			else
			{
				_knifeThrower.OnSuccessfulHit -= HandleSuccessfulHit;
				_knifeThrower.OnFailedHit -= HandleFailedHitStage;

				Debug.Log($"Level Completed with score: {_gameSession.CurrentScore}");

				_recordRepository.SaveRecord(_gameSession.CurrentScore);

				_gameWindow.Hide();
				_victoryWindow.Show();
			}
		}

		private void HandleFailedHitStage()
		{
			Debug.Log($"Level Failed with score: {_gameSession.CurrentScore}");

			_recordRepository.SaveRecord(_gameSession.CurrentScore);

			_gameWindow.Hide();
			_loseWindow.Show();

			_gameSession.ResetScore();
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