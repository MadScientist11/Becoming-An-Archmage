using System;
using System.Collections.Generic;
using System.Linq;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UITK;
using VContainer;

namespace BecomingAnArchmage.Source.Infrastructure.MVVM
{
    public class BaseView<TBindingContext> : DocumentView<TBindingContext>
        where TBindingContext : class, IBindingContext
    {
        private TBindingContext _viewModel;
        private IValueConverter[] _valueConverters;

        [Inject]
        public void Construct(TBindingContext viewModel, IEnumerable<IValueConverter> valueConverters)
        {
            _viewModel = viewModel;
            _valueConverters = valueConverters.ToArray();
        }

        protected override TBindingContext GetBindingContext()
        {
            return _viewModel;
        }

        protected override IValueConverter[] GetValueConverters()
        {
            return _valueConverters;
        }

        protected override IReadOnlyDictionary<Type, object> GetCollectionItemTemplates()
        {
            return null;
        }
    }
}
