using System;

namespace ScriptedTextAnimator.Instructions
{
    public interface IInstructionProperty : IValidationModel
    {
        Type TargetType { get; }
        string Name { get; }
        object Value { get; set; }
        IValueStrategy ValueStrategy { get; }
    }
}