namespace BecomingAnArchmage.Source.Infrastructure.GameFsm
{
    public class LoadGameGameState : IGameState
    {
        private GameStateMachine _gameStateMachine;

        public LoadGameGameState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
        }
    }
}