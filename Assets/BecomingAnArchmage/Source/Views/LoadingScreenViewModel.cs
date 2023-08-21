using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.Services;
using BecomingAnArchmage.Source.Infrastructure.Services.Initialization;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace BecomingAnArchmage.Source.Views
{
    public class LoadingScreenViewModel : IBindingContext
    {
        [Observable] private readonly IProperty<float> _progressValue;

        private readonly IPlayerLifeCycleService _playerLifeCycleService;
        private readonly IInitializationService _initializationService;

        public LoadingScreenViewModel(IInitializationService initializationService)
        {
            _initializationService = initializationService;
            _progressValue = new Property<float>(0);

            _initializationService.ProgressChanged += UpdateProgressBar;
        }

        private void UpdateProgressBar(float value)
        {
            _progressValue.Value = value*100;
        }
    }
}