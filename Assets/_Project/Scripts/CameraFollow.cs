using UnityEngine;

namespace Project.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target Tracking")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -10f);

        [Header("Smoothing")]
        [SerializeField] private float smoothTime = 0.25f;

        private Vector3 _currentVelocity = Vector3.zero;

        private void LateUpdate()
        {
            if (target == null) return;

            // ¬ычисление целевой точки в мировом пространстве
            Vector3 targetPosition = target.position + offset;

            // ѕлавное перемещение камеры без рывков
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref _currentVelocity,
                smoothTime
            );
        }
    }
}