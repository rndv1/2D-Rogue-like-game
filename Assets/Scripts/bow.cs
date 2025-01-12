using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float offset;
    public GameObject arrowPrefab;  // ������ ������
    public float startTimeBtwShots;
    private float timeBtwShots;

    void Update()
    {
        AimAtMouse();
        HandleShooting();
    }

    private void AimAtMouse()
    {
        // ����������� ������� ������� �� ������ � ������� ����������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    private void HandleShooting()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0)) // ������� ����� ������ ����
            {
                Shoot();
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if (arrowPrefab != null)
        {
            // ������� ������ �� ����������� �� ������� ������� �� ��������� �������
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;

            GameObject arrowInstance = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrowInstance.GetComponent<Arrow>().SetDirection(direction);
        }
        else
        {
            Debug.LogWarning("Arrow prefab is not assigned in the inspector.");
        }
    }
}
