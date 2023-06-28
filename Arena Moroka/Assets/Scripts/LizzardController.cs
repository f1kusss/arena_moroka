using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class LizzardController : MonoBehaviour
{
    [Header("Lizzard settings")]
    public int health;
    public GameObject FloatingTextPrefab;
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    public float attackRange;
    public float attackCooldown;
    public int damage;
    private bool canAttack = true;
    private PlayerController playerContr;
    public GameObject harcha = null;
    private Vector3 randomPoint;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform;
        playerContr = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null)
        {
            
            Ray ray = new Ray(transform.position, target.position - transform.position);
            RaycastHit hit;
            Debug.DrawLine(transform.position, target.position, Color.red);
            if (Physics.Raycast(ray, out hit, attackRange))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (canAttack)
                    {
                        Attack();
                        return;
                    }
                    else if (canAttack == false && gameObject.name == "Lizzard wizard")
                    {
                        agent.SetDestination(randomPoint);
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            Vector3 direction = target.position - transform.position;
                            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*7);
                        }
                    }
                }
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }
    }

    public void Attack()
    {
        if (gameObject.tag == "Lizzard wizard")
        {
            Vector3 direction = target.position - transform.position;
            GameObject harch = Instantiate(harcha, transform.position, transform.rotation);
            harch.GetComponent<Rigidbody>().velocity = direction.normalized * 15f;
            randomPoint.x = Random.Range(transform.position.x - agent.speed*1.4f, transform.position.x + agent.speed*1.4f);
            randomPoint.z = Random.Range(transform.position.z - agent.speed*1.4f, transform.position.z + agent.speed*1.4f);
            randomPoint.y = transform.position.y;
        }
        else
        {
            animator.SetTrigger("Attack");
            animator.SetTrigger("Run");
        }

        canAttack = false;
        StartCoroutine(AttackDelayCoroutine());
    }

    public void damagePlayer()
    {
        Ray ray = new Ray(transform.position, target.position - transform.position);
        RaycastHit hit;
        Debug.DrawLine(transform.position, target.position, Color.red);

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            playerContr.TakeDamage(damage);
            Debug.Log("dealed damage");

        }
    }

    

    private IEnumerator AttackDelayCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ShowFloatingText(damage.ToString());

        if (health <= 0)
        {
            Die();
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
        Destroy(gameObject);
    }
}
