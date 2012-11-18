namespace ScriptedGifBuilder.Presentation.VersionCheck
{
    /// <summary>
    /// Interaction logic for VersionCheckWindow.xaml
    /// </summary>
    internal partial class VersionCheckWindow
    {
        public VersionCheckWindow()
        {
            InitializeComponent();
        }

        public VersionCheckWindowViewModel ViewModel
        {
            get { return DataContext as VersionCheckWindowViewModel; }
            set { DataContext = value; }
        }
    }
}
