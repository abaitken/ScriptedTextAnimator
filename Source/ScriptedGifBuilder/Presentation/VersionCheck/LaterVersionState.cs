using System.Windows.Media;

namespace ScriptedGifBuilder.Presentation.VersionCheck
{
    internal class LaterVersionState : IVersionState
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
            get { return "You have a later version. This might be because you have a beta build or the remote version has been temporarily downgraded. Please visit the website for more information"; }
        }
    }
}