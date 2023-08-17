using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.Services;
using VContainer;

namespace BecomingAnArchmage.Source.Infrastructure.Installers
{
    public class AppServicesInstaller
    {
        private readonly IContainerBuilder _scopeBuilder;

        public AppServicesInstaller(IContainerBuilder scopeBuilder)
        {
            _scopeBuilder = scopeBuilder;
        }
        
        public void RegisterServices()
        {
            _scopeBuilder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            _scopeBuilder.Register<TimeService>(Lifetime.Singleton).AsImplementedInterfaces();
            RegisterGameplayServices();
        }

        private void RegisterGameplayServices()
        {
            _scopeBuilder.Register<PlayerLifeCycleService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}