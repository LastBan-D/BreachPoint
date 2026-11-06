using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        [Header("Movement Configuration")]
        [SerializeField] private float walkSpeed = 6.0f;
        [SerializeField] private float sprintSpeed = 10.0f;

        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;
        private bool _isSprinting;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }

        private void OnEnable() => _inputActions.Player.Enable();

        private void OnDisable() => _inputActions.Player.Disable();

        private void OnDestroy()
        {
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
            if (context.performed)
            {
                Debug.Log("<color=orange>[Action] Выстрел произведен!</color>");
            }
        }

        private void FixedUpdate()
        {
            if (_moveInput.sqrMagnitude > 0.001f)
            {
                float currentSpeed = _isSprinting ? sprintSpeed : walkSpeed;
                Vector3 direction = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;
                transform.position += direction * (currentSpeed * Time.fixedDeltaTime);
            }
        }
    }
}