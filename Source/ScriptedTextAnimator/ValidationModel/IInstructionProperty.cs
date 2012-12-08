using System;
using ScriptedTextAnimator.Instructions;
using ScriptedTextAnimator.ValueStrategies;

namespace ScriptedTextAnimator.ValidationModel
{
    public interface IInstructionProperty : IValidationModel
    {
        Type TargetType { get; }
        string Name { get; }
        object Value { get; set; }
        IValueStrategy ValueStrategy { get; }
    }
}