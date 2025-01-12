using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // �������� �������
    private Vector3 targetPosition; // ������� ����
    private float damage; // ���� �������

    // �������� ��� target � Transform �� Vector3
    public void Initialize(Vector3 targetPosition, float damage)
    {
        this.targetPosition = targetPosition;
        this.damage = damage;
    }

    private void Update()
    {
        // ������������ ����������� � ����
        Vector3 direction = (targetPosition - transform.position).normalized;

        // ���������� ������ � ����������� ����
        transform.position += direction * speed * Time.deltaTime;

        // ��������� ���������� �� ����, ����� ���������� ������ ��� ���������
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        // ��������� ����� ������ ������ �� �������
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null)
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage((int)damage); // �������� damage � int
            }
        }

        Destroy(gameObject); // ���������� ������ ����� ���������
    }

    private void Start()
    {
        // ���������� ������ ����� 3 �������, ���� �� �� ����� � ������
        Destroy(gameObject, 3f);
    }
}