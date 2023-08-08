using BecomingAnArchmage.Source.Infrastructure.GameFsm;
using BecomingAnArchmage.Source.Infrastructure.Services;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.Scopes
{
    public class AppLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsSelf();
            builder.Register<StatesFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<BootState>(Lifetime.Singleton).AsSelf();
            builder.Register<LoadGameState>(Lifetime.Singleton).AsSelf();
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
