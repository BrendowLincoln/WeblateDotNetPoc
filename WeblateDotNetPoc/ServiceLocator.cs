using LightInject;

namespace WeblateDotNetPoc
{
    public static class ServiceLocator
    {
        public static ServiceContainer Container { get; private set; }

        public static void SetContainer(ServiceContainer container)
        {
            Container = container;
        }

        public static T Get<T>() => Container.GetInstance<T>();
    }
}