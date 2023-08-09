using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer.Unity;

public class PlayerProgressViewModel : IBindingContext, ITickable
{
    
    [Observable("Age")]
    private readonly IProperty<int> _age;

    private ITimeService _timeService;

    public PlayerProgressViewModel(ITimeService timeService)
    {
        _timeService = timeService;
        _age = new Property<int>(0);
    }

    public void Tick()
    {
        _age.Value = _timeService.Hour;
    }
}