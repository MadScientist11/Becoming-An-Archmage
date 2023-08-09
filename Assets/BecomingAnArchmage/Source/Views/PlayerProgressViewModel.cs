using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer.Unity;

public class PlayerProgressViewModel : IBindingContext, ITickable
{
    
    public readonly IProperty<int> Age;

    private ITimeService _timeService;

    public PlayerProgressViewModel(ITimeService timeService)
    {
        _timeService = timeService;
        Age = new Property<int>(0);
    }

    public void Tick()
    {
        Age.Value = (int)(100f*_timeService.DeltaTime);
        Debug.Log(Age.Value);
    }
}