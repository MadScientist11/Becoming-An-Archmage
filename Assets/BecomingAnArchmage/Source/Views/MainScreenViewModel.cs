using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace BecomingAnArchmage.Source.Views
{
    public class MainScreenViewModel : IBindingContext
    {
        [Observable] private readonly IReadOnlyProperty<ProgressionPanelsViewModel> _progression;
        [Observable] private readonly IReadOnlyProperty<PlayerProgressViewModel> _playerProgress;

        public MainScreenViewModel(PlayerProgressViewModel playerProgressViewModel,
            ProgressionPanelsViewModel progressionViewModel)
        {
            _progression = new ReadOnlyProperty<ProgressionPanelsViewModel>(progressionViewModel);
            _playerProgress = new ReadOnlyProperty<PlayerProgressViewModel>(playerProgressViewModel);
        }
    }
}