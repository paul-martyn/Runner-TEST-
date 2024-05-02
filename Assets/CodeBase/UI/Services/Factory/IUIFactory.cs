using CodeBase.Services;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        public void CreateUIRoot();
        public void CreateMainMenu();
        public void CreateFailScreen();
        public void CreateCompleteScreen();
    }
}