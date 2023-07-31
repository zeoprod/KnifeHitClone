using System;
using ScriptableObjects.Classes;
using UnityEngine;

namespace Gameplay.UI.Windows
{
	public class GameUI : MonoBehaviour
	{
		public event Action OnNextButtonClicked;
		public event Action OnRetryButtonClicked;
		public event Action OnMenuButtonClicked;
		
		[SerializeField] private GameWindow _gameWindow;
		[SerializeField] private VictoryWindow _victoryWindow;
		[SerializeField] private LoseWindow _loseWindow;

		private void Awake()
		{
			_victoryWindow.OnNextButtonClicked += NotifyAboutNextClick;
			_victoryWindow.OnMenuButtonClicked += NotifyAboutMenuClick;
			_loseWindow.OnMenuButtonClicked += NotifyAboutMenuClick;
			_loseWindow.OnRetryButtonClicked += NotifyAboutRetryClick;
		}

		private void OnDestroy()
		{
			_victoryWindow.OnNextButtonClicked -= NotifyAboutNextClick;
			_victoryWindow.OnMenuButtonClicked -= NotifyAboutMenuClick;
			_loseWindow.OnMenuButtonClicked -= NotifyAboutMenuClick;
			_loseWindow.OnRetryButtonClicked -= NotifyAboutRetryClick;
		}

		private void NotifyAboutMenuClick() => OnMenuButtonClicked?.Invoke();
		private void NotifyAboutNextClick() => OnNextButtonClicked?.Invoke();
		private void NotifyAboutRetryClick() => OnRetryButtonClicked?.Invoke();
		
		public void ShowGameWindow()
		{
			_gameWindow.Show();
			_victoryWindow.Hide();
			_loseWindow.Hide();
		}

		public void ShowVictoryWindow()
		{
			_gameWindow.Hide();
			_victoryWindow.Show();
			_loseWindow.Hide();
		}

		public void ShowLoseWindow()
		{
			_gameWindow.Hide();
			_victoryWindow.Hide();
			_loseWindow.Show();
		}

		public void StartLevel(LevelConfig currentLevelConfig)
		{
			_gameWindow.LevelProgressBar.Initialize(currentLevelConfig);
			
			ShowGameWindow();
		}

		public void UpdateCurrentStage(int currentStage)
		{
			_gameWindow.LevelProgressBar.SetCurrent(currentStage);
		}
	}
}