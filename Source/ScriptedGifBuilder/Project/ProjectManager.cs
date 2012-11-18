using System;
using System.Collections.Generic;
using ScriptedGifBuilder.Instructions;
using TemperedSoftware.Shared.Serialization;

namespace ScriptedGifBuilder.Project
{
    internal class ProjectManager
    {
        private readonly Version schemaVersion;

        public ProjectManager()
        {
            schemaVersion = new Version("1.2.0.0");
        }

        private static Version GetProgramVersion()
        {
            var appInfo = new ApplicationInformation();

            return new Version(appInfo.FileVersion);
        }

        public Project New()
        {
            return new Project
                   {
                       ProgramVersion = GetProgramVersion().ToString(),
                       SchemaVersion = schemaVersion.ToString(),
                       OutputFileName = string.Empty,
                       ProjectInstructions = CreateProjectInstructions(),
                       ScriptedInstructions = new List<ScriptedInstruction>()
                   };
        }

        private static ProjectInstructions CreateProjectInstructions()
        {
            var helper = new InstructionFactory(typeof(ProjectInstructions));

            var instruction = helper.Create();

            return (ProjectInstructions)instruction;
        }

        public void Save(Project project, string filePath)
        {
            project.SchemaVersion = schemaVersion.ToString();
            project.ProgramVersion = GetProgramVersion().ToString();

            XmlSerializationHelper.SaveToFile(project, filePath);
        }

        public Project Load(string filePath)
        {
            var basicProject = XmlSerializationHelper.LoadFromFile<BasicProject>(filePath);

            var projectSchemaVersion = string.IsNullOrEmpty(basicProject.SchemaVersion) ? new Version("0.0.0.0") : new Version(basicProject.SchemaVersion);
            if(projectSchemaVersion > schemaVersion)
                throw new InvalidOperationException("This project has a newer schema version than is currently supported.");

            var project = XmlSerializationHelper.LoadFromFile<Project>(filePath);

            if (projectSchemaVersion < schemaVersion)
            {
                var upgradeTasks = CreateUpgradeTasks();

                foreach (var projectUpgradeTask in upgradeTasks)
                {
                    if (projectUpgradeTask.SchemaVersion < projectSchemaVersion)
                        continue;

                    project = projectUpgradeTask.Upgrade(project);
                }

                project.SchemaVersion = schemaVersion.ToString();
                project.ProgramVersion = GetProgramVersion().ToString();
            }

            return project;
        }

        private static IEnumerable<IProjectUpgradeTask> CreateUpgradeTasks()
        {
            yield return new FixScanlineDefaultValues("1.1.0.0");
        }
    }
}