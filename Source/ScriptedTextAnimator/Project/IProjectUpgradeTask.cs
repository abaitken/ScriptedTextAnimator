using System;

namespace ScriptedTextAnimator.Project
{
    internal interface IProjectUpgradeTask
    {
        Version SchemaVersion { get; }
        Project Upgrade(Project project);
    }
}