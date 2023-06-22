using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player settings")]
    public float moveSpeed;
    public float jumpForce;
    public int health;
    public int takeDamage;

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
        // ��������� ����� ��� ������������ �� ����������� � ���������
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ��������� ����������� ������� ������
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // ���������� ������������ ������������, ����� �������� ���������� �� ���������
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // ������������ �����������
        cameraForward.Normalize();
        cameraRight.Normalize();

        // ������� ����������� �������� ������
        Vector3 movementDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        // ���������� �������� � ������
        rb.velocity = new Vector3(movementDirection.x * moveSpeed, rb.velocity.y, movementDirection.z * moveSpeed);

        // ������ ��� ������� �������
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        health -= takeDamage; // �������� ���� �� ��������

        if (health < 0)
            health = 0;

        Debug.Log("Health: " + health);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // ��������, ����� ����� �������� ����� ��� ����������� ������
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }


}
