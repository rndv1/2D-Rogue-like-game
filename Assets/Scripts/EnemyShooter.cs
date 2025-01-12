using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public int health = 100; // Health of the enemy
    public float speed = 2f; // Movement speed of the enemy
    public int attackDamage = 10; // Damage dealt to the player
    public float attackRange = 1.5f; // Range for melee attack
    public float attackCooldown = 1f; // Time between attacks
    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint; // Point from where projectiles are fired
    public float shootDistance = 5f; // Distance at which the enemy will shoot

    private Animator animator;
    private bool facingRight = true; // Direction the enemy is facing
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastAttackTime = Time.time; // Initialize last attack time
    }

    void Update()
    {
        Move();
        CheckHealth();
    }

    private void Move()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerObject.transform.position);

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer(playerObject.GetComponent<Player>());
            }
            else if (distanceToPlayer <= shootDistance)
            {
                ShootAtPlayer(playerObject.transform);
            }
            else
            {
                MoveTowardsPlayer(playerObject.transform);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveTowardsPlayer(Transform player)
    {
        float horizontalMovement = (player.position.x - transform.position.x) > 0 ? 1 : -1;

        if (horizontalMovement > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && facingRight)
        {
            Flip();
        }

        transform.Translate(Vector2.right * horizontalMovement * speed * Time.deltaTime);
        animator.SetBool("isMoving", true);
    }

    private void ShootAtPlayer(Transform player)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(player.position, attackDamage);
            }
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time; // Update last attack time
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
        animator.SetTrigger("Die");
        Destroy(gameObject, 1f); // Delay destruction to allow death animation
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0; // Prevent negative health
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
                Destroy(collision.gameObject); // Destroy the arrow
            }
        }
    }

    private void AttackPlayer(Player player)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            player.TakeDamage(attackDamage); // Deal damage to the player
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time; // Update last attack time
        }
    }
}