using System;
using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BecomingAnArchmage.Source.Infrastructure.Services.Initialization
{
    public class InitializationService : IInitializationService
    {
        private readonly AppLifetimeScope _appLifetimeScope;
        private readonly IReadOnlyList<IInitializableService> _services;
        private readonly IResourceManager _resourceManager;
        private readonly ISceneLoader _sceneLoader;

        private float _maxInitializationValue = 0;
        private float _initializationProgress;

        public float InitializationProgress
        {
            get => _initializationProgress / _maxInitializationValue;
            private set
            {
                _initializationProgress = value;
                ProgressChanged?.Invoke(InitializationProgress);
            }
        }

        public event Action<float> ProgressChanged;

        public InitializationService(AppLifetimeScope appLifetimeScope, IReadOnlyList<IInitializableService> services, IResourceManager resourceManager, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _resourceManager = resourceManager;
            _services = services;
            _appLifetimeScope = appLifetimeScope;
        }

        public async UniTask InitializeGame()
        {
            foreach (IInitializableService service in _services)
            {
                _maxInitializationValue += service.Weight;
            }

            Debug.Log(_maxInitializationValue);

            InitializationChain initializationChain = new InitializationChain();
            initializationChain.AddInitializationStep(InitializeEssentials);
            initializationChain.AddInitializationStep(ShowLoadingScreen);
            initializationChain.AddInitializationStep(InitializeServices);
            
            await initializationChain.ExecuteInitializationAsync();
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

        private async UniTask InitializeServices()
        {
            IEnumerable<UniTask> initializeTasks = _services.Select(async service =>
            {
                await service.Initialize();
                InitializationProgress = _initializationProgress + service.Weight;
                Debug.Log($"Service {service.GetType().Name} initialized.");
            });

            await UniTask.WhenAll(initializeTasks);
        }
    }

    public interface IInitializationService
    {
        UniTask InitializeGame();
        event Action<float> ProgressChanged;
    }

    public interface IHaveInitializationWeight
    {
        float Weight { get; }
    }
}