using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int attackDamage = 10; // Урон, который враг наносит игроку
    public float attackRange = 1.5f; // Дистанция, на которой враг может атаковать игрока
    public float attackCooldown = 1f; // Время между атаками

    private Animator animator;
    private bool facingRight = false; // Начальное направление врага смотрит влево
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("AnimatorController не назначен на объект " + name);
        }

        lastAttackTime = Time.time; // Инициализация времени последней атаки
    }

    void Update()
    {
        Move();
        CheckHealth();
    }

    private void Move()
    {
        // Найти игрока
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerObject.transform.position);

            // Если игрок в пределах досягаемости, атакуем
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer(playerObject.GetComponent<Player>());
            }
            else
            {
                // Движение к игроку
                float horizontalMovement = (playerObject.transform.position.x - transform.position.x) > 0 ? 1 : -1;

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
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
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
        if (collision.CompareTag("Arrow"))
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
        // Проверка на время между атаками
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Enemy attacks player!");
            player.TakeDamage(attackDamage); // Метод атаки, который наносит урон игроку
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time; // Обновляем время последней атаки
        }
    }
}