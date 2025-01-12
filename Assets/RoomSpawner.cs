using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomVariants variants;
    private static int roomCount = 0; // Счетчик спавненных комнат
    private const int maxRooms = 5; // Максимальное количество комнат спавна
    private int rand;
    private bool spawned = false;

    void Start()
    {
        GameObject roomsObject = GameObject.FindGameObjectWithTag("Rooms");
        if (roomsObject != null)
        {
            variants = roomsObject.GetComponent<RoomVariants>();
            if (variants == null)
            {
                Debug.LogError("RoomVariants component not found on object with tag 'Rooms'.");
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            Debug.LogError("Object with tag 'Rooms' not found.");
            Destroy(gameObject);
            return;
        }

        // Спавн комнаты через короткую задержку
        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        // Проверка на уже созданные комнаты и лимит
        if (spawned || variants == null || roomCount >= maxRooms) return;

        // Спавн комнаты в зависимости от направления
        switch (direction)
        {
            case Direction.Top:
                rand = Random.Range(0, variants.topRooms.Length);
                Instantiate(variants.topRooms[rand], transform.position, Quaternion.identity);
                break;
            case Direction.Bottom:
                rand = Random.Range(0, variants.bottomRooms.Length);
                Instantiate(variants.bottomRooms[rand], transform.position, Quaternion.identity);
                break;
            case Direction.Right:
                rand = Random.Range(0, variants.rightRooms.Length);
                Instantiate(variants.rightRooms[rand], transform.position, Quaternion.identity);
                break;
            case Direction.Left:
                rand = Random.Range(0, variants.leftRooms.Length);
                Instantiate(variants.leftRooms[rand], transform.position, Quaternion.identity);
                break;
        }

        spawned = true; // Устанавливаем флаг о том, что комната создана
        roomCount++; // Увеличиваем счетчик спавненных комнат

        // Уничтожаем объект спавнера после завершения спавна
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Уничтожаем этот объект, если другая комната уже была создана
        if (other.CompareTag("RoomPoint"))
        {
            RoomSpawner otherSpawner = other.GetComponent<RoomSpawner>();
            if (otherSpawner != null && otherSpawner.spawned)
            {
                Destroy(gameObject);
            }
        }
    }
}