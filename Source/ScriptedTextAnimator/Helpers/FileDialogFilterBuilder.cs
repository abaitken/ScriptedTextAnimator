using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptedTextAnimator
{
    internal class FileDialogFilterBuilder
    {
        private readonly StringBuilder _resultBuilder;

        public FileDialogFilterBuilder()
        {
            _resultBuilder = new StringBuilder();
        }

        public void Add(string name, params string[] extensionPatterns)
        {
            if (_resultBuilder.Length != 0)
                _resultBuilder.Append("|");

            var extensionPattern = new StringBuilder();

            foreach (var pattern in extensionPatterns)
            {
                if (extensionPattern.Length != 0)
                    extensionPattern.Append(";");
                extensionPattern.Append(pattern);
            }

            _resultBuilder.AppendFormat("{0} ({1})|{1}", name, extensionPattern);
        }

        public void AddAllFiles()
        {
            Add("All files", "*.*");
        }

        public override string ToString()
        {
            return _resultBuilder.ToString();
        }
    }
}
