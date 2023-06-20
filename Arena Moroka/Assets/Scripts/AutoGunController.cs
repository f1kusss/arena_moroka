using UnityEngine;

public class AutoGunController : MonoBehaviour
{
    [Header("�������� ������")]
    public Camera mainCamera;

    [Header("����� ��������")]
    public Transform firePoint;

    [Header("���� ��������")]
    public AudioClip shootSound;

    [Header("������ ��������� �� �����")]
    public GameObject groundHitEffectPrefab;
    public float groundHitEffectDuration;

    [Header("������ ��������� �� �����")]
    public GameObject lizzardHitEffectPrefab;
    public float lizzardHitEffectDuration;

    [Header("��������� ������")]
    public float fireRange;
    public float fireRate; 
    public int magazineSize;
    public float reloadTime;
    public float damage;

    private AudioSource audioSource;
    private float lastFireTime;
    private int currentAmmo;
    private bool isReloading;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = magazineSize;
    }

    private void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetButton("Fire1") && CanShoot())
        {
            Shoot();
            lastFireTime = Time.time;

            currentAmmo--;

        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            StartReload();
        }

        AlignWeapon();
    }

    private bool CanShoot()
    {
        return Time.time - lastFireTime >= 1f / fireRate && currentAmmo > 0;
    }

    private void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        audioSource.PlayOneShot(shootSound);

        if (Physics.Raycast(ray, out hit, fireRange))
        {
            Debug.Log("��������� � ������: " + hit.collider.name);

            if (hit.collider.tag == "Ground")
            {
                GameObject hitEffect = Instantiate(groundHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, groundHitEffectDuration);
            }

            if (hit.collider.tag == "Lizzard")
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

    private void AlignWeapon()
    {
        Vector3 cameraDirection = mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }

    private void StartReload()
    {
        if (!isReloading && currentAmmo < magazineSize)
        {
            Debug.Log("�����������...");

            isReloading = true;
            currentAmmo = 0;

            Invoke("FinishReload", reloadTime);
        }
    }

    private void FinishReload()
    {
        Debug.Log("����������� ���������");

        isReloading = false;
        currentAmmo = magazineSize;
    }
}
