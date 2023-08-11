using System;
using BecomingAnArchmage.Source.Gameplay.Services;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer.Unity;

public class PlayerProgressViewModel : IBindingContext, IDisposable
{
    [Observable] private readonly IProperty<string> _age;
    [Observable] private readonly IProperty<string> _days;

    private ITimeService _timeService;
    private readonly IPlayerLifeCycleService _playerLifeCycleService;

    public PlayerProgressViewModel(IPlayerLifeCycleService playerLifeCycleService)
    {
        _playerLifeCycleService = playerLifeCycleService;
        _age = new Property<string>(GetAgeString(playerLifeCycleService.Age));
        _days = new Property<string>(GetDaysString(playerLifeCycleService.Days));
        
        _playerLifeCycleService.AgeChanged += OnAgeChanged;
        _playerLifeCycleService.DaysChanged += OnDaysChanged;
    }

    public void Dispose()
    {
        _playerLifeCycleService.AgeChanged -= OnAgeChanged;
        _playerLifeCycleService.DaysChanged -= OnDaysChanged;
    }

    private void OnAgeChanged(object sender, int age)
    {
        _age.Value = GetAgeString(age);
    }

    private void OnDaysChanged(object sender, int days)
    {
        _days.Value = GetDaysString(days);
    }

    private string GetAgeString(int age)
    {
        return $"Age: {age}";
    }

    private string GetDaysString(int days)
    {
        return $"Days: {days % 365}";
    }


  
}