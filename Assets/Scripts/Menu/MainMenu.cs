using Core.Enums;
using Core.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private Button _startButton;

		private SceneLoader _sceneLoader;

		[Inject]
		public void Construct(SceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
		}

		private void Awake() => _startButton.onClick.AddListener(LoadGameScene);
		private void OnDestroy() => _startButton.onClick.RemoveListener(LoadGameScene);
		private void LoadGameScene() => _sceneLoader.Load(SceneType.Game);
	}
}