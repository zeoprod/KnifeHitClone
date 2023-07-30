using System;

namespace Gameplay
{
	public class GameSession
	{
		public event Action OnScoreChanged;
		
		private int _currentScore;

		public int CurrentScore => _currentScore;

		public void IncreaseScore()
		{
			_currentScore++;
			
			OnScoreChanged?.Invoke();
		}

		public void ResetScore()
		{
			_currentScore = 0;
			
			OnScoreChanged?.Invoke();
		}
	}
}