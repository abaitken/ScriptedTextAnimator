using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Commands;
using ScriptedTextAnimator.Background;
using ScriptedTextAnimator.Instructions;
using ScriptedTextAnimator.Presentation.About;
using ScriptedTextAnimator.Presentation.ErrorHandling;
using ScriptedTextAnimator.Project;
using ScriptedTextAnimator.Properties;
using ScriptedTextAnimator.Rendering;
using ScriptedTextAnimator.ValidationModel;
using TemperedSoftware.Shared.FileSystem;
using TemperedSoftware.Shared.Presentation.Commands;
using TemperedSoftware.Shared.Presentation.PresentationModel;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ScriptedTextAnimator.Presentation.Main
{
    internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private readonly Animator animator;
        private readonly VersionChecker versionChecker;
        private readonly MainWindow view;
        private IEnumerable<InstructionItem> actions;
        private bool hasChanges;
        private ImageSource image;
        private bool loaded;
        private string outputFileName;
        private int progressValue;
        private ProjectInstructions projectInstructions;
        private ScriptedInstructionViewModel projectInstructionsViewModel;
        private string projectPath;
        private ObservableCollection<ScriptedInstructionViewModel> script;
        private InstructionItem selectAction;
        private int selectedIndex;
        private ScriptedInstructionViewModel selectedInstruction;
        private string statusText;
        private readonly ApplicationInformation applicationInformation;
        private MRUManager mruManager;

        public MainWindowViewModel(MainWindow view)
        {
            this.view = view;
            animator = new Animator();
            animator.ProgressUpdate += OnProgressUpdate;

            CreateCommands();

            SelectedAction = Actions.First();

            OnNewProject();

            versionChecker = new VersionChecker(view, false);
            applicationInformation = new ApplicationInformation();
            mruManager = new MRUManager();
        }

        public string ProjectPath
        {
            get { return projectPath; }
            set
            {
                if (projectPath == value)
                    return;

                projectPath = value;
                SendPropertyChanged("ProjectPath");
                SendPropertyChanged("WindowTitle");
            }
        }

        #region IMainWindowViewModel Members

        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (hasChanges == value)
                    return;

                hasChanges = value;
                SendPropertyChanged("HasChanges");
                SendPropertyChanged("WindowTitle");
            }
        }

        public ICommand NewProjectCommand { get; set; }
        public ICommand SaveProjectCommand { get; set; }
        public ICommand OpenProjectCommand { get; set; }
        public ICommand AddActionCommand { get; set; }
        public ICommand InsertActionCommand { get; set; }
        public ICommand DeleteActionCommand { get; set; }
        public ICommand RenderPreviewCommand { get; set; }
        public ICommand RenderToDiskCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand DocumentationCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand OpenRecentProjectCommand { get; set; }


        public string WindowTitle
        {
            get
            {
                var project = string.IsNullOrEmpty(ProjectPath) ? "New Untitled Project" : ProjectPath;
                var hasChanged = HasChanges ? "*" : string.Empty;
                var productName = applicationInformation.AssemblyProduct;
                return string.Format("{2} : {0}{1}",
                                     project,
                                     hasChanged,
                                     productName);
            }
        }


        public ScriptedInstructionViewModel ProjectInstructionsViewModel
        {
            get
            {
                if (projectInstructionsViewModel == null)
                    projectInstructionsViewModel = new ScriptedInstructionViewModel(this)
                                                   {
                                                       ScriptedInstruction = projectInstructions
                                                   };

                return projectInstructionsViewModel;
            }
            set
            {
                if (projectInstructionsViewModel == value)
                    return;

                projectInstructionsViewModel = value;
                SendPropertyChanged("ProjectInstructionsViewModel");
            }
        }


        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex == value)
                    return;

                selectedIndex = value;
                SendPropertyChanged("SelectedIndex");
            }
        }


        public ScriptedInstructionViewModel SelectedInstruction
        {
            get { return selectedInstruction; }
            set
            {
                if (selectedInstruction == value)
                    return;

                selectedInstruction = value;
                SendPropertyChanged("SelectedInstruction");
            }
        }

        public MainWindow View
        {
            get { return view; }
        }


        public ObservableCollection<ScriptedInstructionViewModel> Script
        {
            get { return script; }
            set
            {
                if (script == value)
                    return;

                script = value;
                SendPropertyChanged("Script");
            }
        }


        public int ProgressValue
        {
            get { return progressValue; }
            set
            {
                if (progressValue == value)
                    return;

                progressValue = value;
                SendPropertyChanged("ProgressValue");
            }
        }

        public IEnumerable<InstructionItem> Actions
        {
            get
            {
                if (actions == null)
                {
                    var types = from type in Assembly.GetExecutingAssembly().GetTypes()
                                where type.IsSubclassOf(typeof (ScriptedInstruction))
                                let attributes = type.GetCustomAttributes<ScriptedActionAttribute>(false).ToArray()
                                where attributes.Length > 0
                                let info = attributes[0]
                                select new InstructionItem(type, info.Name);

                    actions = types.ToList();
                }

                return actions;
            }
        }


        public InstructionItem SelectedAction
        {
            get { return selectAction; }
            set
            {
                if (selectAction == value)
                    return;

                selectAction = value;
                SendPropertyChanged("SelectedAction");
            }
        }


        public ImageSource Image
        {
            get { return image; }
            set
            {
                if (image == value)
                    return;

                image = value;
                SendPropertyChanged("Image");
            }
        }


        public string StatusText
        {
            get { return statusText; }
            set
            {
                if (statusText == value)
                    return;

                statusText = value;
                SendPropertyChanged("StatusText");
            }
        }


        public void OnLoaded()
        {
            if (loaded)
                return;
            loaded = true;
            versionChecker.Check();
        }

        public bool OnWindowClosing()
        {
            return !AskToDiscardChanges("Exit");
        }

        public IEnumerable<string> RecentFiles
        {
            get { return mruManager.RecentFiles; }
        }

        #endregion

        private void RefreshCommandBindings()
        {
            SendPropertyChanged("NewProjectCommand");
            SendPropertyChanged("SaveProjectCommand");
            SendPropertyChanged("OpenProjectCommand");
            SendPropertyChanged("AddActionCommand");
            SendPropertyChanged("InsertActionCommand");
            SendPropertyChanged("DeleteActionCommand");
            SendPropertyChanged("RenderPreviewCommand");
            SendPropertyChanged("RenderToDiskCommand");
            SendPropertyChanged("AboutCommand");
            SendPropertyChanged("ExitCommand");
        }

        private void CreateCommands()
        {
            NewProjectCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnNewProject);
            SaveProjectCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnSaveProject);
            OpenProjectCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnOpenProject);
            AddActionCommand = new DelegateCommand<InstructionItem>(OnAddAction);
            InsertActionCommand = new DelegateCommand<InstructionItem>(OnInsertAction);
            DeleteActionCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnDeleteAction);
            RenderPreviewCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnRenderPreview, CanExecute);
            RenderToDiskCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnRenderToDisk, CanExecute);
            AboutCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnAbout);
            ExitCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnExit);
            OpenRecentProjectCommand = new DelegateCommand<string>(OnOpenRecentProject);
            DocumentationCommand = new TemperedSoftware.Shared.Presentation.Commands.DelegateCommand(OnDocumentation);
        }

        private static void OnDocumentation()
        {
            var programFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var helpFile = Path.Combine(programFolder, "ScriptedTextAnimator.chm");
            Process.Start(helpFile);
        }

        private bool CanExecute()
        {
            return !animator.IsRendering;
        }

        private void OnNewProject()
        {
            if (!AskToDiscardChanges("New Project"))
                return;

            ResetPreview();
            var projectManager = new ProjectManager();
            var project = projectManager.New();
            UpdateProjectState(project, string.Empty);
        }

        private bool AskToDiscardChanges(string action)
        {
            if (!HasChanges)
                return true;

            var messageResult =
                MessageBox.Show(
                    @"You will lose all of your current changes.
Are you sure you wish to continue?",
                    action, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return messageResult == DialogResult.Yes;
        }

        private void UpdateProjectState(Project.Project project, string projectPath)
        {
            var viewModels = from item in project.ScriptedInstructions
                             select new ScriptedInstructionViewModel(this)
                                    {
                                        ScriptedInstruction = item
                                    };

            if (Script != null)
                Script.CollectionChanged -= ScriptCollectionChanged;

            Script = new ObservableCollection<ScriptedInstructionViewModel>(viewModels.ToList());
            Script.CollectionChanged += ScriptCollectionChanged;
            projectInstructions = project.ProjectInstructions;
            ProjectInstructionsViewModel = new ScriptedInstructionViewModel(this)
                                           {
                                               ScriptedInstruction = projectInstructions
                                           };
            outputFileName = project.OutputFileName;
            ProjectPath = projectPath;
            HasChanges = false;
        }

        private void ScriptCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasChanges = true;
        }
        
        const string defaultExtension = ".staprj";

        private void OnSaveProject()
        {
            var filter = CreateFilter(false);

            var dialog = new SaveFileDialog
                         {
                             AddExtension = true,
                             AutoUpgradeEnabled = true,
                             CheckPathExists = true,
                             Filter = filter,
                             DefaultExt = defaultExtension,
                             OverwritePrompt = true
                         };

            if (!string.IsNullOrEmpty(ProjectPath))
                dialog.FileName = ProjectPath;

            var dialogResult = dialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
                return;

            ProjectPath = dialog.FileName;

            var projectManager = new ProjectManager();
            var project = projectManager.New();
            project.OutputFileName = outputFileName;
            var scriptedInstructions = from item in Script
                                       select item.ScriptedInstruction;
            project.ScriptedInstructions = scriptedInstructions.ToList();
            project.ProjectInstructions = projectInstructions;
            projectManager.Save(project, dialog.FileName);
            ManageRecentFiles(dialog.FileName);
            HasChanges = false;
        }

        private static string CreateFilter(bool isLoad)
        {
            var filterBuilder = new FileDialogFilterBuilder();
            filterBuilder.Add("Project Files", string.Format("*{0}", defaultExtension));

            if (isLoad)
            {
                filterBuilder.Add("Legacy Project Files", "*.gifprj");
                filterBuilder.Add("XML Project Files", "*.xml");
                filterBuilder.AddAllFiles();
            }

            return filterBuilder.ToString();
        }

        private void ManageRecentFiles(string fileName)
        {
            mruManager.Update(fileName);
            SendPropertyChanged("RecentFiles");
        }

        private void OnOpenRecentProject(string fileName)
        {
            if (fileName.Equals(mruManager.NoneRecentFileToken))
                return;

            if(!File.Exists(fileName))
            {
                MessageBox.Show("Project file does not exist.", "Open Project", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            if (!AskToDiscardChanges("Open Project"))
                return;

            OpenProject(fileName);
        }

        private void OnOpenProject()
        {
            if (!AskToDiscardChanges("Open Project"))
                return;

            var filter = CreateFilter(true);
            var dialog = new OpenFileDialog
                         {
                             AddExtension = true,
                             AutoUpgradeEnabled = true,
                             CheckFileExists = true,
                             CheckPathExists = true,
                             Filter = filter,
                             DefaultExt = defaultExtension,
                             Multiselect = false
                         };

            if (!string.IsNullOrEmpty(ProjectPath))
                dialog.FileName = ProjectPath;

            var dialogResult = dialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
                return;

            var fileName = dialog.FileName;

            OpenProject(fileName);
        }

        private void OpenProject(string fileName)
        {
            var projectManager = new ProjectManager();

            Project.Project project;
            try
            {
                project = projectManager.Load(fileName);
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.ShowWindow(view, ex, "Failed to load project.");
                return;
            }

            UpdateProjectState(project, fileName);
            ManageRecentFiles(fileName);
        }

        private void OnAddAction(InstructionItem instructionItem)
        {
            var action = CreateAction(instructionItem);
            Script.Add(action);
            SelectedIndex = Script.Count - 1;
        }

        private ScriptedInstructionViewModel CreateAction(InstructionItem instructionItem)
        {
            var helper = new InstructionFactory(instructionItem.Type);

            var instruction = helper.Create();

            var viewModel = new ScriptedInstructionViewModel(this)
                            {
                                ScriptedInstruction = instruction
                            };

            return viewModel;
        }

        private void OnInsertAction(InstructionItem instructionItem)
        {
            if (SelectedIndex < 0)
                return;

            var action = CreateAction(instructionItem);
            Script.Insert(SelectedIndex, action);
            SelectedIndex = SelectedIndex - 1;
        }

        private void OnDeleteAction()
        {
            if (SelectedIndex < 0)
                return;

            var currentIndex = SelectedIndex;

            Script.RemoveAt(SelectedIndex);
            SelectedIndex = currentIndex - 1;
        }

        private void OnAbout()
        {
            var window = new AboutWindow
                         {
                             Owner = view, 
                             WindowStartupLocation = WindowStartupLocation.CenterOwner
                         };
            window.Show();
        }

        private static void OnExit()
        {
            Application.Current.MainWindow.Close();
        }

        private void OnRenderPreview()
        {
            ResetPreview();
            animator.TaskFinished += OnPreviewRenderFinished;
            RenderImpl();
        }

        private void OnRenderToDisk()
        {
            var filterBuilder = new FileDialogFilterBuilder();
            filterBuilder.Add("GIF Image", "*.gif");

            var saveDialog = new SaveFileDialog
                             {
                                 AddExtension = true,
                                 AutoUpgradeEnabled = true,
                                 CheckPathExists = true,
                                 DefaultExt = ".gif",
                                 Filter = filterBuilder.ToString(),
                                 OverwritePrompt = true
                             };

            if (!string.IsNullOrEmpty(outputFileName))
                saveDialog.FileName = outputFileName;

            var result = saveDialog.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            outputFileName = saveDialog.FileName;
            animator.TaskFinished += OnRenderToDiskFinished;
            RenderImpl();
        }

        private void OnPreviewRenderFinished(object sender, RenderFinishedArgs e)
        {
            DisplayPreview(e);
            animator.TaskFinished -= OnPreviewRenderFinished;
        }

        private void DisplayPreview(RenderFinishedArgs e)
        {
            if (e.FinishState == RenderFinishState.Success && e.Image != null)
            {
                var ms = new MemoryStream(e.Image, false);
                var bitmap = (Bitmap) System.Drawing.Image.FromStream(ms);
                view.Preview.Child = new PictureBox
                                     {
                                         Image = bitmap
                                     };
            }
            else
            {
                ExceptionHandler.ShowWindow(view, e.Exception);
                ResetPreview();
            }
            RefreshCommandBindings();
        }


        private void ResetPreview()
        {
            view.Preview.Child = null;
        }


        private void OnProgressUpdate(object sender, BackgroundTaskProgressArgs e)
        {
            ProgressValue = e.PercentageComplete;
            StatusText = e.CurrentStep;
        }

        private void RenderImpl()
        {
            if (!ValidateRenderArgs())
                return;

            RefreshCommandBindings();
            var instructions = from item in Script
                               select item.ScriptedInstruction;

            animator.StartRender(new RenderArgs(instructions.ToArray(), projectInstructions));
        }

        private bool ValidateRenderArgs()
        {
            var result = true;

            foreach (var instructionViewModel in Script)
            {
                instructionViewModel.Validate();
                result = instructionViewModel.IsValid && result;
            }

            StatusText = string.Empty;

            if (!result)
                StatusText = "Cannot render. There are errors in the script";

            result = Script.Count != 0 && result;
            if (Script.Count == 0)
                StatusText = "Script instructions are required";

            ProjectInstructionsViewModel.Validate();
            result = ProjectInstructionsViewModel.IsValid && result;

            if (!result && string.IsNullOrEmpty(StatusText))
                StatusText = "Project instructions are invalid";

            return result;
        }

        private void OnRenderToDiskFinished(object sender, RenderFinishedArgs e)
        {
            animator.TaskFinished -= OnRenderToDiskFinished;
            using (var writer = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                writer.Write(e.Image, 0, e.Image.Length);
                writer.Close();
            }
            DisplayPreview(e);
        }
    }
}