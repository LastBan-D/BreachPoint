using UnityEngine;
using UnityEngine.InputSystem;   // здесь живЄт тип InputActionReference

public class PlayerShootingController : MonoBehaviour
{
    [Header("—сылки")]
    [SerializeField] private GameObject projectilePrefab;     // ј——≈“ префаба пули (из окна Project!)
    [SerializeField] private Transform firePoint;             // точка выстрела
    [SerializeField] private InputActionReference fireAction; // действие Fire из ассета .inputactions

    [Header("Ќастройки")]
    [SerializeField] private float fireCooldown = 0.25f;      // пауза между выстрелами, секунд

    private float nextFireTime;   // игровое врем€, когда снова можно стрел€ть

    private void OnEnable()
    {
        if (fireAction != null) fireAction.action.Enable();   // гарантируем, что действие включено
    }

    private void Update()
    {
        // WasPressedThisFrame = true только в кадр Ќј∆ј“»я (удержание не считаетс€).
        if (fireAction != null && fireAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Time.time Ч секунды с начала запуска игры. ѕерезар€дка не прошла Ч уходим.
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireCooldown;

        // Instantiate(чертЄж, позици€, поворот) Ч создаЄт  ќѕ»ё префаба в сцене:
        // рождаетс€ в точке FirePoint и летит туда, куда тот повЄрнут.
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}