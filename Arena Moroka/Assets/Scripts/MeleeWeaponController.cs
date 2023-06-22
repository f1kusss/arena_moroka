using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    [Header("Binding camera")]
    public Camera mainCamera;

    [Header("Ground shoot effect")]
    public GameObject groundHitEffectPrefab;
    public float groundHitEffectDuration;

    [Header("Lizzard shoot effect")]
    public GameObject lizzardHitEffectPrefab;
    public float lizzardHitEffectDuration;

    [Header("Weapon settings")]
    public float fireRange;
    public int damage;

    private Animator anim;
    private bool isAnimating = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAnimating)
        {
            Attack();
        }
    }


    private void Attack()
    {
        isAnimating = true;

        anim.SetTrigger("Attack");

        Invoke(nameof(AttackDuration), 1.5f);

    }

    private void RayAttackZone()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, fireRange))
        {
            Debug.Log("Hitting an object: " + hit.collider.name);

            if (hit.collider.CompareTag("Ground"))
            {
                GameObject hitEffect = Instantiate(groundHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, groundHitEffectDuration);
            }

            if (hit.collider.CompareTag("Lizzard"))
            {
                LizzardController lizzard = hit.collider.GetComponent<LizzardController>(); // Получаем компонент врага из столкнувшегося объекта

                if (lizzard != null)
                {
                    lizzard.TakeDamage(damage); // Наносим урон врагу
                }

                GameObject hitEffect = Instantiate(lizzardHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, lizzardHitEffectDuration);
            }
        }
        
        else
        {
            Debug.Log("������");
        }
    }

    private void AttackDuration()
	{
        isAnimating = false;
    }
}
