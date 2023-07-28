using Gameplay;
using Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private ScoreView                _scoreView;
        [SerializeField] private LevelProgressBar         _levelProgressBar;
        [SerializeField] private KnifeThrowerAttemptsView _knifeThrowerAttemptsView;
    
        public override void InstallBindings()
        {
            InstallScoreView();
            InstallLevelProgressBar();
            InstallKnifeThrowerAttemptsView();
        }

        private void InstallScoreView()
        {
            Container.Bind<ScoreView>().FromInstance(_scoreView).AsSingle();
        }
    
        private void InstallLevelProgressBar()
        {
            Container.Bind<LevelProgressBar>().FromInstance(_levelProgressBar).AsSingle();
        }

        private void InstallKnifeThrowerAttemptsView()
        {
            Container.Bind<KnifeThrowerAttemptsView>().FromInstance(_knifeThrowerAttemptsView).AsSingle();
        }
    }
}