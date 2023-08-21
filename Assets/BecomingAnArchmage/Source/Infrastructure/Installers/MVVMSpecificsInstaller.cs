using System;
using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Views;
using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.Installers
{
    public class MVVMSpecificsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            RegisterValueConverters(builder);
            RegisterViewModels(builder);
            RegisterCollectionItemTemplates(builder);
        }

        private void RegisterValueConverters(IContainerBuilder builder)
        {
            builder.Register<IValueConverter, FloatToStrConverter>(Lifetime.Singleton);
            builder.Register<IValueConverter, IntToStrConverter>(Lifetime.Singleton);
        }

        private void RegisterViewModels(IContainerBuilder builder)
        {
            builder.Register<TestViewModel>(Lifetime.Singleton).AsSelf().As<ITickable>();
            builder.Register<PlayerProgressViewModel>(Lifetime.Singleton).AsSelf().As<IDisposable>();
            builder.Register<ProgressionPanelsViewModel>(Lifetime.Singleton).AsSelf();
            builder.Register<MainScreenViewModel>(Lifetime.Singleton).AsSelf().As<IInitializable>();
            
            builder.Register<LoadingScreenViewModel>(Lifetime.Singleton).AsSelf();
            
            builder.Register<TaskItemViewModel>(Lifetime.Singleton).AsSelf();

        }

        private void RegisterCollectionItemTemplates(IContainerBuilder builder)
        {
            Dictionary<Type, object> templates =  new Dictionary<Type, object>
            {
                { typeof(TaskItemViewModel), null }
            };
            builder.Register<ICollectionItemProvider>(_ => new CollectionItemsProvider(templates), Lifetime.Singleton);
        }
    }
}