using System.Collections;
using System.Collections.Generic;
using BecomingAnArchmage.Source.Infrastructure.MVVM;
using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

public class ProgressionPanelsView : BaseView<ProgressionPanelsViewModel>
{
 
}

public class ProgressionPanelsViewModel : IBindingContext
{
        
    [Observable("Test")]
    private readonly IProperty<int> _test;

    private ITimeService _timeService;

    public ProgressionPanelsViewModel()
    {
        _test = new Property<int>(0);
    }
}
