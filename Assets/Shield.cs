using UnityEngine;
using UnityEngine.UI; // �� �������� ���������� ������������ ���� ��� UI

public class Shield : MonoBehaviour
{
    public float shieldDuration = 5f; // ����� �������� ����
    private bool isActive = false;      // ������� �� ���
    public Image shieldImage;           // ������ �� UI-������� ��� ����������� ����

    private void Start()
    {
        // ���������, ��� ����������� ���� ���������� ������
        if (shieldImage != null)
        {
            shieldImage.fillAmount = 0f; // ������������� 0 � ������
            shieldImage.gameObject.SetActive(false); // �������� �����������
        }
    }

    private void Update()
    {
        // ��������, ������� �� ���, � ���������� ������� ��������
        if (isActive)
        {
            shieldDuration -= Time.deltaTime;

            // ��������� ���������� ����������� ����
            if (shieldImage != null)
            {
                shieldImage.fillAmount = shieldDuration / 5f; // ���������� �� 0 �� 1
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
        shieldDuration = 5f; // ���������� ������������ ��� ���������
        if (shieldImage != null)
        {
            shieldImage.gameObject.SetActive(true); // ���������� ����������� ����
            shieldImage.fillAmount = 1f; // ��������� �� ��������
        }

        // ������� ��� ������, ����� ��� �� �������� ��� �������
        // gameObject.SetActive(false); 

        Debug.Log("��� �����������!");
    }

    private void DeactivateShield()
    {
        isActive = false;
        if (shieldImage != null)
        {
            shieldImage.gameObject.SetActive(false); // �������� ����������� ���� ��� �����������
        }
        Debug.Log("��� �������������!");
    }

    public bool IsShieldActive()
    {
        return isActive;
    }
}
