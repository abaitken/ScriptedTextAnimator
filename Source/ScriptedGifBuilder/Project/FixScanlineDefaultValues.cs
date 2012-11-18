namespace ScriptedGifBuilder.Project
{
    internal class FixScanlineDefaultValues : ProjectUpgradeTaskBase
    {
        public FixScanlineDefaultValues(string version) 
            : base(version)
        {
        }

        public override Project Upgrade(Project project)
        {
            if (project.ProjectInstructions.ScanlineWidth == 0)
                project.ProjectInstructions.ScanlineWidth = 1;

            if (project.ProjectInstructions.ScanlineGap == 0)
                project.ProjectInstructions.ScanlineGap = 1;

            return project;
        }
    }
}