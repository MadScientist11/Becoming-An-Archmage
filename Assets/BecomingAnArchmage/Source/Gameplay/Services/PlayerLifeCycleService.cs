using System;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Infrastructure.Services.Initialization;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Gameplay.Services
{
    public interface IPlayerLifeCycleService
    {
        int Days { get; }
        int Age { get; }

        event EventHandler<int> AgeChanged;
        event EventHandler<int> DaysChanged;
    }

    public class PlayerLifeCycleService : IPlayerLifeCycleService, IInitializableService, ITickable, IDisposable
    {
        private int _days;
        private int _age;
        public float Weight => 3;

        public int Days
        {
            get => _days;
            set
            {
                _days = value;
                DaysChanged?.Invoke(this, _days);
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                AgeChanged?.Invoke(this, _age);
            }
        }

        public event EventHandler<int> AgeChanged;
        public event EventHandler<int> DaysChanged;

        private float _dayDuration = .001f;
        private float _elapsed;

        private readonly ITimeService _timeService;
        private IInitializationService _initializationService;

        public PlayerLifeCycleService(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public async UniTask Initialize()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Weight));
            DaysChanged += OnDaysChanged;
        }

        public void Dispose()
        {
            DaysChanged -= OnDaysChanged;
        }

        public void Tick()
        {
            _elapsed += Time.deltaTime;

            if (_elapsed < _dayDuration)
            {
                return;
            }

            _elapsed = 0.0f;

            Days++;
        }

        private void OnDaysChanged(object sender, int days)
        {
            int age = days / 365;

            
            if (age > Age)
            {
                Age = age;
            }
        }

    }
}