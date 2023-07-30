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

		[SerializeField] private GameWindow    _gameWindow;
		[SerializeField] private VictoryWindow _victoryWindow;
		[SerializeField] private LoseWindow    _loseWindow;

		public override void InstallBindings()
		{
			BindWindows();

			BindLevelProgressBar();
			BindScoreView();
			BindKnifeThrowerAttemptsView();
		}

		private void BindWindows()
		{
			Container.Bind<GameWindow>().FromInstance(_gameWindow).AsSingle();
			Container.Bind<VictoryWindow>().FromInstance(_victoryWindow).AsSingle();
			Container.Bind<LoseWindow>().FromInstance(_loseWindow).AsSingle();
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