using UnityEngine;
using TMPro;

public class AutoGunController : MonoBehaviour
{
    [Header("Binding camera")]
    public Camera mainCamera;

    [Header("Fire point")]
    public Transform firePoint;

    [Header("Shoot sound")]
    public AudioClip shootSound;

    [Header("Ground shoot effect")]
    public GameObject groundHitEffectPrefab;
    public float groundHitEffectDuration;

    [Header("Lizzard shoot effect")]
    public GameObject lizzardHitEffectPrefab;
    public float lizzardHitEffectDuration;

    [Header("Gun settings")]
    public float fireRange;
    public float fireRate; 
    public int magazineSize;
    public float reloadTime;
    public int damage;

    public TextMeshProUGUI ammo;
    public int currentAmmo = 15;

    private AudioSource audioSource;
    private float lastFireTime;
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

            ammo.text = currentAmmo.ToString();
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
            Debug.Log("Hitting an object: " + hit.collider.name);

            if (hit.collider.tag == "Ground")
            {
                GameObject hitEffect = Instantiate(groundHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, groundHitEffectDuration);
            }

            if (hit.collider.tag == "Lizzard" || hit.collider.tag == "Lizzard wizzard")
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
            Debug.Log("Промах");
        }
    }

    private void AlignWeapon()
    {
        Vector3 cameraDirection = mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(cameraDirection);

        transform.Rotate(0, 180, 0);
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

        ammo.text = currentAmmo.ToString();
    }
    private void Awake()
    {
        GameManager gameManager = GameManager.Instance;
        damage = gameManager.auto;
        magazineSize = gameManager.ammo;
        currentAmmo = gameManager.ammo;
    }
}
