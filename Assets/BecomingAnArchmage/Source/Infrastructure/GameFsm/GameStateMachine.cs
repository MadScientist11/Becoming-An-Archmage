using System.Collections.Generic;
using UnityEngine;

namespace BecomingAnArchmage.Source.Infrastructure.GameFsm
{
    public enum GameState
    {
        Boot = 0,
    }

    public class GameStateMachine
    {
        private readonly Dictionary<GameState, IGameState> _states = new();
        private readonly StatesFactory _statesFactory;
        private IGameState _currentState;
        
        public GameStateMachine(StatesFactory statesFactory)
        {
            _statesFactory = statesFactory;
        }
        
        public void SwitchState(GameState nextState)
        {

            if (!_states.TryGetValue(nextState, out var _))
            {
                _states.Add(nextState, _statesFactory.CreateState(nextState));
            }
            
            if (_states[nextState] == _currentState)
            {
                Debug.LogWarning($"State {nextState} is already active");
                return;
            }

            if (_currentState is IExitableGameState state)
            {
                state.Exit();
            }
            
            _currentState = _states[nextState];
            _currentState.Enter();
        }
    }
}