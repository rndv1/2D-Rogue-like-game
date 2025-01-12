using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // Находим основную камеру
    }

    private void OnTriggerEnter2D(Collider2D other) // Исправлена регистрация метода
    {
        if (other.CompareTag("Player"))
        {
            // Перемещаем игрока на заданную позицию
            other.transform.position += playerChangePos;

            // Перемещаем камеру на заданную позицию
            cam.transform.position += new Vector3(cameraChangePos.x, cameraChangePos.y, cam.transform.position.z);
        }
    }

    void Update()
    {
        // Ваши обновления здесь, если они необходимы
    }
}