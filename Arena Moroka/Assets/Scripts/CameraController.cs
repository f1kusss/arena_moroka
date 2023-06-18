using UnityEngine;

public class СameraController : MonoBehaviour
{
    [Header("Чувствительность мыши")]
    public float mouseSensitivity = 2f;

    [Header("Привязка игрока")]
    public Transform Player;

    private float mouseX;
    private float mouseY;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Player.Rotate(mouseX * new Vector3(0, 1, 0));
        transform.Rotate(-mouseY * new Vector3(1, 0, 0));
    }
}
