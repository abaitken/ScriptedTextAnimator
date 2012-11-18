using System;

namespace ScriptedGifBuilder.Project
{
    internal interface IProjectUpgradeTask
    {
        Version SchemaVersion { get; }
        Project Upgrade(Project project);
    }
}