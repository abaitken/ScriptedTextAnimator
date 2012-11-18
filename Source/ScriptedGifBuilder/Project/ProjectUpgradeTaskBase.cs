using System;

namespace ScriptedGifBuilder.Project
{
    internal abstract class ProjectUpgradeTaskBase : IProjectUpgradeTask
    {
        private readonly Version version;

        protected ProjectUpgradeTaskBase(string version)
        {
            this.version = new Version(version);
        }

        public Version SchemaVersion { get { return version; } }

        public abstract Project Upgrade(Project project);
    }
}