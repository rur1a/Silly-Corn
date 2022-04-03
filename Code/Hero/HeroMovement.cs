using System.Collections;
using Code.Character;
using Code.Data;
using Code.Hero.Input;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Pause;
using Code.Infrastructure.Services.PersistantProgress;
using Code.Logic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Code.Hero
{
    [RequireComponent(
        typeof(Rigidbody),
        typeof(CapsuleCollider))]
    public class HeroMovement : MonoBehaviour, ITakeable, ISavedProgress, IPauseHandler
    {
        
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private float _speed, _maxGroundAcceleration, _jumpHeight,_jumpLength;
        [SerializeField] private AudioSource _jump;

        private Vector3 _gravity = new Vector3(0, -50, 0), _groundNormal, _climbingNormal;
        private CapsuleCollider _collider;
        private bool _climbing;
        private LayerMask _layerMask;
        
        private IGroundCheckerService _groundCheckerService;
        private Rope _rope;
        private PlayerInputRouter _inputRouter;
        
        public void Construct(IGroundCheckerService groundCheckerService)
        {
            _groundCheckerService = groundCheckerService;
        }
        
        public bool Grounded => _groundCheckerService.Grounded(transform, _collider.height, out _groundNormal);
        public Rigidbody Rigidbody { get; private set; }
        
        private void Awake()
        {
            _inputRouter = new PlayerInputRouter(this);
            _collider = GetComponent<CapsuleCollider>();
            Rigidbody = GetComponent<Rigidbody>();
            _layerMask = 1 << LayerMask.NameToLayer("Rope");
        }

        private void OnEnable() => 
            _inputRouter.OnEnable();

        private void OnDisable() => 
            _inputRouter.OnDisable();

        private void FixedUpdate()
        {
            Vector3 moveDirection = _inputRouter.GetInputDirection();
            Move(moveDirection);
        }

        private void Move(Vector3 direction)
        {
            if (_rope != null)
                _rope.Move(direction);
            else if (_climbing)
                ClimbMove(direction);
            else
                Accelerate(direction);
        }

        private void Accelerate(Vector3 direction)
        {
            Vector3 finalVelocity = direction * _speed;
            Vector3 acceleration = (finalVelocity - Rigidbody.velocity) / Time.fixedDeltaTime;
            float maxAccelerate = Grounded || _climbing ? _maxGroundAcceleration : float.MaxValue;
            acceleration = Vector3.ClampMagnitude(acceleration, maxAccelerate);
            Rigidbody.velocity += acceleration.Where(y: 0) * Time.fixedDeltaTime;
            if (Grounded == false && _climbing == false) Rigidbody.velocity += _gravity * Time.fixedDeltaTime;
            _animator.Move(Rigidbody.velocity.magnitude);
            Rotate(direction);
        }

        private void ClimbMove(Vector3 direction)
        {
            direction.y = direction.z;
            direction.z = 0;
            direction = Vector3.ProjectOnPlane(direction, _climbingNormal);
            Rigidbody.velocity = direction * _speed;
            _animator.Climb(Rigidbody.velocity.magnitude);
        }

        private void Rotate(Vector3 direction)
        {
            if(direction==Vector3.zero) return;
            Rigidbody.rotation = Quaternion.Slerp (Rigidbody.rotation, Quaternion.LookRotation (direction), 0.1f);
            if (Vector3.Cross(transform.right, _groundNormal) == Vector3.zero) return;
            Rigidbody.rotation = Quaternion.Slerp (Rigidbody.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, _groundNormal)), 0.1f);
        }

        public void TryJump(InputAction.CallbackContext context)
        {
            if (!Grounded) 
                return;
            StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            _jump.Play();
            //_rigidbody.velocity = _rigidbody.velocity.Where(y: _rigidbody.velocity.y + Mathf.Sqrt(-2f * _gravity.y * _jumpHeight));
            float time = _jumpLength / _speed;
            float halfJumpLength = _jumpLength / 2;
            _gravity.y = -2 * _jumpHeight * _speed * _speed / (halfJumpLength*halfJumpLength);
            Rigidbody.velocity += Rigidbody.velocity.Where(y: Rigidbody.velocity.y + 2 * _jumpHeight * _speed / halfJumpLength);
            yield return new WaitForSeconds(time);
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (ContactPoint contact in other.contacts)
            {
                float angle = Vector3.Angle(transform.up, contact.normal);
                if (angle > 60f && other.gameObject.layer == 3)
                {
                    _animator.Move(0);
                    _climbing = true;
                    _climbingNormal = contact.normal;
                    return;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            _climbing = false;
            _animator.Climb(0);
        }

        public void TrySwing(InputAction.CallbackContext context)
        {
            var results = new Collider[1];
            int check = Physics.OverlapSphereNonAlloc(transform.position, 2, results, _layerMask);
            
            if (check > 0)
            {
                results[0].TryGetComponent(out _rope);
                _rope.AddRigidbody(Rigidbody);
                _collider.enabled = false;
                _animator.Move(0);
            }

        }

        public void StopSwing(InputAction.CallbackContext context)
        {
            if (_rope == null) return;
            _collider.enabled = true;
            Rigidbody.velocity = _rope.Rigidbody.velocity;
            _rope.Reset();
            _rope = null;
        }

        public void StartClimb(Vector3 to)
        {
            StartCoroutine(Climb(to));
        }

        private IEnumerator Climb(Vector3 to)
        {
            _animator.ClimbOnShelf();
            Vector3 startPosition = Rigidbody.position;
            for (float i = 0; i < 1.5f; i += Time.deltaTime / 1.5f)
            {
                Rigidbody.position = Vector3.Lerp(startPosition, to, i);
                yield return null;
            }
        }
        public void StopMovement()
        {
            _collider.enabled = false;
            enabled = false;
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevelName(),
                transform.position.AsVector3Data());

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevelName() == progress.WorldData.PositionOnLevel.LevelName)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if(savedPosition!=null)
                    Move(to: savedPosition);
            }
        }

        public void Move(Vector3Data to)
        {
            Rigidbody.detectCollisions = false;
            transform.position = to.AsVector3().AddY(_collider.height);
            Rigidbody.detectCollisions = true;
        }

        private string CurrentLevelName()
        {
            return SceneManager.GetActiveScene().name;
        }

        public void Pause() => 
            Rigidbody.isKinematic = true;

        public void Unpause() => 
            Rigidbody.isKinematic = false;
    }
}
