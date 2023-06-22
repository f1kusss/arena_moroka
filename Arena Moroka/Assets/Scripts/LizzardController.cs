using UnityEngine;

public class LizzardController : MonoBehaviour
{
    [Header("Lizzard settings")]
    public int health; // Здоровье врага

    public void TakeDamage(int damage)
    {
        health -= damage; // Вычитаем урон из здоровья

        if (health <= 0)
        {
            Die(); // Если здоровье опустилось до нуля или ниже, вызываем метод смерти
        }
    }

    private void Die()
    {
        // Здесь можно добавить код, выполняющийся при смерти врага
        Destroy(gameObject); // Уничтожаем объект врага
    }
}
