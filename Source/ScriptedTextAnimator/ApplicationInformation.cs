using System.IO;
using System.Reflection;

namespace ScriptedTextAnimator
{
    internal class ApplicationInformation
    {
        private readonly Assembly _assembly;

        public ApplicationInformation() 
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public string AssemblyVersion
        {
            get { return _assembly.GetName().Version.ToString(); }
        }

        public string FileVersion
        {
            get { return _assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version; }
        }

        public string AssemblyConfiguration
        {
            get { return _assembly.GetCustomAttribute<AssemblyConfigurationAttribute>().Configuration; }
        }

        public string AssemblyDescription
        {
            get { return _assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description; }
        }

        public string AssemblyProduct
        {
            get { return _assembly.GetCustomAttribute<AssemblyProductAttribute>().Product; }
        }

        public string AssemblyCopyright
        {
            get { return _assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright; }
        }

        public string AssemblyCompany
        {
            get { return _assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company; }
        }

        public string ProgramFolder => Path.GetDirectoryName(_assembly.Location);
    }
}