using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // Скорость снаряда
    private Vector3 targetPosition; // Позиция цели
    private float damage; // Урон снаряда

    // Изменяем тип target с Transform на Vector3
    public void Initialize(Vector3 targetPosition, float damage)
    {
        this.targetPosition = targetPosition;
        this.damage = damage;
    }

    private void Update()
    {
        // Рассчитываем направление к цели
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Перемещаем снаряд в направлении цели
        transform.position += direction * speed * Time.deltaTime;

        // Проверяем расстояние до цели, чтобы уничтожить снаряд при попадании
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        // Попробуем найти объект игрока по позиции
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null)
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage((int)damage); // Приводим damage к int
            }
        }

        Destroy(gameObject); // Уничтожаем снаряд после попадания
    }

    private void Start()
    {
        // Уничтожаем снаряд через 3 секунды, если он не попал в игрока
        Destroy(gameObject, 3f);
    }
}