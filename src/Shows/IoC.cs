using SimpleInjector;

namespace Shows
{
    internal static class IoC
    {
        public static Container Container { get; internal set; }

        public static T GetInstance<T>()
            where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}
