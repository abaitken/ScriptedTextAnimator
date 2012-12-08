using System.Windows.Controls;

namespace ScriptedTextAnimator.ValueStrategies
{
    public interface IValueStrategy
    {
        bool IsMultiValue { get; }
        object[] Values { get; }
        object DefaultValue { get; }
        ValidationResult Validate(object value);
    }
}