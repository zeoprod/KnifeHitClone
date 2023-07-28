using System.Threading.Tasks;
using Gameplay.LevelElements;
using Gameplay.Repositories.HighScoreRepository;
using Gameplay.UI;
using ScriptableObjects.Classes;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public class Game : IInitializable
	{
		private readonly LevelIterator        _levelIterator;
		private readonly GameSession          _gameSession;
		private readonly StageGenerator       _stageGenerator;
		private readonly KnifeThrower         _knifeThrower;
		private readonly LevelProgressBar     _levelProgressBar;
		private readonly IHighScoreRepository _highScoreRepository;

		private int         _currentStage;
		private LevelConfig _currentLevelConfig;

		public Game(
			IHighScoreRepository highScoreRepository,
			LevelIterator levelIterator,
			GameSession gameSession,
			LevelProgressBar levelProgressBar,
			StageGenerator stageGenerator,
			KnifeThrower knifeThrower)
		{
			_levelIterator = levelIterator;
			_highScoreRepository = highScoreRepository;
			_gameSession = gameSession;
			_levelProgressBar = levelProgressBar;
			_stageGenerator = stageGenerator;
			_knifeThrower = knifeThrower;
		}

		public void Initialize()
		{
			Application.targetFrameRate = 60;

			_currentStage = 0;

			_currentLevelConfig = _levelIterator.GetCurrentLevelConfig();
			var currentStageSettings = _currentLevelConfig.GetStageConfig(_currentStage);

			_levelProgressBar.Initialize(_currentLevelConfig);
			_levelProgressBar.SetCurrent(_currentStage);

			_stageGenerator.Generate(_currentLevelConfig.GetStageConfig(_currentStage));

			_knifeThrower.OnSuccessfulHit += HandleSuccessfulHit;
			_knifeThrower.OnFailedHit += HandleFailedHitStage;
			_knifeThrower.Initialize(currentStageSettings);
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

			if (_currentStage < _currentLevelConfig.StagesCount)
			{
				var currentStageSettings = _currentLevelConfig.GetStageConfig(_currentStage);

				await Task.Delay(500);

				_stageGenerator.Generate(currentStageSettings);
				_knifeThrower.Initialize(currentStageSettings);
			}
			else
			{
				_knifeThrower.OnSuccessfulHit -= HandleSuccessfulHit;
				_knifeThrower.OnFailedHit -= HandleFailedHitStage;

				Debug.Log("Level Completed");

				_highScoreRepository.SaveHighScore(_gameSession.Data.Score.Value);
			}
		}

		private void HandleFailedHitStage()
		{
			_knifeThrower.OnSuccessfulHit -= HandleCompletedStage;
			_knifeThrower.OnFailedHit -= HandleFailedHitStage;

			Debug.Log("Level Failed");

			_gameSession.ResetScore();
		}
	}
}