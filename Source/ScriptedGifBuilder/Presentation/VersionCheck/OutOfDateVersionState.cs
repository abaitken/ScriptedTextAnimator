using System.Windows.Media;

namespace ScriptedGifBuilder.Presentation.VersionCheck
{
    internal class OutOfDateVersionState : IVersionState
    {
        public Brush LocalVersionColor
        {
            get { return Brushes.Red; }
        }

        public Brush RemoteVersionColor
        {
            get { return Brushes.Green; }
        }

        public Brush InstructionTextVersionColor
        {
            get { return Brushes.Red; }
        }

        public string InstructionText
        {
            get { return "You are out of date. Please visit the website and download the new version."; }
        }
    }
}