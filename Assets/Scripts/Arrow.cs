using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public int damage;
    private Vector2 moveDirection;
    public LayerMask whatIsSolid;

    // Метод для установки направления после создания стрелы
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // Нормализуем направление

        // Устанавливаем поворот стрелы в направлении полёта
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Удаляем стрелу через 3 секунды после выстрела
        Destroy(gameObject, 3f);
    }

    // Перемещение стрелы
    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }

    // Обработка столкновений
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Если столкнулись с врагом, наносим урон
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Удаление стрелы после попадания в врага
            Destroy(gameObject);
        }
        else if (whatIsSolid == (whatIsSolid | (1 << collision.gameObject.layer)))
        {
            // Если столкнулись с другим твердым объектом, например, стеной
            Destroy(gameObject);
        }
    }
}
