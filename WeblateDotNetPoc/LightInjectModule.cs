using LightInject;

namespace WeblateDotNetPoc
{
    public class LightInjectModule : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            // Event Detector
            serviceRegistry.Register<ITranslationMediator, TranslationMediator>();
        }
    }
}