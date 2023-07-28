using Gameplay.Repositories;
using Gameplay.Repositories.HighScoreRepository;
using Gameplay.Repositories.LevelIndexRepository;
using Zenject;

namespace Installers
{
	public class RepositoriesInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			CreateHighScoreRepository();
			CreateLevelIndexRepository();
		}

		private void CreateHighScoreRepository()
		{
			Container.Bind<IHighScoreRepository>().To<PlayerPrefsHighScoreRepository>().AsSingle();
		}

		private void CreateLevelIndexRepository()
		{
			Container.Bind<ILevelIndexRepository>().To<PlayerPrefsLevelIndexRepository>().AsSingle();
		}
	}
}
