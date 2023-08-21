using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace BecomingAnArchmage.Source.Views
{
    public class LoadingScreenViewModel : IBindingContext
    {
        [Observable] private readonly IProperty<float> _value;

        private ITimeService _timeService;
        private readonly IPlayerLifeCycleService _playerLifeCycleService;

        public LoadingScreenViewModel(IPlayerLifeCycleService playerLifeCycleService)
        {
            _playerLifeCycleService = playerLifeCycleService;
            _value = new Property<float>(100);
        }

    }
}