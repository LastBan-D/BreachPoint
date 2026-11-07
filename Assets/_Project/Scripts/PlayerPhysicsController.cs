using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerPhysicsController : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        [Header("Movement Dynamics")]
        [Tooltip("Базовая скорость ходьбы персонажа")]
        [Range(2.0f, 15.0f)]
        [SerializeField] private float walkSpeed = 7.0f;

        [Tooltip("Скорость персонажа при спринте")]
        [Range(5.0f, 25.0f)]
        [SerializeField] private float sprintSpeed = 11.0f;

        private Rigidbody _rigidbody;
        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;
        private bool _isSprinting;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            // Фиксируем вращение по X и Z, чтобы капсула не заваливалась при столкновениях
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }

        private void OnEnable() => _inputActions.Player.Enable();

        private void OnDisable() => _inputActions.Player.Disable();

        private void OnDestroy()
        {
            // Обязательная очистка неуправляемых ресурсов подсистемы ввода
            _inputActions?.Dispose();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed) _isSprinting = true;
            else if (context.canceled) _isSprinting = false;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
        }

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            float speed = _isSprinting ? sprintSpeed : walkSpeed;
            Vector3 targetDirection = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;

            // Расчет горизонтальной скорости с сохранением текущей вертикальной (гравитации)
            Vector3 targetVelocity = targetDirection * speed;

            // Поддержка Unity 6 (linearVelocity) и предыдущих версий Unity (velocity)
#if UNITY_6000_0_OR_NEWER
            Vector3 currentVel = _rigidbody.linearVelocity;
            _rigidbody.linearVelocity = new Vector3(targetVelocity.x, currentVel.y, targetVelocity.z);
#else
            Vector3 currentVel = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(targetVelocity.x, currentVel.y, targetVelocity.z);
#endif
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"<color=red>[Collision]</color> Столкновение с: {collision.gameObject.name} на слое {LayerMask.LayerToName(collision.gameObject.layer)}");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"<color=green>[Trigger]</color> Вход в триггерную зону: {other.gameObject.name}");
        }
    }
}