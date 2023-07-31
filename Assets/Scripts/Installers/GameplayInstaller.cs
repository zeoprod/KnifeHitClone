using Gameplay;
using Gameplay.Input;
using Gameplay.LevelElements;
using ScriptableObjects.Classes.Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private GameConfig _gameConfig;

		[SerializeField] private StageGenerator _stageGenerator;
		[SerializeField] private KnifeThrower   _knifeThrower;

		public override void InstallBindings()
		{
			BindGameConfig();

			BindGameSession();

			BindLevelIterator();
			BindStageGenerator();
			BindKnifeThrowInput();
			BindKnifeThrower();

			BindGame();
		}

		private void BindGameConfig()
		{
			Container.Bind<GameConfig>().FromInstance(_gameConfig);
		}

		private void BindGameSession()
		{
			Container.BindInterfacesAndSelfTo<GameSession>().AsSingle();
		}

		private void BindLevelIterator()
		{
			Container.BindInterfacesAndSelfTo<LevelIterator>().AsSingle();
		}

		private void BindStageGenerator()
		{
			Container.Bind<StageGenerator>().FromInstance(_stageGenerator).AsSingle();
		}

		private void BindKnifeThrowInput()
		{
			Container.Bind<KnifeThrowInputState>().AsSingle();
			Container.BindInterfacesAndSelfTo<KnifeThrowInput>().AsSingle();
		}

		private void BindKnifeThrower()
		{
			Container.Bind<KnifeThrower>().FromInstance(_knifeThrower).AsSingle();
		}

		private void BindGame()
		{
			Container.BindInterfacesTo<Game>().AsSingle();
		}
	}
}