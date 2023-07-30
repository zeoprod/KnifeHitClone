using Core.Services;
using Gameplay.Repositories.HighScoreRepository;
using Gameplay.Repositories.LevelIndexRepository;
using Zenject;

namespace Installers
{
	public class GlobalInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindSceneLoaderService();
			
			BindRecordRepository();
			BindLevelIndexRepository();
		}

		private void BindSceneLoaderService()
		{
			Container.Bind<SceneLoader>().AsSingle();
		}

		private void BindRecordRepository()
		{
			Container.Bind<IRecordRepository>().To<PlayerPrefsRecordRepository>().AsSingle();
		}

		private void BindLevelIndexRepository()
		{
			Container.Bind<ILevelIndexRepository>().To<PlayerPrefsLevelIndexRepository>().AsSingle();
		}
	}
}
