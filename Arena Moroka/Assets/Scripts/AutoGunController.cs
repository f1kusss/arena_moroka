using UnityEngine;

public class AutoGunController : MonoBehaviour
{
    [Header("Привязка камеры")]
    public Camera mainCamera;

    [Header("Точка выстрела")]
    public Transform firePoint;

    [Header("Звук выстрела")]
    public AudioClip shootSound;

    [Header("Эффект попадания по земле")]
    public GameObject groundHitEffectPrefab;
    public float groundHitEffectDuration;

    [Header("Эффект попадания по ящеру")]
    public GameObject lizzardHitEffectPrefab;
    public float lizzardHitEffectDuration;

    [Header("Настройки оружия")]
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
            Debug.Log("Попадание в объект: " + hit.collider.name);

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
            Debug.Log("Промах");
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
            Debug.Log("Перезарядка...");

            isReloading = true;
            currentAmmo = 0;

            Invoke("FinishReload", reloadTime);
        }
    }

    private void FinishReload()
    {
        Debug.Log("Перезарядка завершена");

        isReloading = false;
        currentAmmo = magazineSize;
    }
}
