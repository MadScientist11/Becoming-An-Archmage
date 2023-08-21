using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using BecomingAnArchmage.Source.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.EntryPoints
{
    public class Boot : IAsyncStartable
    {
        private IReadOnlyList<IInitializableService> _services;
        private IResourceManager _resourcesManager;
        private ISceneLoader _sceneLoader;
        private AppLifetimeScope _appLifetimeScope;

        [Inject]
        public void Construct(AppLifetimeScope appLifetimeScope, IReadOnlyList<IInitializableService> services, ISceneLoader sceneLoader)
        {
            _appLifetimeScope = appLifetimeScope;
            _sceneLoader = sceneLoader;
            _services = services;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await InitializeEssentials();

            await ShowLoadingScreen();

            await InitializeServices();
            await LoadGameAssets();
            await LoadGame();
        }

        private async UniTask LoadGame()
        {
            await _sceneLoader.LoadSceneInjected(GameConstants.AddressablesRefs.GameScene, LoadSceneMode.Additive, _appLifetimeScope);
        }

        private async UniTask LoadGameAssets()
        {
            await UniTask.Yield();
        }

        private async UniTask InitializeServices()
        {
            IEnumerable<UniTask> initializeTasks = _services.Select(async service =>
            {
                await service.Initialize();
                Debug.Log($"Service {service.GetType().Name} initialized.");
            });

            await UniTask.WhenAll(initializeTasks);
        }

        private async UniTask ShowLoadingScreen()
        {
            //await _resourcesManager.LoadScene(GameConstants.AddressablesRefs.LoadingScreenScene);

            await UniTask.Yield();
        }

        private async UniTask InitializeEssentials()
        {
            await _resourcesManager.Initialize();
            //addressables to load loading screen right away
        }
    }
}