using UnityEngine;

namespace Project.Scripts
{
    public class PlayerMovementPrototype : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Базовая скорость перемещения персонажа по арене")]
        [Range(1.0f, 20.0f)]
        [SerializeField] private float moveSpeed = 8.0f;

        // Вектор кэширования ввода между кадрами Update и тиками FixedUpdate
        private Vector3 _rawInputDirection;

        private void Update()
        {
            // 1. Опрос пользовательского ввода выполняется строго в графическом кадре
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            _rawInputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        }

        private void FixedUpdate()
        {
            // 2. Применение перемещения выполняется в фиксированном физическом цикле
            if (_rawInputDirection.sqrMagnitude > 0.001f)
            {
                // Для FixedUpdate вместо Time.deltaTime используется Time.fixedDeltaTime
                transform.position += _rawInputDirection * (moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}