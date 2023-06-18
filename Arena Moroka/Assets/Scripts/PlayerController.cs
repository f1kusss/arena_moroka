using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Скорость и сила прыжка")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Получение ввода для передвижения по горизонтали и вертикали
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Получение направления взгляда камеры
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Исключение вертикальной составляющей, чтобы движение оставалось на плоскости
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Нормализация направлений
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Рассчет направления движения игрока
        Vector3 movementDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        // Применение движения к игроку
        rb.velocity = new Vector3(movementDirection.x * moveSpeed, rb.velocity.y, movementDirection.z * moveSpeed);

        // Прыжок при нажатии пробела
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверка, когда игрок касается земли для возможности прыжка
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
