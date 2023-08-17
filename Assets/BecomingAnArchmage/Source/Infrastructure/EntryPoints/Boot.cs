using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using BecomingAnArchmage.Source.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.EntryPoints
{
    public class Boot : IAsyncStartable
    {
        private IReadOnlyList<IInitializableService> _services;
        private IResourceManager _resourcesManager;

        [Inject]
        public void Construct(IReadOnlyList<IInitializableService> services, IResourceManager resourceManager)
        {
            _resourcesManager = resourceManager;
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
            await _resourcesManager.LoadScene(GameConstants.AddressablesRefs.GameScene);
        }

        private async UniTask LoadGameAssets()
        {
            await UniTask.Yield();
        }

        private async UniTask InitializeServices()
        {
            await UniTask.WhenAll(_services.Select(service => service.Initialize().ContinueWith(() =>
            {
                Debug.Log($"Service {service.GetType().Name} initialized.");
            })));
        }

        private async UniTask ShowLoadingScreen()
        {
            await _resourcesManager.LoadScene(GameConstants.AddressablesRefs.LoadingScreenScene);

            await UniTask.Yield();
        }

        private async UniTask InitializeEssentials()
        {
            await _resourcesManager.Initialize();
            //addressables to load loading screen right away
        }
    }
}