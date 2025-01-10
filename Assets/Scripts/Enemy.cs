using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int attackDamage = 10; // Урон, который враг наносит игроку

    private Animator animator;
    private bool facingRight = false; // Начальное направление врага смотрит влево

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("AnimatorController не назначен на объект " + name);
        }
    }

    void Update()
    {
        Move();
        CheckHealth();
    }

    private void Move()
    {
        float horizontalMovement = GetHorizontalInput(); // Функция получения ввода или логики направления

        if (horizontalMovement > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && facingRight)
        {
            Flip();
        }

        // Движение объекта
        transform.Translate(Vector2.right * horizontalMovement * speed * Time.deltaTime);
        animator.SetBool("isMoving", Mathf.Abs(horizontalMovement) > 0); // Только когда движение активно
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died.");
        animator.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage: " + damage + ". Current health: " + health);
        animator.SetTrigger("TakeDamage");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                AttackPlayer(player);
            }
        }
        else if (collision.CompareTag("Arrow"))
        {
            Arrow arrow = collision.GetComponent<Arrow>();
            if (arrow != null)
            {
                TakeDamage(arrow.damage);
                Destroy(collision.gameObject);
            }
        }
    }

    private void AttackPlayer(Player player)
    {
        Debug.Log("Enemy attacks player!");
        player.TakeDamage(attackDamage); // Метод атаки, который наносит урон игроку
        animator.SetTrigger("Attack");
    }

    // Пример метода для получения ввода, замените на ваш собственный механизм
    private float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal"); // Или используйте другую логику
    }
}