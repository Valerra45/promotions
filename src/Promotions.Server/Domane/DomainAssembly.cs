using System.Reflection;

namespace Domane
{
    public static class DomainAssembly
    {
        public static readonly Assembly Instance = typeof(DomainAssembly).Assembly;
    }

}
