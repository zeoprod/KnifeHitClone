using Gameplay;
using Gameplay.UI;
using Gameplay.UI.Windows;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class GameplayUIInstaller : MonoInstaller
	{
		[SerializeField] private ScoreView                _scoreView;
		[SerializeField] private LevelProgressBar         _levelProgressBar;
		[SerializeField] private KnifeThrowerAttemptsView _knifeThrowerAttemptsView;

		[SerializeField] private GameUI        _gameUI;

		public override void InstallBindings()
		{
			BindGameUI();

			BindLevelProgressBar();
			BindScoreView();
			BindKnifeThrowerAttemptsView();
		}

		private void BindGameUI()
		{
			Container.Bind<GameUI>().FromInstance(_gameUI).AsSingle();
		}

		private void BindScoreView()
		{
			Container.Bind<ScoreView>().FromInstance(_scoreView).AsSingle();
		}

		private void BindLevelProgressBar()
		{
			Container.Bind<LevelProgressBar>().FromInstance(_levelProgressBar).AsSingle();
		}

		private void BindKnifeThrowerAttemptsView()
		{
			Container.Bind<KnifeThrowerAttemptsView>().FromInstance(_knifeThrowerAttemptsView).AsSingle();
		}
	}
}