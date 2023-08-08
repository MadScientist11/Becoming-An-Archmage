using System;
using VContainer;

namespace BecomingAnArchmage.Source.Infrastructure.GameFsm
{
    public class StatesFactory
    {
        private readonly IObjectResolver _instantiator;

        public StatesFactory(IObjectResolver instantiator)
        {
            _instantiator = instantiator;
        }

        public IGameState CreateState(GameState gameState)
        {
            return gameState switch
            {
                GameState.Boot => _instantiator.Resolve<BootState>(),
                _ => throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null)
            };
        }
    }
}