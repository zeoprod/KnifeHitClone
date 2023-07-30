using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Windows
{
    public class VictoryWindow : BaseWindow
    {
        public event Action OnMenuButtonClicked;
        public event Action OnNextButtonClicked;
        
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _nextButton;

        protected override void Awake()
        {
            base.Awake();
            
            _menuButton.onClick.AddListener(NotifyAboutMenuClick);
            _nextButton.onClick.AddListener(NotifyAboutNextClick);
        }

        private void OnDestroy()
        {
            _menuButton.onClick.RemoveListener(NotifyAboutMenuClick);
            _nextButton.onClick.RemoveListener(NotifyAboutNextClick);
        }

        private void NotifyAboutMenuClick() => OnMenuButtonClicked?.Invoke();
        private void NotifyAboutNextClick() => OnNextButtonClicked?.Invoke();
    }
}
