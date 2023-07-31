using System;
using Gameplay.Repositories.HighScoreRepository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI.Windows
{
	public class LoseWindow : BaseWindow
	{
		public event Action OnMenuButtonClicked;
		public event Action OnRetryButtonClicked;

		[SerializeField] private TMP_Text _currentScoreValue;
		[SerializeField] private TMP_Text _recordScoreValue;
		
		[SerializeField] private Button            _menuButton;
		[SerializeField] private Button            _retryButton;
		
		private                  GameSession       _gameSession;
		private                  IRecordRepository _recordRepository;

		[Inject]
		public void Construct(IRecordRepository recordRepository, GameSession gameSession)
		{
			_recordRepository = recordRepository;
			_gameSession = gameSession;
		}

		protected override void Awake()
		{
			base.Awake();
			
			_menuButton.onClick.AddListener(NotifyAboutMenuClick);
			_retryButton.onClick.AddListener(NotifyAboutRetryClick);
		}

		private void OnDestroy()
		{
			_menuButton.onClick.RemoveListener(NotifyAboutMenuClick);
			_retryButton.onClick.RemoveListener(NotifyAboutRetryClick);
		}

		public override void Show(bool force = false)
		{
			SetWindowData();
			
			base.Show(force);
		}

		private void SetWindowData()
		{
			_recordScoreValue.text = _recordRepository.GetRecord().ToString();
			_currentScoreValue.text = _gameSession.CurrentScore.ToString();
		}

		private void NotifyAboutMenuClick() => OnMenuButtonClicked?.Invoke();
		private void NotifyAboutRetryClick() => OnRetryButtonClicked?.Invoke();
	}
}