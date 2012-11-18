using System.Reflection;
using TemperedSoftware.Shared.Services;

namespace ScriptedTextAnimator
{
    internal class ApplicationInformation : ApplicationInformationBase
    {
        public ApplicationInformation() 
            : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}