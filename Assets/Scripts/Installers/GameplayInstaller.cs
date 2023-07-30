using Gameplay;
using Gameplay.Input;
using Gameplay.LevelElements;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private StageGenerator _stageGenerator;
		[SerializeField] private KnifeThrower   _knifeThrower;

		public override void InstallBindings()
		{
			BindGameSession();
			
			BindLevelIterator();
			BindStageGenerator();
			BindKnifeThrowInput();
			BindKnifeThrower();

			BindGame();
		}

		private void BindGameSession()
		{
			Container.Bind<GameSession>().AsSingle();
		}

		private void BindLevelIterator()
		{
			Container.Bind<LevelIterator>().AsSingle();
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