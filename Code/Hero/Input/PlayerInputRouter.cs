using UnityEngine;

namespace Code.Hero.Input
{
    public class PlayerInputRouter
    {
        private PlayerInput _playerInput;
        private HeroMovement _heroMovement;
        public PlayerInputRouter(HeroMovement heroMovement)
        {
            _playerInput = new PlayerInput();
            _heroMovement = heroMovement;
        }
        public void OnEnable()
        {
            _playerInput.Enable();

            _playerInput.Player.Jump.performed += _heroMovement.TryJump;
            _playerInput.Player.Swing.started += _heroMovement.TrySwing;
            _playerInput.Player.Swing.canceled += _heroMovement.StopSwing;
        }
        public void OnDisable()
        {
            _playerInput.Player.Jump.performed -= _heroMovement.TryJump;
            _playerInput.Player.Swing.started -= _heroMovement.TrySwing;
            _playerInput.Player.Swing.canceled -= _heroMovement.StopSwing;

            _playerInput.Disable();
        }

        public Vector3 GetInputDirection()
        {
            Vector3 direction = _playerInput.Player.Move.ReadValue<Vector2>();
            return direction.Where(y: 0, z: direction.y);
        }
    }
}
