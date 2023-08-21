using System;
using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BecomingAnArchmage.Source.Infrastructure.Services.Initialization
{
    public interface IInitializationService
    {
        UniTask InitializeGame();
    }

    public class InitializationService : IInitializationService
    {
        private readonly AppLifetimeScope _appLifetimeScope;
        private readonly IReadOnlyList<IInitializableService> _services;
        private readonly IResourceManager _resourceManager;
        private readonly ISceneLoader _sceneLoader;

        public InitializationService(AppLifetimeScope appLifetimeScope, IReadOnlyList<IInitializableService> services, IResourceManager resourceManager, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _resourceManager = resourceManager;
            _services = services;
            _appLifetimeScope = appLifetimeScope;
        }

        public async UniTask InitializeGame()
        {
            InitializationChain initializationChain = new InitializationChain();
            initializationChain.AddInitializationStep(InitializeEssentials);
            initializationChain.AddInitializationStep(ShowLoadingScreen);
            initializationChain.AddInitializationStep(InitializeServices);
            initializationChain.AddInitializationStep(LoadGameAssets);
            
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