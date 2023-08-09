using BecomingAnArchmage.Source.Infrastructure.GameFsm;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Views;
using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer;
using VContainer.Unity;
using ITickable = VContainer.Unity.ITickable;

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
            builder.Register<TestViewModel>(Lifetime.Singleton).AsSelf().As<ITickable>();
            builder.Register<PlayerProgressViewModel>(Lifetime.Singleton).AsSelf().As<ITickable>();
            builder.Register<ProgressionPanelsViewModel>(Lifetime.Singleton).AsSelf();
            builder.Register<TimeService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IValueConverter, FloatToStrConverter>(Lifetime.Singleton);
            builder.Register<IValueConverter, IntToStrConverter>(Lifetime.Singleton);
        }
    }
}
