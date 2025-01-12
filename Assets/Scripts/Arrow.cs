using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public int damage;
    private Vector2 moveDirection;
    public LayerMask whatIsSolid;

    // ����� ��� ��������� ����������� ����� �������� ������
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // ����������� �����������

        // ������������� ������� ������ � ����������� �����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // ������� ������ ����� 3 ������� ����� ��������
        Destroy(gameObject, 3f);
    }

    // ����������� ������
    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }

    // ��������� ������������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ���� ����������� � ������, ������� ����
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // �������� ������ ����� ��������� � �����
            Destroy(gameObject);
        }
        else if (whatIsSolid == (whatIsSolid | (1 << collision.gameObject.layer)))
        {
            // ���� ����������� � ������ ������� ��������, ��������, ������
            Destroy(gameObject);
        }
    }
}
