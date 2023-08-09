using System;
using System.Collections.Generic;
using UnityMvvmToolkit.Core.Interfaces;
using UnityMvvmToolkit.UITK;
using VContainer;

namespace BecomingAnArchmage.Source.Infrastructure.MVVM
{
    public class BaseView<TBindingContext> : DocumentView<TBindingContext>
        where TBindingContext : class, IBindingContext
    {
        private TBindingContext _viewModel;

        [Inject]
        public void Construct(TBindingContext viewModel)
        {
            _viewModel = viewModel;
        }

        protected override TBindingContext GetBindingContext()
        {
            return _viewModel;
        }

        protected override IValueConverter[] GetValueConverters()
        {
            return null;
        }

        protected override IReadOnlyDictionary<Type, object> GetCollectionItemTemplates()
        {
            return null;
        }
    }
}
