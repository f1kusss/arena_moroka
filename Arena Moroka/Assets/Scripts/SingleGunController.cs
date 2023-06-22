using UnityEngine;

public class SingleGunController : MonoBehaviour
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
    public float fireDelay;
    public int magazineSize; 
    public float reloadTime;
    public int damage;

    private AudioSource audioSource;
    private float lastFireTime;
    private int currentAmmo;
    private bool isReloading;
    private Quaternion Rotation;
    private Quaternion NewRotation;

    private void Start()
    {

        // ��������� ���������� AudioSource
        audioSource = GetComponent<AudioSource>();
        currentAmmo = magazineSize; // ������������� ���������� ��������
    }

    private void Update()
    {
        if (isReloading)
        {
            return; // ���� ���� �����������, ��������� ����������
        }

        // ��������� �������� ��� ������� ����� ������ ����
        if ((Input.GetButtonDown("Fire1")) && CanShoot())
        {
            Shoot();
            lastFireTime = Time.time;

            // ��������� ���������� ��������
            currentAmmo--;

        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            StartReload();
        }

        // ������������ ������ � ������������ ������
        AlignWeapon();
    }

    private bool CanShoot()
    {
        // �������� ������� � ������� ���������� ��������
        return Time.time - lastFireTime >= fireDelay && currentAmmo > 0;
    }

    private void Shoot()
    {
        // �������� ���� (Ray) � ����������� ������
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ��������������� ����� ��������
        audioSource.PlayOneShot(shootSound);

        // ������ ���� � �������� ������������ � ���������
        if (Physics.Raycast(ray, out hit, fireRange))
        {
            // ��������� ���������
            Debug.Log("Hitting an object: " + hit.collider.name);

            if (hit.collider.tag == "Ground")
            {
                GameObject hitEffect = Instantiate(groundHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, groundHitEffectDuration);
            }

            if (hit.collider.tag == "Lizzard")
            {
                LizzardController lizzard = hit.collider.GetComponent<LizzardController>(); // �������� ��������� ����� �� �������������� �������

                if (lizzard != null)
                {
                    lizzard.TakeDamage(damage); // ������� ���� �����
                }

                GameObject hitEffect = Instantiate(lizzardHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, lizzardHitEffectDuration);
            }


            // ����������� ����� ���� ������ ��������� ���������
        }
        else
        {
            // ���� ��� �� ���������� � ��������, ������������ ������
            Debug.Log("������");
        }
    }

    private void AlignWeapon()
    {
        // ��������� ����������� ������� ������
        Vector3 cameraDirection = mainCamera.transform.forward;

        // ������������ ������ � ������������ ������
        transform.rotation = Quaternion.LookRotation(cameraDirection);

        transform.Rotate(-90, -180, 0);
    }

    private void StartReload()
    {
        if (!isReloading)
        {
            // ���� �� ���� �����������, ��������� ��
            Debug.Log("�����������...");

            isReloading = true;
            currentAmmo = 0;

            // ��������� ������ �����������
            Invoke("FinishReload", reloadTime);
        }
    }

    private void FinishReload()
    {
        // ��������� �����������
        Debug.Log("����������� ���������");

        isReloading = false;
        currentAmmo = magazineSize;
    }
}
