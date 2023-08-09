using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Gameplay.Services
{
    public interface IPlayerLifeCycleService
    {
        float Days { get; }
        float Age { get; }
    }

    public class PlayerLifeCycleService : IPlayerLifeCycleService, ITickable
    {
        public float Age => Days / 365;
        public float Days { get; private set; }
        
        
        private readonly ITimeService _timeService;

        public PlayerLifeCycleService(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void Tick()
        {
            Debug.Log((int)Days);
            Debug.Log((int)Age);
            Debug.Log(Age);
            Days += _timeService.DeltaTime * 20;
        }
    }
}