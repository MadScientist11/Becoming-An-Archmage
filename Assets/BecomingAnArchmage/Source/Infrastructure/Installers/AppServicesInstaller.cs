using System;
using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Infrastructure.Services.Initialization;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.Installers
{
    public class DummyService1 : IInitializableService
    {
        public float Weight { get; } = 1;
        public async UniTask Initialize()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Weight));
        }
    }
    
    public class DummyService2 : IInitializableService
    {
        public float Weight { get; } = 4;
        public async UniTask Initialize()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Weight));
        }
    }
    
    public class DummyService3 : IInitializableService
    {
        public float Weight { get; } = 9;
        public async UniTask Initialize()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Weight));
        }
    }
    
    public class DummyService4 : IInitializableService
    {
        public float Weight { get; } = 11;
        public async UniTask Initialize()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Weight));
        }
    }
    public class AppServicesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ResourceManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InitializationService>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<DummyService1>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DummyService2>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DummyService3>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DummyService4>(Lifetime.Singleton).AsImplementedInterfaces();
            
            
            RegisterGameplayServices(builder);
        }

        private void RegisterGameplayServices(IContainerBuilder builder)
        {
            builder.Register<PlayerLifeCycleService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}