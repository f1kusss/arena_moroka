using UnityEngine;

public class SingleGunController : MonoBehaviour
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

    [Header("Настройки оружия")]
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

        // Получение компонента AudioSource
        audioSource = GetComponent<AudioSource>();
        currentAmmo = magazineSize; // Инициализация количества патронов
    }

    private void Update()
    {
        if (isReloading)
        {
            return; // Если идет перезарядка, прерываем обновление
        }

        // Обработка стрельбы при нажатии левой кнопки мыши
        if ((Input.GetButtonDown("Fire1")) && CanShoot())
        {
            Shoot();
            lastFireTime = Time.time;

            // Уменьшаем количество патронов
            currentAmmo--;

        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            StartReload();
        }

        // Выравнивание оружия с направлением камеры
        AlignWeapon();
    }

    private bool CanShoot()
    {
        // Проверка времени с момента последнего выстрела
        return Time.time - lastFireTime >= fireDelay && currentAmmo > 0;
    }

    private void Shoot()
    {
        // Создание луча (Ray) в направлении камеры
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Воспроизведение звука выстрела
        audioSource.PlayOneShot(shootSound);

        // Выпуск луча и проверка столкновения с объектами
        if (Physics.Raycast(ray, out hit, fireRange))
        {
            // Обработка попадания
            Debug.Log("Попадание в объект: " + hit.collider.name);

            // Создание эффекта попадания
            GameObject hitEffect = Instantiate(groundHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            // Удаление эффекта попадания через заданное время
            Destroy(hitEffect, groundHitEffectDuration);

            // Продолжайте здесь свою логику обработки попадания
        }
        else
        {
            // Если луч не столкнулся с объектом, обрабатываем промах
            Debug.Log("Промах");
        }
    }

    private void AlignWeapon()
    {
        // Получение направления взгляда камеры
        Vector3 cameraDirection = mainCamera.transform.forward;

        // Выравнивание оружия с направлением камеры
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }

    private void StartReload()
    {
        if (!isReloading)
        {
            // Если не идет перезарядка, запускаем ее
            Debug.Log("Перезарядка...");

            isReloading = true;
            currentAmmo = 0;

            // Запускаем таймер перезарядки
            Invoke("FinishReload", reloadTime);
        }
    }

    private void FinishReload()
    {
        // Завершаем перезарядку
        Debug.Log("Перезарядка завершена");

        isReloading = false;
        currentAmmo = magazineSize;
    }
}
