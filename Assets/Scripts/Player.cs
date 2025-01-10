using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int maxHealth = 100; // Максимальное здоровье игрока
    private int currentHealth; // Текущее здоровье игрока

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator animator;

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Устанавливаем текущее здоровье равным максимальному
    }

    void Update()
    {
        HandleMovementInput();
        HandleAnimation();
        HandleFlip();
        CheckHealth(); // Проверяем здоровье в каждом кадре
    }

    private void HandleMovementInput()
    {
        // Получаем ввод пользователя
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void HandleAnimation()
    {
        // Устанавливаем анимацию бега
        bool isRunning = moveInput.x != 0 || moveInput.y != 0;
        animator.SetBool("isRunning", isRunning);
    }

    private void HandleFlip()
    {
        // Проверяем, нужно ли перевернуть спрайт персонажа
        if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput.x < 0)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Двигаем персонажа
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        // Меняем направление персонажа
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void Die()
    {
        // Логика для обработки смерти игрока
        Debug.Log("Player has died.");

        // Например, вы можете добавить анимацию смерти или другие действия
        // Также возможен переход на экран завершения игры или перезагрузка уровня
        // Например, использовать Destroy(gameObject) для удаления игрока
        Destroy(gameObject); // Если вы хотите удалить объект игрока из сцены
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем текущее здоровье на полученный урон
        Debug.Log("Player took damage: " + damage + ". Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Вызываем метод смерти, если здоровье упало до нуля
        }
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            // Логика для обработки смерти игрока (например, перезагрузка уровня или отображение меню)
            Debug.Log("Player has died.");
            // Здесь можно добавить анимацию смерти или другие действия
            // Например, Destroy(gameObject); если нужно удалить игрока из сцены
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth; // Метод для получения текущего здоровья игрока
    }
}