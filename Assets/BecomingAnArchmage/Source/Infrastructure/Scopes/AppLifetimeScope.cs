using BecomingAnArchmage.Source.Infrastructure.EntryPoints;
using BecomingAnArchmage.Source.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.Scopes
{
    public class AppLifetimeScope : LifetimeScope
    {
        [SerializeField] private VisualTreeAsset _taskItemAsset;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Boot>();

            new AppServicesInstaller(builder).RegisterServices();
            new MVVMSpecificsInstaller(builder).RegisterMVVMSpecifics();
            
        }
    }
}