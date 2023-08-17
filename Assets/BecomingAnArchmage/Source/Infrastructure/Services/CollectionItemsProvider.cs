using System;
using System.Collections.Generic;
using UnityMvvmToolkit.Common.Interfaces;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface ICollectionItemProvider
    {
        public IReadOnlyDictionary<Type, object> CollectionItems { get; }
        T Get<T>() where T : class, ICollectionItem;
    }

    public class CollectionItemsProvider : ICollectionItemProvider
    {
        public IReadOnlyDictionary<Type, object> CollectionItems { get; }

        public CollectionItemsProvider(IReadOnlyDictionary<Type, object> collectionItems)
        {
            CollectionItems = collectionItems;
        }

        public T Get<T>() where T : class, ICollectionItem
        {
            return CollectionItems[typeof(T)] as T;
        }
    }
}