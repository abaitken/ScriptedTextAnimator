using System.Reflection;
using TemperedSoftware.Shared.Services;

namespace ScriptedGifBuilder
{
    internal class ApplicationInformation : ApplicationInformationBase
    {
        public ApplicationInformation() 
            : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}