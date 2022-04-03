using Code.Infrastructure.Factory;

namespace Code.Infrastructure.Services.Pause
{
    public class PauseService : IPauseService
    {
        private readonly IGameFactory _gameFactory;

        public PauseService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Pause()
        {
            foreach (IPauseHandler pauseHandler in _gameFactory.PauseHandlers)
            {
                pauseHandler.Pause();
            }
        }

        public void Unpause()
        {
            foreach (IPauseHandler pauseHandler in _gameFactory.PauseHandlers)
            {
                pauseHandler.Unpause();
            }
        }
    }

    public interface IPauseHandler
    {
        void Unpause();
        void Pause();
    }
}