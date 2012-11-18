using System.Windows.Media;

namespace ScriptedTextAnimator.Presentation.VersionCheck
{
    internal interface IVersionState
    {
        Brush LocalVersionColor { get; }
        Brush RemoteVersionColor { get; }
        Brush InstructionTextVersionColor { get; }
        string InstructionText { get; }
    }
}