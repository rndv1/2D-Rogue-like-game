using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator animator;

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleAnimation();
        HandleFlip();
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
}
