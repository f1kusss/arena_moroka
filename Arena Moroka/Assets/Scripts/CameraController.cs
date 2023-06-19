using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("���������������� ����")]
    public float mouseSensitivity;

    [Header("�������� ������")]
    public Transform player;

    [Header("����������� ������������� ������")]
    public float verticalAngleLimit;

    private float mouseX;
    private float mouseY;
    private float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // ��������� ���������� ������� ������
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // �������� ������ �� �����������
        player.Rotate(Vector3.up * mouseX);

        // �������� ������ �� ��������� � ������������
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalAngleLimit, verticalAngleLimit);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
