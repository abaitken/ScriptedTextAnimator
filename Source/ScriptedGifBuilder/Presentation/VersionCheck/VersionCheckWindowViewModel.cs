using System;
using System.Diagnostics;
using System.Windows.Input;
using TemperedSoftware.Shared.Presentation.Commands;
using System.Windows.Media;

namespace ScriptedGifBuilder.Presentation.VersionCheck
{
    class VersionCheckWindowViewModel
    {
        private readonly VersionCheckWindow view;
        private readonly Version localVersion;
        private readonly Version remoteVersion;
        private readonly IVersionState versionState;

        public VersionCheckWindowViewModel(VersionCheckWindow view, Version localVersion, Version remoteVersion)
        {
            this.view = view;
            this.localVersion = localVersion;
            this.remoteVersion = remoteVersion;
            CreateCommands();
            versionState = CreateVersionState(localVersion, remoteVersion);
        }

        private static IVersionState CreateVersionState(Version localVersion, Version remoteVersion)
        {
            if(remoteVersion == null)
                return new ErrorState();

            if (localVersion == remoteVersion)
                return new UpToDateVersionState();

            if (localVersion > remoteVersion)
                return new LaterVersionState();

            if (localVersion < remoteVersion)
                return new OutOfDateVersionState();

            throw new InvalidOperationException("Unexpected condition");
        }

        public Brush LocalVersionColor
        {
            get { return VersionState.LocalVersionColor; }
        }

        public Brush RemoteVersionColor
        {
            get { return VersionState.RemoteVersionColor; }
        }

        public Brush InstructionTextVersionColor
        {
            get { return VersionState.InstructionTextVersionColor; }
        }

        public string InstructionText
        {
            get { return VersionState.InstructionText; }
        }

        protected IVersionState VersionState
        {
            get { return versionState; }
        }

        private void CreateCommands()
        {
            CloseCommand = new DelegateCommand(OnClose);
            OpenWebsiteCommand = new DelegateCommand(OnOpenWebsiteCommand);
        }

        private void OnOpenWebsiteCommand()
        {
            Process.Start(Website);
        }

        private void OnClose()
        {
            view.Close();
        }

        public string RemoteVersion
        {
            get { return remoteVersion == null ? "?.?.?.?" : remoteVersion.ToString(); }
        }

        public Version LocalVersion
        {
            get { return localVersion; }
        }

        public ICommand OpenWebsiteCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public string Website
        {
            get { return "http://projects.logaans-site.co.uk/scripted-gif-builder"; }
        }
    }
}