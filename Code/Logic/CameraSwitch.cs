using Cinemachine;
using UnityEngine;

namespace Code.Logic
{
    public class CameraSwitch : MonoBehaviour
    {
        private CinemachineVirtualCamera _cinemachinePlayer, _cinemachineHand;

        public void Construct(CinemachineVirtualCamera playerCamera, CinemachineVirtualCamera handCamera)
        {
            _cinemachinePlayer = playerCamera;
            _cinemachineHand = handCamera;
        }
        public void SwitchTarget()
        {
            (_cinemachinePlayer.Priority, _cinemachineHand.Priority) =
                (_cinemachineHand.Priority, _cinemachinePlayer.Priority);
        }
    }
}
