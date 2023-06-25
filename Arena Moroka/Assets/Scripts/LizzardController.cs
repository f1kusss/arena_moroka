using UnityEngine;

public class LizzardController : MonoBehaviour
{
    [Header("Lizzard settings")]
    public int health; // �������� �����
    public GameObject FloatingTextPrefab;

    public void TakeDamage(int damage)
    {
        health -= damage; // �������� ���� �� ��������

        ShowFloatingText(damage.ToString());
        
        if (health <= 0)
        {
            Die(); // ���� �������� ���������� �� ���� ��� ����, �������� ����� ������
        }
    }

    private void ShowFloatingText(string text)
    {
        if (FloatingTextPrefab)
        {
            var go = Instantiate(FloatingTextPrefab, transform.position, transform.rotation, transform);
            go.GetComponentInChildren<TMPro.TextMeshPro>().text = text; 
        }
    }

    private void Die()
    {
        // ����� ����� �������� ���, ������������� ��� ������ �����
        Destroy(gameObject); // ���������� ������ �����
    }
}
