using System.Collections.ObjectModel;
using UnityEngine;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Views
{
    public class MainScreenViewModel : IBindingContext, IInitializable
    {
        [Observable] private readonly IReadOnlyProperty<ProgressionPanelsViewModel> _progression;
        [Observable] private readonly IReadOnlyProperty<PlayerProgressViewModel> _playerProgress;


        [Observable("TaskItems")]
        private readonly IReadOnlyProperty<ObservableCollection<TaskItemViewModel>> _taskItems;
        public MainScreenViewModel(PlayerProgressViewModel playerProgressViewModel,
            ProgressionPanelsViewModel progressionViewModel)
        {
            _progression = new ReadOnlyProperty<ProgressionPanelsViewModel>(progressionViewModel);
            _playerProgress = new ReadOnlyProperty<PlayerProgressViewModel>(playerProgressViewModel);
            
            _taskItems =
                new ReadOnlyProperty<ObservableCollection<TaskItemViewModel>>(
                    new ObservableCollection<TaskItemViewModel>());
        }

        public void Initialize()
        {
            _taskItems.Value.Add(new TaskItemViewModel());
            Debug.Log(_taskItems.Value[0]);
        }
    }
}