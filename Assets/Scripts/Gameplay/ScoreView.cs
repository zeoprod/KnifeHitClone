using System;
using TMPro;
using UnityEngine;
using Utils.Disposables;
using Zenject;

namespace Gameplay
{
	public class ScoreView : MonoBehaviour
	{
		private GameSession _gameSession;

		[SerializeField] private TMP_Text _scoreValue;

		private readonly CompositeDisposable _subscribes = new();
		
		[Inject]
		public void Construct(GameSession gameSession)
		{
			_gameSession = gameSession;

			_subscribes.Retain(_gameSession.Data.Score.Subscribe(UpdateScore));
		}

		private void OnDestroy()
		{
			_subscribes.Dispose();
		}

		private void UpdateScore(int newValue, int oldValue)
		{
			_scoreValue.text = newValue.ToString();
		}
	}
}