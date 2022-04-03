using Code.Logic;

namespace Code.Hand
{
    public class MiniGame
    {
        private CameraSwitch _cameraSwitch;
        private HandMovement _handMovement;
        private Timer _timer;

        public MiniGame(CameraSwitch cameraSwitch, HandMovement handMovement, Timer timer)
        {
            _cameraSwitch = cameraSwitch;
            _handMovement = handMovement;
            _timer = timer;
            
            Subscribe();
        }

        private void Subscribe()
        {
            _timer.OnTimerEnd += () =>
            {
                _handMovement.StartGrabbing();
                _cameraSwitch.SwitchTarget();
            };
            _handMovement.Return +=() =>
            {
                _cameraSwitch.SwitchTarget();
                Start();
            };
        }

        public void Start() 
            => _timer.StartCount();
    }
}