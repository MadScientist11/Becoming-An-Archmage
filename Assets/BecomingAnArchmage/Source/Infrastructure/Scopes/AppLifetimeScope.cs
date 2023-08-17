using System;
using System.Collections.Generic;
using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.GameFsm;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Views;
using UnityEngine;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core.Converters.PropertyValueConverters;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.Scopes
{
    public class AppLifetimeScope : LifetimeScope
    {
        [SerializeField] private VisualTreeAsset _taskItemAsset;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameStateMachine(builder);
            RegisterServices(builder);
            
            RegisterValueConverters(builder);
            RegisterViewModels(builder);
            RegisterCollectionItemTemplates(builder);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerLifeCycleService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsSelf();
            builder.Register<StatesFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<BootState>(Lifetime.Singleton).AsSelf();
            builder.Register<LoadGameState>(Lifetime.Singleton).AsSelf();
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
            
            builder.Register<TaskItemViewModel>(Lifetime.Singleton).AsSelf();

        }
        
        private void RegisterCollectionItemTemplates(IContainerBuilder builder)
        {
            Dictionary<Type, object> templates =  new Dictionary<Type, object>
            {
                { typeof(TaskItemViewModel), _taskItemAsset }
            };
            builder.Register<ICollectionItemProvider>(_ => new CollectionItemsProvider(templates), Lifetime.Singleton);
        }
    }
}