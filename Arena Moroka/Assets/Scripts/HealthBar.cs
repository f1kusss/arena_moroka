using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public PlayerController player;
    public Gradient gradient;

    void Update()
    {
        float fillAmount = player.currentHealth / player.maxHealth;

        healthBar.fillAmount = fillAmount;

        // ��������� ����� �� ��������� � ����������� �� fillAmount
        Color color = gradient.Evaluate(fillAmount);

        // ���������� ����� � ������� ��������
        healthBar.color = color;
    }
}