namespace ScriptedTextAnimator.Instructions
{
    public interface IValueStrategy
    {
        bool IsMultiValue { get; }
        object[] Values { get; }
    }
}