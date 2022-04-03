namespace Code.Infrastructure.Services.Pause
{
    public interface IPauseService : IService
    {
        void Pause();
        void Unpause();
    }
}