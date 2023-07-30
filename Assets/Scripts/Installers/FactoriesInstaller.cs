using Gameplay;
using Gameplay.Factories;
using ScriptableObjects.Classes;
using ScriptableObjects.Classes.Logs;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class FactoriesInstaller : MonoInstaller
	{
		[SerializeField] private AllLevelsConfig    _allLevelsConfig;
		[SerializeField] private AllLogsConfigs     _allLogsConfigs;
		[SerializeField] private AllKnivesConfig    _allKnivesConfig;
		[SerializeField] private AllObstaclesConfig _allObstaclesConfig;

		public override void InstallBindings()
		{
			BindLevelConfigsFactory();
			BindLogsFactory();
			BindKnivesFactory();
			BindObstaclesConfig();
		}

		private void BindLevelConfigsFactory()
		{
			Container.Bind<LevelConfigsFactory>().AsSingle().WithArguments(_allLevelsConfig);
		}

		private void BindLogsFactory()
		{
			Container.Bind<LogsFactory>().AsSingle().WithArguments(_allLogsConfigs);
		}

		private void BindKnivesFactory()
		{
			Container.Bind<KnivesFactory>().AsSingle().WithArguments(_allKnivesConfig);
		}

		private void BindObstaclesConfig()
		{
			Container.Bind<ObstaclesFactory>().AsSingle().WithArguments(_allObstaclesConfig);
		}
	}
}