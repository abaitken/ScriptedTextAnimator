using System.Windows.Media;

namespace ScriptedTextAnimator.Presentation.VersionCheck
{
    internal class UpToDateVersionState : IVersionState
    {
        public Brush LocalVersionColor
        {
            get { return Brushes.Green; }
        }

        public Brush RemoteVersionColor
        {
            get { return Brushes.Green; }
        }

        public Brush InstructionTextVersionColor
        {
            get { return Brushes.Green; }
        }

        public string InstructionText
        {
            get { return "You already have the latest version."; }
        }
    }
}