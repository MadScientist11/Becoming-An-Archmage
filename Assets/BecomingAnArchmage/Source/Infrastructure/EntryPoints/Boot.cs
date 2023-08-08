using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.GameFsm;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace BecomingAnArchmage.Source.Infrastructure.EntryPoints
{
    public class Boot : MonoBehaviour
    {
        private IReadOnlyList<IInitializableService> _services;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake() => 
            _gameStateMachine.SwitchState(GameState.Boot);
    }
}