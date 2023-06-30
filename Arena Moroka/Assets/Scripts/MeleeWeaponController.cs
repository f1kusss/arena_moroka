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
        if (Input.GetButtonDown("Fire1") && !isAnimating && GunSwitch.currentWeaponIndex == 0)
        {
            Attack();
        }
    }


    private void Attack()
    {
        isAnimating = true;

        anim.SetTrigger("Attack");

        Invoke(nameof(AttackDuration), 4.2f);

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

            if (hit.collider.CompareTag("Lizzard") || hit.collider.tag == "Lizzard wizzard")
            {
                LizzardController lizzard = hit.collider.GetComponent<LizzardController>(); // Получаем компонент врага из столкнувшегося объекта

                if (lizzard != null)
                {
                    lizzard.TakeDamage(damage, hit); // Наносим урон врагу
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
    private void Awake()
    {
        GameManager gameManager = GameManager.Instance;
        damage = gameManager.hammer;
    }
}
