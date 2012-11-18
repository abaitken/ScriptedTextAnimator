using System;
using System.Windows.Input;
using ScriptedTextAnimator.Background;
using TemperedSoftware.Shared.Presentation.Commands;
using TemperedSoftware.Shared.Presentation.PresentationModel;
using System.Diagnostics;

namespace ScriptedTextAnimator.Presentation.About
{
    internal class AboutWindowViewModel : ViewModelBase
    {
        private readonly ApplicationInformation applicationInformation;
        private readonly AboutWindow view;
        private readonly VersionChecker versionChecker;

        public AboutWindowViewModel(AboutWindow view)
        {
            this.view = view;
            applicationInformation = new ApplicationInformation();
            versionChecker = new VersionChecker(view, true);
            versionChecker.TaskFinished += VersionCheckerTaskFinished;
            CreateCommands();
        }

        public ApplicationInformation ApplicationInformation
        {
            get { return applicationInformation; }
        }

        public ICommand CloseCommand { get; set; }
        public ICommand UpdateCheckCommand { get; set; }
        public ICommand OpenWebsiteCommand { get; set; }

        private void CreateCommands()
        {
            CloseCommand = new DelegateCommand(OnClose);
            UpdateCheckCommand = new DelegateCommand(OnUpdateCheck, CanExecuteUpdateCheck);
            OpenWebsiteCommand = new DelegateCommand(OnOpenWebsiteCommand);
        }

        private void OnOpenWebsiteCommand()
        {
            Process.Start(Website);
        }

        private bool CanExecuteUpdateCheck()
        {
            return !versionChecker.IsWorking;
        }

        private void OnUpdateCheck()
        {
            versionChecker.Check();
            SendPropertyChanged("UpdateCheckCommand");
        }

        private void VersionCheckerTaskFinished(object sender, EventArgs<Version> e)
        {
            SendPropertyChanged("UpdateCheckCommand");
        }

        private void OnClose()
        {
            view.Close();
        }

        public string Website
        {
            get { return "http://temperedsoftware.co.uk/ScriptedTextAnimator"; }
        }
    }
}