using UnityEngine;

public class SingleGunController : MonoBehaviour
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

    [Header("��������� ������")]
    public float fireRange;
    public float fireDelay;
    public int magazineSize; 
    public float reloadTime; 

    private AudioSource audioSource;
    private float lastFireTime;
    private int currentAmmo;
    private bool isReloading;

    private void Start()
    {
        transform.Rotate(-90, -180, 0);

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
            Debug.Log("��������� � ������: " + hit.collider.name);

            // �������� ������� ���������
            GameObject hitEffect = Instantiate(groundHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            // �������� ������� ��������� ����� �������� �����
            Destroy(hitEffect, groundHitEffectDuration);

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
