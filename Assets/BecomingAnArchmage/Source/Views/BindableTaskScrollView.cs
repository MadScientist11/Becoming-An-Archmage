using UnityEngine.UIElements;
using UnityMvvmToolkit.UITK.BindableUIElements;

namespace BecomingAnArchmage.Source.Views
{
    public partial class BindableTaskScrollView : BindableScrollView<TaskItemViewModel>
    {
        public override void Initialize()
        {
            base.Initialize();

            contentViewport.style.overflow = Overflow.Visible;
            contentContainer.style.overflow = Overflow.Visible;
        }
    }
    public partial class BindableTaskScrollView
    {
        public new class UxmlFactory : UxmlFactory<BindableTaskScrollView, UxmlTraits>
        {
        }
    }
}