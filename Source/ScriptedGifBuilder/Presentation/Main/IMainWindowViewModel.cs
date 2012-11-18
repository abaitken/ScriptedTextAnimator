using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using ScriptedGifBuilder.Instructions;

namespace ScriptedGifBuilder.Presentation.Main
{
    internal interface IMainWindowViewModel : INotifyPropertyChanged
    {
        MainWindow View { get; }
        ObservableCollection<ScriptedInstructionViewModel> Script { get; }
        int ProgressValue { get; }
        ImageSource Image { get; }
        string StatusText { get; }
        ScriptedInstructionViewModel SelectedInstruction { get; set; }
        ScriptedInstructionViewModel ProjectInstructionsViewModel { get; }
        IEnumerable<InstructionItem> Actions { get; }
        InstructionItem SelectedAction { get; set; }
        int SelectedIndex { get; set; }
        ICommand NewProjectCommand { get; }
        ICommand SaveProjectCommand { get; }
        ICommand OpenProjectCommand { get; }
        ICommand AddActionCommand { get; }
        ICommand InsertActionCommand { get; }
        ICommand DeleteActionCommand { get; }
        ICommand RenderPreviewCommand { get; }
        ICommand RenderToDiskCommand { get; }
        ICommand AboutCommand { get; }
        ICommand DocumentationCommand { get; }
        ICommand ExitCommand { get; }
        ICommand OpenRecentProjectCommand { get; }
        string WindowTitle { get; }
        bool HasChanges { get; set; }
        void OnLoaded();
        bool OnWindowClosing();
        IEnumerable<string> RecentFiles { get; }
    }
}