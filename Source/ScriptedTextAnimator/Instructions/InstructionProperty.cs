using System;
using System.Reflection;
using ScriptedTextAnimator.Presentation;
using TemperedSoftware.Shared.Presentation.PresentationModel;

namespace ScriptedTextAnimator.Instructions
{
    public interface IValidationModel
    {
        bool IsValid { get; }
        string ValidationMessage { get; }
        void Validate();
    }

    abstract class ValidationModel : ViewModelBase, IValidationModel
    {

        bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set
            {
                if (isValid == value)
                    return;

                isValid = value;
                SendPropertyChanged("IsValid");
            }
        }


        string validationMessage;
        public string ValidationMessage
        {
            get { return validationMessage; }
            set
            {
                if (validationMessage == value)
                    return;

                validationMessage = value;
                SendPropertyChanged("ValidationMessage");
            }
        }

        public abstract void Validate();
    }


    internal class InstructionProperty : ValidationModel, IInstructionProperty
    {
        private readonly ScriptedInstructionViewModel parentViewModel;
        private readonly ScriptedPropertyAttribute propertyAttribute;
        private readonly PropertyInfo propertyInfo;

        public InstructionProperty(ScriptedPropertyAttribute propertyAttribute, PropertyInfo propertyInfo,
                                   ScriptedInstructionViewModel parentViewModel)
        {
            this.propertyAttribute = propertyAttribute;
            this.propertyInfo = propertyInfo;
            this.parentViewModel = parentViewModel;
            IsValid = true;
        }

        #region IInstructionProperty Members

        public Type TargetType
        {
            get { return propertyInfo.PropertyType; }
        }

        public string Name
        {
            get { return propertyAttribute.DisplayName; }
        }

        public object Value
        {
            get { return propertyInfo.GetValue(parentViewModel.ScriptedInstruction); }
            set
            {
                var result = Value != null &&
                             (bool)
                             TargetType.InvokeMember("Equals", BindingFlags.InvokeMethod, null, Value, new[] {value});

                propertyInfo.SetValue(parentViewModel.ScriptedInstruction, value);
                parentViewModel.Update();

                if (!result)
                    SendPropertyChanged("Value");

                Validate();
            }
        }

        public IValueStrategy ValueStrategy
        {
            get { return propertyAttribute.ValueStrategy; }
        }

        public override void Validate()
        {
            var validationResult = propertyAttribute.ValueStrategy.Validate(Value);
            IsValid = validationResult.IsValid;
            ValidationMessage = (string) validationResult.ErrorContent;
        }

        #endregion
    }
}