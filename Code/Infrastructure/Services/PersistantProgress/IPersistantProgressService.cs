using Code.Data;

namespace Code.Infrastructure.Services.PersistantProgress
{
    public interface IPersistantProgressService : IService
    {
        PlayerProgress Progress { get; set; }
        void ClearProgress();
    }
}