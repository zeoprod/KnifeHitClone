using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public class ScoreView : MonoBehaviour
	{
		private GameSession _gameSession;

		[SerializeField] private TMP_Text _scoreValue;

		[Inject]
		public void Construct(GameSession gameSession)
		{
			_gameSession = gameSession;

			_gameSession.OnScoreChanged += UpdateScore;
		}

		private void OnDestroy()
		{
			_gameSession.OnScoreChanged -= UpdateScore;
		}

		private void UpdateScore()
		{
			_scoreValue.text = _gameSession.CurrentScore.ToString();
		}
	}
}