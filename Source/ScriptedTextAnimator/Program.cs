using System;
using System.Windows;
using System.Windows.Threading;
using ScriptedTextAnimator.Presentation.ErrorHandling;
using ScriptedTextAnimator.Presentation.Main;
using ScriptedTextAnimator.Properties;

namespace ScriptedTextAnimator
{
    public class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.DispatcherUnhandledException += AppDispatcherUnhandledException;
            app.Exit += AppExit;
            app.Run(new MainWindow());
        }

        private static void AppExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }

        private static void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptionHandler.ShowWindow(null, e.Exception);
            e.Handled = false;
        }
    }
}