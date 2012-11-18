using System.Windows;
namespace ScriptedTextAnimator.Presentation.ErrorHandling
{
    internal static class ExceptionHandler
    {
        public static void ShowWindow(Window owner, System.Exception exception)
        {
            ShowWindow(owner, exception, string.Empty);
        }

        public static void ShowWindow(Window owner, System.Exception exception, string message)
        {
            var view = new ExceptionMessageWindow();
            var viewModel = new ExceptionMessageViewModel(view, exception, message);
            view.ViewModel = viewModel;

            if (owner != null)
                view.Owner = owner;

            view.ShowDialog();
        }
    }
}
