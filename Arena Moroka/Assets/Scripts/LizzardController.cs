using UnityEngine;

public class LizzardController : MonoBehaviour
{
    [Header("Lizzard settings")]
    public int health; // �������� �����

    public void TakeDamage(int damage)
    {
        health -= damage; // �������� ���� �� ��������

        if (health <= 0)
        {
            Die(); // ���� �������� ���������� �� ���� ��� ����, �������� ����� ������
        }
    }

    private void Die()
    {
        // ����� ����� �������� ���, ������������� ��� ������ �����
        Destroy(gameObject); // ���������� ������ �����
    }
}
