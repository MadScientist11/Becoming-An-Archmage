using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer.Unity;

public class PlayerProgressViewModel : IBindingContext, ITickable
{
    
    public IProperty<string> Age { get; }
    public IProperty<string> Days { get; }

    private ITimeService _timeService;
    private IPlayerLifeCycleService _playerLifeCycleService;

    public PlayerProgressViewModel(IPlayerLifeCycleService playerLifeCycleService)
    {
        _playerLifeCycleService = playerLifeCycleService;
        Age = new Property<string>($"Age: {playerLifeCycleService.Age}");
        Days = new Property<string>($"Days: {playerLifeCycleService.Days}");
    }


    public void Tick()
    {
        Age.Value = $"Age: {(int)_playerLifeCycleService.Age}";
        Days.Value = $"Days: {(int)_playerLifeCycleService.Days % 365}";
        
    }
}