using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    [Header("Привязка камеры")]
    public Camera mainCamera;

    [Header("Эффект попадания по земле")]
    public GameObject groundHitEffectPrefab;
    public float groundHitEffectDuration;

    [Header("Эффект попадания по ящеру")]
    public GameObject lizzardHitEffectPrefab;
    public float lizzardHitEffectDuration;

    [Header("Настройки оружия")]
    public float fireRange;

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

        AlignWeapon();
    }


    private void Attack()
    {
        isAnimating = true;

        anim.SetTrigger("Attack");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, fireRange))
        {
            Debug.Log("Попадание в объект: " + hit.collider.name);

            if (hit.collider.CompareTag("Ground"))
            {
                GameObject hitEffect = Instantiate(groundHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, groundHitEffectDuration);
            }

            if (hit.collider.CompareTag("Lizzard"))
            {
                GameObject hitEffect = Instantiate(lizzardHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, lizzardHitEffectDuration);
            }
        }
        else
        {
            Debug.Log("Промах");
        }

        Invoke(nameof(AttackDuration), 1.5f);

    }

    private void AlignWeapon()
    {
        Vector3 cameraDirection = mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }

    private void AttackDuration()
	{
        isAnimating = false;
    }
}
