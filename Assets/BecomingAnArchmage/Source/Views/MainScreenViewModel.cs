using UnityMvvmToolkit.Core.Attributes;
using UnityMvvmToolkit.Core.Interfaces;

namespace BecomingAnArchmage.Source.Views
{
    public class MainScreenViewModel : IBindingContext
    {
        [Observable("Age")]
        private IReadOnlyProperty<int> _age => _playerProgressViewModel.Age;
        
        [Observable("Test")]
        private IReadOnlyProperty<int> _test => _progressionPanelsViewModel.Test;

        private PlayerProgressViewModel _playerProgressViewModel;
        private ProgressionPanelsViewModel _progressionPanelsViewModel;

        public MainScreenViewModel(PlayerProgressViewModel playerProgressViewModel,
            ProgressionPanelsViewModel progressionPanelsViewModel)
        {
            _progressionPanelsViewModel = progressionPanelsViewModel;
            _playerProgressViewModel = playerProgressViewModel;
        }
        
        
    }
}