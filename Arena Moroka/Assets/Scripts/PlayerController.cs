using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player settings")]
    public float moveSpeed;
    public float jumpForce;
    public int takeDamage;
    public float maxHealth;
    public float currentHealth;
    public float healInterval;

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isJumping = false;
    private float timeSinceLastDamage = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        currentHealth = maxHealth;
    }

    private void Update()
    {
        timeSinceLastDamage += Time.deltaTime;

        // Если прошло достаточно времени, исцеляем игрока
        if ((timeSinceLastDamage >= healInterval) && currentHealth < 100)
        {
            currentHealth += 0.25f;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movementDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        rb.velocity = new Vector3(movementDirection.x * moveSpeed, rb.velocity.y, movementDirection.z * moveSpeed);

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
        timeSinceLastDamage = 0f;
        currentHealth -= takeDamage;

        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log("Health: " + currentHealth);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
