using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    [Header("�������� ������")]
    public Camera mainCamera;

    [Header("������ ��������� �� �����")]
    public GameObject groundHitEffectPrefab;
    public float groundHitEffectDuration;

    [Header("������ ��������� �� �����")]
    public GameObject lizzardHitEffectPrefab;
    public float lizzardHitEffectDuration;

    [Header("��������� ������")]
    public float fireRange;
    private bool canDamage = false;

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

        Invoke(nameof(AttackDuration), 1.5f);

    }

    private void AlignWeapon()
    {
        Vector3 cameraDirection = mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }

    private void RayAttackZone()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, fireRange))
        {
            Debug.Log("��������� � ������: " + hit.collider.name);

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
            Debug.Log("������");
        }
    }

    private void AttackDuration()
	{
        isAnimating = false;
    }
}
