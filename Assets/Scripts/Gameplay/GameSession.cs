namespace Gameplay
{
	public class GameSession
	{
		public GameSessionData Data => _data;

		private GameSessionData _data;
		
		public GameSession()
		{
			ResetState();
		}
		
		public void IncreaseScore()
		{
			_data.Score.Value += 1;
		}

		public void ResetScore()
		{
			_data.Score.Value = 0;
		}
		
		private void ResetState()
		{
			_data = new GameSessionData();
		}
	}
}