using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace BecomingAnArchmage.Source.Views
{
    public class TestViewModel : IBindingContext
    {
        [Observable("Date")]
        private readonly IReadOnlyProperty<string> _date;

        public TestViewModel()
        {
            _date = new ReadOnlyProperty<string>("05.05.2012");
        }
        
    }
}