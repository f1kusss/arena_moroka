using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class LizzardController : MonoBehaviour
{
    [Header("Lizzard settings")]
    public int health; // �������� �����
    public GameObject FloatingTextPrefab;
    public Transform target; // Ссылка на цель (игрока)
    private NavMeshAgent agent; // Ссылка на компонент NavMeshAgent
    public float attackRange;
    public float attackCooldown;
    public int damage;
    public float attackDelay = 2f; // Задержка атаки в секундах
    private float attackTimer = 0f; // Таймер задержки атаки
    private bool canAttack = true; // Флаг, указывающий, можно ли атаковать
    public PlayerController playerContr;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            Ray ray = new Ray(transform.position, target.position - transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, attackRange))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {

                    if (canAttack)
                    {
                        Attack();
                        return;
                    }
                }
            }
        }
    }

    public void Attack()
    {
        playerContr.TakeDamage(damage);
        canAttack = false;
        attackTimer = attackDelay;
        StartCoroutine(AttackDelayCoroutine());
    }

    private IEnumerator AttackDelayCoroutine()
    {
        while (attackTimer > 0f)
        {
            // Уменьшить таймер задержки атаки
            attackTimer -= Time.deltaTime;

            yield return null;
        }

        // Завершить задержку атаки, разрешить следующую атаку
        canAttack = true;
    }

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
