using UnityEngine;

namespace Project.Scripts
{
    public class PlayerMovementPrototype : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 7.0f;

        private void Update()
        {
            // Считывание базового ввода по осям (горизонталь и вертикаль)
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Формирование вектора направления в плоскости XZ
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            // Перемещение объекта через Transform с компенсацией FPS
            if (movementDirection.sqrMagnitude > 0.001f)
            {
                transform.position += movementDirection * (moveSpeed * Time.deltaTime);
            }
        }
    }
}