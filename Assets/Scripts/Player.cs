using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public int maxHealth = 100; // Максимальное здоровье игрока
    [SerializeField] private int currentHealth; // Текущее здоровье игрока
    public Text healthDisplay; 
    public Shield shielTimer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator animator;

    private bool facingRight = true;

    public GameObject shield;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Не инициализируем currentHealth здесь, чтобы использовать значение из инспектора
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth; // Устанавливаем текущее здоровье равным максимальному, если оно не задано в инспекторе
        }
    }

    void Update()
    {
        HandleMovementInput();
        HandleAnimation();
        HandleFlip();
    }

    private void HandleMovementInput()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void HandleAnimation()
    {
        bool isRunning = moveInput.x != 0 || moveInput.y != 0;
        animator.SetBool("isRunning", isRunning);
    }

    private void HandleFlip()
    {
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
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage + ". Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potion"))
        {
            ChangeHealth(5);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            shield.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    private void ChangeHealth(int amount)
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (shield.activeInHierarchy || shield.activeInHierarchy && currentHealth > 0)
        {
            currentHealth += amount;
            healthDisplay.text = "HP: " + currentHealth;
        }

        Debug.Log("Player health changed by: " + amount + ". Current health: " + currentHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}