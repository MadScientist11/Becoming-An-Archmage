using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure.EntryPoints
{
    public class Boot : IInitializable
    {
        private IReadOnlyList<IInitializableService> _services;
        private AppLifetimeScope _appLifetimeScope;

        [Inject]
        public void Construct(AppLifetimeScope appLifetimeScope)
        {
            _appLifetimeScope = appLifetimeScope;
        }

        public void Initialize()
        {
            Object.DontDestroyOnLoad(_appLifetimeScope);
        }
    }
}