using CodeBase.Data;

namespace CodeBase.Services.Progress
{
    public class LevelProgressService : ILevelProgressService
    {
        public LevelProgress Progress { get; set; }

        public void CreateNewProgress() => 
            Progress = new LevelProgress();
    }
}