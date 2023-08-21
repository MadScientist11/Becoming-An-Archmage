using System.Runtime.CompilerServices;
using BecomingAnArchmage.Source.Views.CustomBindableElements;
using UnityEngine.UIElements;
using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Extensions;
using UnityMvvmToolkit.Core.Interfaces;

namespace BecomingAnArchmage.Source.CustomBindableElements
{
    public partial class BindableProgressBar : ProgressBar, IBindableElement
    {
        private IReadOnlyProperty<float> _valueProperty;
        private PropertyBindingData _propertyBindingData;

        public virtual void SetBindingContext(IBindingContext context, IObjectProvider objectProvider)
        {
            if (string.IsNullOrWhiteSpace(BindingValuePath))
            {
                return;
            }

            _propertyBindingData ??= BindingValuePath.ToPropertyBindingData();

            _valueProperty = objectProvider.RentReadOnlyProperty<float>(context, _propertyBindingData);
            _valueProperty.ValueChanged += OnPropertyValueChanged;

            UpdateValue(_valueProperty.Value);
        }

        public virtual void ResetBindingContext(IObjectProvider objectProvider)
        {
            if (_valueProperty is null)
            {
                return;
            }

            _valueProperty.ValueChanged -= OnPropertyValueChanged;

            objectProvider.ReturnReadOnlyProperty(_valueProperty);

            _valueProperty = null;

            UpdateValue(default);
        }

        private void OnPropertyValueChanged(object sender, float newValue)
        {
            UpdateValue(newValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void UpdateValue(float newValue)
        {
            value = newValue;
        }
    }

    public partial class BindableProgressBar
    {
        public string BindingValuePath { get; private set; }

        public new class UxmlFactory : UxmlFactory<BindableProgressBar, UxmlTraits>
        {
        }

        public new class UxmlTraits : ProgressBar.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _bindingValueAttribute = new()
                { name = "binding-value-path", defaultValue = "" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                visualElement.As<BindableProgressBar>().BindingValuePath =
                    _bindingValueAttribute.GetValueFromBag(bag, context);
            }
        }
    }
}