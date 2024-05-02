using CodeBase.Data.Static;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        public void Load();
        public GameBalance GameBalance();
        public LevelStaticData LevelStaticData();
        public BoostsStaticData BoostsStaticData();
        public WindowConfig ForWindow(WindowId windowId);
    }
}