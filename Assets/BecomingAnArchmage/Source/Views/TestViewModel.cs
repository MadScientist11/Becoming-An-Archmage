using BecomingAnArchmage.Source.Infrastructure.Services;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Views
{
    public class TestViewModel : IBindingContext, ITickable
    {
        [Observable("Time")]
        private readonly IProperty<float> _time;

        private readonly ITimeService _timeService;

        public TestViewModel(ITimeService timeService)
        {
            _timeService = timeService;
            _time = new Property<float>(0f);
        }

        public void Tick()
        {
            _time.Value = _timeService.DeltaTime;
        }
    }
}