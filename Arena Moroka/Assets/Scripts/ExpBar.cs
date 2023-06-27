using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Image healthBar;
    public PlayerController player;
    public Gradient gradient;

    void Update()
    {
        float fillAmount = player.exp / player.maxExp;

        healthBar.fillAmount = fillAmount;

        // ��������� ����� �� ��������� � ����������� �� fillAmount
        Color color = gradient.Evaluate(fillAmount);

        // ���������� ����� � ������� ��������
        healthBar.color = color;
    }
}