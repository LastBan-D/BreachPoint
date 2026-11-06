using UnityEngine;

namespace Project.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target Tracking")]
        [Tooltip("Объект слежения (персонаж)")]
        [SerializeField] private Transform target;

        [Tooltip("Смещение камеры относительно объекта")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -10f);

        [Header("Smoothing")]
        [Tooltip("Время сглаживания перемещения камеры")]
        [Range(0.01f, 1.0f)]
        [SerializeField] private float smoothTime = 0.25f;

        private Vector3 _currentVelocity = Vector3.zero;

        private void LateUpdate()
        {
            if (target == null) return;

            // Вычисление целевой точки с учетом смещения
            Vector3 targetPosition = target.position + offset;

            // Плавное следование камеры без микрофризов
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref _currentVelocity,
                smoothTime
            );
        }
    }
}