using UnityEngine;

namespace Project.Scripts
{
    public class GameTicker : MonoBehaviour
    {
        private void Awake() => Debug.Log("<color=cyan>[Lifecycle] 1. Awake: Инициализация объекта</color>");
        private void OnEnable() => Debug.Log("<color=green>[Lifecycle] 2. OnEnable: Подписка на события</color>");
        private void Start() => Debug.Log("<color=yellow>[Lifecycle] 3. Start: Старт логики перед первым кадром</color>");
        private void OnDisable() => Debug.Log("<color=orange>[Lifecycle] OnDisable: Отписка от событий</color>");
        private void OnDestroy() => Debug.Log("<color=red>[Lifecycle] OnDestroy: Уничтожение объекта и освобождение памяти</color>");
    }
}