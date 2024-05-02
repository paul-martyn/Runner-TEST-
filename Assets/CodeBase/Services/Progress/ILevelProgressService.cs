using CodeBase.Data;

namespace CodeBase.Services.Progress
{
    public interface ILevelProgressService : IService
    {
        public LevelProgress Progress { get; set; }
        public void CreateNewProgress();
    }
}
