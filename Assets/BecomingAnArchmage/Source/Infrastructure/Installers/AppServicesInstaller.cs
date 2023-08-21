using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.Services;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.Installers
{
    public class AppServicesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ResourceManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeService>(Lifetime.Singleton).AsImplementedInterfaces();
            RegisterGameplayServices(builder);
        }

        private void RegisterGameplayServices(IContainerBuilder builder)
        {
            builder.Register<PlayerLifeCycleService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}