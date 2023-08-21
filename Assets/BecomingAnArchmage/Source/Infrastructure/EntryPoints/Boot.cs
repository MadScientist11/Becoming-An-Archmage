using System;
using System.Collections.Generic;
using System.Threading;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Infrastructure.Services.Initialization;
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
        private ISceneLoader _sceneLoader;
        private AppLifetimeScope _appLifetimeScope;
        private IResourceManager _resourceManager;

        [Inject]
        public void Construct(AppLifetimeScope appLifetimeScope, IReadOnlyList<IInitializableService> services, IResourceManager resourceManager, ISceneLoader sceneLoader)
        {
            _resourceManager = resourceManager;
            _appLifetimeScope = appLifetimeScope;
            _sceneLoader = sceneLoader;
            _services = services;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            InitializationChain initializationChain = new InitializationChain();
            initializationChain.AddInitializationStep(InitializeEssentials);
            initializationChain.AddInitializationStep(ShowLoadingScreen);
            initializationChain.AddInitializationStep(InitializeServices);
            initializationChain.AddInitializationStep(LoadGameAssets);
            //initializationChain.AddInitializationStep(LoadGame);


            await initializationChain.ExecuteInitializationAsync(new Progress<float>());
        }

        private async UniTask InitializeEssentials()
        {
            await _resourceManager.Initialize();
            //addressables to load loading screen right away
        }
        
        private async UniTask ShowLoadingScreen()
        {
            await _sceneLoader.LoadSceneInjected(GameConstants.AddressablesRefs.LoadingScreenScene, LoadSceneMode.Additive, _appLifetimeScope);

            await UniTask.Yield();
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
    }
}