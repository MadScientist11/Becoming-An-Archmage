using System;
using UnityMvvmToolkit.Common.Interfaces;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;

namespace BecomingAnArchmage.Source.Views
{
    public class TaskItemViewModel : ICollectionItem
    {
        private readonly IProperty<string> _name = new Property<string>();

        public TaskItemViewModel()
        {
            Id = Guid.NewGuid().GetHashCode();
        }

        public int Id { get; }

        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }
    }
}