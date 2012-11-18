using System.Collections.Generic;
using System.Linq;
using ScriptedTextAnimator.Instructions;
using ScriptedTextAnimator.Presentation.Main;

namespace ScriptedTextAnimator.Presentation
{
    internal class ScriptedInstructionViewModel : ValidationModel
    {
        private readonly IMainWindowViewModel parentViewModel;

        private ScriptedInstruction scriptedInstruction;
        private IEnumerable<IInstructionProperty> properties;

        public ScriptedInstructionViewModel(IMainWindowViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;
            IsValid = true;
        }

        public ScriptedInstruction ScriptedInstruction
        {
            get { return scriptedInstruction; }
            set
            {
                if (scriptedInstruction == value)
                    return;

                scriptedInstruction = value;
                SendPropertyChanged("ScriptedInstruction");
            }
        }

        public string DisplayText
        {
            get
            {
                if (ScriptedInstruction == null)
                    return string.Empty;

                return ScriptedInstruction.DisplayText;
            }
        }

        public void Update()
        {
            SendPropertyChanged("DisplayText");
            parentViewModel.HasChanges = true;
        }

        public IEnumerable<IInstructionProperty> Properties
        {
            get
            {
                if (scriptedInstruction == null)
                    return Enumerable.Empty<IInstructionProperty>();

                if(properties == null)
                    properties = CreateProperties();

                return properties;
            }
        }

        private IEnumerable<IInstructionProperty> CreateProperties()
        {
            var results = new List<IInstructionProperty>();

            var helper = new InstructionFactory(ScriptedInstruction.GetType());
            var instructionProperties = helper.GetScriptedPropertyInformation();

            foreach (var item in instructionProperties)
            {
                var propertyInfo = item.Key;
                var propertyAttribute = item.Value;
                results.Add(new InstructionProperty(propertyAttribute, propertyInfo, this));
            }
            return results;
        }

        public override void Validate()
        {
            var result = true;

            foreach (var instructionProperty in Properties)
            {
                instructionProperty.Validate();
                result = result && instructionProperty.IsValid;
            }

            ValidationMessage = result ? string.Empty : "Properties of this action are in an invalid state";

            IsValid = result;
        }
    }
}