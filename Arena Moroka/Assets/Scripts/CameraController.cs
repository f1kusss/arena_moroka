using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Чувствительность мыши")]
    public float mouseSensitivity;

    [Header("Привязка игрока")]
    public Transform player;

    [Header("Ограничение вертикального обзора")]
    public float verticalAngleLimit;

    private float mouseX;
    private float mouseY;
    private float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Установка начального взгляда камеры
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Вращение игрока по горизонтали
        player.Rotate(Vector3.up * mouseX);

        // Вращение камеры по вертикали с ограничением
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalAngleLimit, verticalAngleLimit);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
