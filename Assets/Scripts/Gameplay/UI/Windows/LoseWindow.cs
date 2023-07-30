using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Windows
{
	public class LoseWindow : BaseWindow
	{
		public event Action OnMenuButtonClicked;
		public event Action OnRetryButtonClicked;
		
		[SerializeField] private Button _menuButton;
		[SerializeField] private Button _retryButton;

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

		private void NotifyAboutMenuClick() => OnMenuButtonClicked?.Invoke();
		private void NotifyAboutRetryClick() => OnRetryButtonClicked?.Invoke();
	}
}