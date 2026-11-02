using UnityEngine;

namespace Project.Scripts
{
    public class GameTicker : MonoBehaviour
    {
        private void Awake() => Debug.Log("<color=cyan>[Lifecycle] Awake</color>");
        private void OnEnable() => Debug.Log("<color=green>[Lifecycle] OnEnable</color>");
        private void Start() => Debug.Log("<color=yellow>[Lifecycle] Start</color>");

        private void FixedUpdate()
        {
            // Срабатывает с постоянной частотой физического тика
        }

        private void Update()
        {
            // Срабатывает каждый графический кадр
        }

        private void LateUpdate()
        {
            // Срабатывает после всех Update
        }

        private void OnDisable() => Debug.Log("<color=orange>[Lifecycle] OnDisable</color>");
        private void OnDestroy() => Debug.Log("<color=red>[Lifecycle] OnDestroy</color>");
    }
}