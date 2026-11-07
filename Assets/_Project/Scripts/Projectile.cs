using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Движение")]
    [SerializeField] private float speed = 8f;      // скорость, юнитов в секунду

    [Header("Жизнь")]
    [SerializeField] private float lifetime = 2f;   // сколько секунд живёт, если ни во что не врезалась

    private void Start()
    {
        // Берём с этого же объекта компонент Rigidbody и задаём скорость.
        // linearVelocity = куда и с какой скоростью физика везёт объект каждый тик.
        // transform.forward — "СИНЯЯ стрелка" объекта (ось Z): пуля летит туда, куда смотрит.
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        // Страховка: Destroy(gameObject, N) уничтожит объект через N секунд,
        // даже если пуля улетела в вечность и ни во что не врезалась.
        Destroy(gameObject, lifetime);
    }

    // Вызывается, когда наш триггер входит в чужой коллайдер (стена, враг).
    // В 3D параметр типа Collider, а метод без "2D" на конце — частая ошибка новичка!
    private void OnTriggerEnter(Collider other)
    {
        // Пока просто уничтожаем. День 12 перепишет это на пул объектов — так задумано.
        Destroy(gameObject);
    }
}