namespace ScriptedGifBuilder.Presentation.About
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    internal partial class AboutWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            ViewModel = new AboutWindowViewModel(this);
        }

        public AboutWindowViewModel ViewModel
        {
            get { return DataContext as AboutWindowViewModel; }
            set { DataContext = value; }
        }
    }
}
