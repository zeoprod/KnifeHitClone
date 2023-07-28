using Gameplay;
using Gameplay.Factories;
using Gameplay.Input;
using Gameplay.LevelElements;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private StageGenerator _stageGenerator;
		[SerializeField] private KnifeThrower   _knifeThrower;

		public override void InstallBindings()
		{
			InstallLevelIterator();

			InstallGameSession();
			InstallStageGenerator();

			InstallKnifeThrowInput();
			InstallKnifeThrower();

			InstallGame();
		}

		private void InstallLevelIterator()
		{
			Container.Bind<LevelIterator>().AsSingle();
		}

		private void InstallGameSession()
		{
			Container.Bind<GameSession>().AsSingle();
		}

		private void InstallStageGenerator()
		{
			Container.Bind<StageGenerator>().FromInstance(_stageGenerator).AsSingle();
		}

		private void InstallKnifeThrowInput()
		{
			Container.Bind<KnifeThrowInputState>().AsSingle();
			Container.BindInterfacesAndSelfTo<KnifeThrowInput>().AsSingle();
		}

		private void InstallKnifeThrower()
		{
			Container.Bind<KnifeThrower>().FromInstance(_knifeThrower).AsSingle();
		}

		private void InstallGame()
		{
			Container.BindInterfacesTo<Game>().AsSingle();
		}
	}
}