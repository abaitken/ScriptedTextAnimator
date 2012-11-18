using System.Windows.Media;

namespace ScriptedGifBuilder.Presentation.VersionCheck
{
    internal interface IVersionState
    {
        Brush LocalVersionColor { get; }
        Brush RemoteVersionColor { get; }
        Brush InstructionTextVersionColor { get; }
        string InstructionText { get; }
    }
}