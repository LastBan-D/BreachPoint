using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Здоровье")]
    [SerializeField] private int maxHealth = 100;

    [Header("Движение")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Бой")]
    [SerializeField] private int contactDamage = 10;   // урон при касании игрока (пригодится позже)
}