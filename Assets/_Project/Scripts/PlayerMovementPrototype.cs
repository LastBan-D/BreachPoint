using UnityEngine;

namespace Project.Scripts
{
    public class PlayerMovementPrototype : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Ѕазова€ скорость перемещени€ персонажа по арене")]
        [Range(1.0f, 20.0f)]
        [SerializeField] private float moveSpeed = 8.0f;

        private Vector3 _rawInputDirection;

        private void Update()
        {
            // —бор пользовательского ввода в логическом кадре
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            _rawInputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        }

        private void FixedUpdate()
        {
            // ѕрименение перемещени€ с фиксированным шагом физики
            if (_rawInputDirection.sqrMagnitude > 0.001f)
            {
                transform.position += _rawInputDirection * (moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}