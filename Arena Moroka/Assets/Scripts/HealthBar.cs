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

        // Получение цвета из градиента в зависимости от fillAmount
        Color color = gradient.Evaluate(fillAmount);

        // Применение цвета к полоске здоровья
        healthBar.color = color;
    }
}