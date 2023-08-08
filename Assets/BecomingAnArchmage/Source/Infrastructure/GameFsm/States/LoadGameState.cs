using BecomingAnArchmage.Source.Infrastructure.Services;

namespace BecomingAnArchmage.Source.Infrastructure.GameFsm
{
    public class LoadGameState : IGameState
    {
        private GameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadGameState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await _sceneLoader.LoadScene(GameConstants.AssetReferences.Game);
        }
    }
}