using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerPhysicsController : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        [Header("Movement Dynamics")]
        [SerializeField] private float walkSpeed = 7.0f;
        [SerializeField] private float sprintSpeed = 11.0f;

        private Rigidbody _rigidbody;
        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;
        private bool _isSprinting;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }

        private void OnEnable() => _inputActions.Player.Enable();
        private void OnDisable() => _inputActions.Player.Disable();

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
            if (context.performed)
            {
                Debug.Log("<color=yellow>[Physics Player] Fire action triggered.</color>");
            }
        }

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            float speed = _isSprinting ? sprintSpeed : walkSpeed;
            Vector3 targetDirection = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;

            // Расчет целевой горизонтальной скорости с сохранением текущей вертикальной (гравитация)
            Vector3 targetVelocity = targetDirection * speed;
            
            // В Unity 2023+ / 6 используется linearVelocity, в более ранних версиях — velocity
            Vector3 currentVel = _rigidbody.linearVelocity;
            _rigidbody.linearVelocity = new Vector3(targetVelocity.x, currentVel.y, targetVelocity.z);
        }

        // Физическое столкновение с твердым препятствием
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"<color=red>[Collision]</color> Contact with: {collision.gameObject.name} on layer {LayerMask.LayerToName(collision.gameObject.layer)}");
        }

        // Вход в триггерную зону
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"<color=green>[Trigger]</color> Entered zone: {other.gameObject.name}");
        }
    }
}