using System;
using System.Threading;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Infrastructure.Services.Initialization;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.EntryPoints
{
    public class Boot : IAsyncStartable
    {
        private IInitializationService _initializationService;
        private ISceneLoader _sceneLoader;
        private AppLifetimeScope _appLifetimeScope;

        [Inject]
        public void Construct(AppLifetimeScope appLifetimeScope, IInitializationService initializationService, ISceneLoader sceneLoader)
        {
            _appLifetimeScope = appLifetimeScope;
            _sceneLoader = sceneLoader;
            _initializationService = initializationService;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _initializationService.InitializeGame();
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            await LoadGame();
        }

        private async UniTask LoadGame()
        {
            await _sceneLoader.LoadSceneInjected(GameConstants.AddressablesRefs.GameScene, LoadSceneMode.Additive,
                _appLifetimeScope);
        }
    }
}