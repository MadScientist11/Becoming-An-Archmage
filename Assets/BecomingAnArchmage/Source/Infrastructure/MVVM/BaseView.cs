using System;
using System.Collections.Generic;
using System.Linq;
using BecomingAnArchmage.Source.Infrastructure.Services;
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
        private ICollectionItemProvider _collectionItemProvider;

        [Inject]
        public void Construct(TBindingContext viewModel, IEnumerable<IValueConverter> valueConverters, ICollectionItemProvider collectionItemProvider)
        {
            _collectionItemProvider = collectionItemProvider;
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
            return _collectionItemProvider.CollectionItems;
        }
    }
}
