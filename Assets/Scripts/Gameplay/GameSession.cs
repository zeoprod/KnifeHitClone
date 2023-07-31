using System;
using Gameplay.LevelElements;
using Gameplay.UI.Windows;
using Zenject;

namespace Gameplay
{
	public class GameSession : IInitializable, IDisposable
	{
		public event Action OnScoreChanged;

		private readonly KnifeThrower _knifeThrower;
		private readonly GameUI       _gameUI;

		public int CurrentScore { get; private set; }

		public GameSession(KnifeThrower knifeThrower, GameUI gameUI)
		{
			_knifeThrower = knifeThrower;
			_gameUI = gameUI;
		}

		public void Initialize()
		{
			_knifeThrower.OnSuccessfulHit += IncreaseScore;
			_gameUI.OnRetryButtonClicked += ResetScore;
		}

		public void Dispose()
		{
			_knifeThrower.OnSuccessfulHit -= IncreaseScore;
			_gameUI.OnRetryButtonClicked -= ResetScore;
		}
		
		private void IncreaseScore()
		{
			CurrentScore++;

			OnScoreChanged?.Invoke();
		}

		private void ResetScore()
		{
			CurrentScore = 0;

			OnScoreChanged?.Invoke();
		}
	}
}