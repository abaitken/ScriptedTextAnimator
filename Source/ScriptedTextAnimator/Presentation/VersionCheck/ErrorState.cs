using System.Windows.Media;

namespace ScriptedTextAnimator.Presentation.VersionCheck
{
    internal class ErrorState : IVersionState
    {
        public Brush LocalVersionColor
        {
            get { return Brushes.Black; }
        }

        public Brush RemoteVersionColor
        {
            get { return Brushes.Black; }
        }

        public Brush InstructionTextVersionColor
        {
            get { return Brushes.Red; }
        }

        public string InstructionText
        {
            get { return "An error occured whilst checking for updates. Please try again later."; }
        }
    }
}