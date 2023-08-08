using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace BecomingAnArchmage.Source.Infrastructure.EntryPoints
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private AppLifetimeScope _appLifetimeScope;

        private IReadOnlyList<IInitializableService> _services;

        [Inject]
        public void Construct(IReadOnlyList<IInitializableService> allServices)
        {
            _services = allServices;
        }

        private void Awake() =>
            DontDestroyOnLoad(_appLifetimeScope);

        private void Start()
        {
            InitializeServices(_services);
        }

        private void InitializeServices(IReadOnlyList<IInitializableService> services)
        {
            foreach (IInitializableService service in services)
            {
                service.Initialize();
            }
        }
    }
}