using System.Collections.Generic;
using System.Collections.Specialized;
using ScriptedTextAnimator.Properties;

namespace ScriptedTextAnimator
{
    internal class MRUManager
    {
        public string NoneRecentFileToken
        {
            get { return "(None)"; }
        }

        public IEnumerable<string> RecentFiles
        {
            get
            {
                if (Settings.Default.RecentFiles == null)
                    Settings.Default.RecentFiles = new StringCollection();

                if (Settings.Default.RecentFiles.Count == 0)
                    yield return NoneRecentFileToken;

                foreach (var recentFile in Settings.Default.RecentFiles)
                {
                    if (recentFile.Equals(NoneRecentFileToken))
                        continue;

                    yield return recentFile;
                }
            }
        }

        public void Update(string filename)
        {
            Settings.Default.RecentFiles.Remove(filename);
            Settings.Default.RecentFiles.Insert(0, filename);
            if (Settings.Default.RecentFiles.Count > 10)
            {
                var otherItems = new List<string>();

                for (var i = 10; i < Settings.Default.RecentFiles.Count; i++)
                {
                    otherItems.Add(Settings.Default.RecentFiles[i]);
                }

                foreach (var otherItem in otherItems)
                    Settings.Default.RecentFiles.Remove(otherItem);
            }
        }
    }
}