using UnityEngine;
using UnityEngine.UI; // Не забудьте подключить пространство имен для UI

public class Shield : MonoBehaviour
{
    public float shieldDuration = 5f; // Время действия щита
    private bool isActive = false;      // Активен ли щит
    public Image shieldImage;           // Ссылка на UI-элемент для отображения щита

    private void Start()
    {
        // Убедитесь, что изображение щита изначально скрыто
        if (shieldImage != null)
        {
            shieldImage.fillAmount = 0f; // Заполняемость 0 в начале
            shieldImage.gameObject.SetActive(false); // Скрываем изображение
        }
    }

    private void Update()
    {
        // Проверка, активен ли щит, и уменьшение времени действия
        if (isActive)
        {
            shieldDuration -= Time.deltaTime;

            // Обновляем заполнение изображения щита
            if (shieldImage != null)
            {
                shieldImage.fillAmount = shieldDuration / 5f; // Заполнение от 0 до 1
            }

            if (shieldDuration <= 0)
            {
                DeactivateShield();
            }
        }
    }

    public void ActivateShield()
    {
        isActive = true;
        shieldDuration = 5f; // Сбрасываем длительность при активации
        if (shieldImage != null)
        {
            shieldImage.gameObject.SetActive(true); // Показываем изображение щита
            shieldImage.fillAmount = 1f; // Заполняем на максимум
        }

        // Удаляем эту строку, чтобы щит не пропадал при подборе
        // gameObject.SetActive(false); 

        Debug.Log("Щит активирован!");
    }

    private void DeactivateShield()
    {
        isActive = false;
        if (shieldImage != null)
        {
            shieldImage.gameObject.SetActive(false); // Скрываем изображение щита при деактивации
        }
        Debug.Log("Щит деактивирован!");
    }

    public bool IsShieldActive()
    {
        return isActive;
    }
}
