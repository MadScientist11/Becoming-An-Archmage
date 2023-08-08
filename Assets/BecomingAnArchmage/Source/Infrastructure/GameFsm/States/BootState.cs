using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.Scopes;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;

namespace BecomingAnArchmage.Source.Infrastructure.GameFsm
{
    public class BootState : IGameState
    {
        private GameStateMachine _gameStateMachine;
        private readonly IReadOnlyList<IInitializableService> _allServices;
        private readonly AppLifetimeScope _appLifetimeScope;

        public BootState(GameStateMachine gameStateMachine, AppLifetimeScope appLifetimeScope,
            IReadOnlyList<IInitializableService> allServices)
        {
            _appLifetimeScope = appLifetimeScope;
            _allServices = allServices;
            _gameStateMachine = gameStateMachine;
        }


        public void Enter()
        {
            Object.DontDestroyOnLoad(_appLifetimeScope);

            InitializeServices(_allServices);
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