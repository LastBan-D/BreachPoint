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
            // Инициализация объекта сгенерированного класса ввода
            _inputActions = new PlayerInputActions();
            // Подключение текущего класса в качестве слушателя интерфейса
            _inputActions.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            // Активация карты ввода персонажа
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            // Обязательная деактивация для исключения утечек памяти
            _inputActions.Player.Disable();
        }

        // Колбэк New Input System при изменении вектора движения
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        // Колбэк New Input System при нажатии/отпускании кнопки спринта
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed) _isSprinting = true;
            else if (context.canceled) _isSprinting = false;
        }

        // Колбэк New Input System при вызове действия огня
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("<color=orange>[Action] Fire triggered!</color>");
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