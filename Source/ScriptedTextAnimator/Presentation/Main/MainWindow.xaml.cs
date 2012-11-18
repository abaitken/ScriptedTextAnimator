using System.ComponentModel;
using System.Windows;

namespace ScriptedTextAnimator.Presentation.Main
{
    internal partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel(this);
        }

        public IMainWindowViewModel ViewModel
        {
            get { return DataContext as IMainWindowViewModel; }
            set { DataContext = value; }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.OnLoaded();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = ViewModel.OnWindowClosing();
        }
    }
}