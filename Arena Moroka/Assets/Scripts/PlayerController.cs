using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player settings")]
    public float moveSpeed;
    public float jumpForce;
    public float maxHealth;
    public float currentHealth;
    public float healInterval;
    public float exp;
    public float maxExp;
    public int level;

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isJumping = false;
    private float timeSinceLastDamage = 0f;

    [Header("LevelUp unhiding")]
    public RectTransform lvlLeft;
    public RectTransform lvlRight;
    public TextMeshProUGUI lvlText;
    public float unhidingTime;
    private float elapsedTime = 0.0f;
    private Vector2 endLeft, endRight, endLeft2, endRight2;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        exp = 0;
        level = 1;
        currentHealth = maxHealth;
        endLeft = new Vector2(0, lvlLeft.anchoredPosition.y);
        endRight = new Vector2(0, lvlLeft.anchoredPosition.y);
        endLeft2 = new Vector2(-1920, lvlLeft.anchoredPosition.y);
        endRight2 = new Vector2(1920, lvlLeft.anchoredPosition.y);
    }

    private void Update()
    {
        timeSinceLastDamage += Time.deltaTime;
        exp += 0.025f;
        if (exp >= maxExp)
        {
            level += 1;
            exp = 0;
            maxExp *= 2;
            StartCoroutine(startAnim(lvlLeft, lvlRight, lvlText));

        }
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

        Vector3 movementDirection = cameraForward * moveVertical + cameraRight * moveHorizontal;

        rb.velocity = new Vector3(movementDirection.x * moveSpeed, rb.velocity.y, movementDirection.z * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        timeSinceLastDamage = 0f;
        currentHealth -= damage;

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

    IEnumerator startAnim(RectTransform left, RectTransform right, TextMeshProUGUI text)
    {
        yield return MoveObject(left, right, text);
        elapsedTime = 0f;
        yield return ReturnObject(left, right, text);
        elapsedTime = 0f;
    }

    IEnumerator MoveObject(RectTransform left, RectTransform right, TextMeshProUGUI text)
    {
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
        while (elapsedTime < unhidingTime)
        {
            elapsedTime += Time.deltaTime;

            // Вычисляем коэффициент интерполяции
            float t = Mathf.Clamp01(elapsedTime / unhidingTime);
            float ti = Mathf.Clamp01(elapsedTime / (unhidingTime-5.5f));

            // Интерполируем позицию объекта между начальной и конечной точками
            lvlLeft.anchoredPosition = Vector2.Lerp(left.anchoredPosition, endLeft, t);
            lvlRight.anchoredPosition = Vector2.Lerp(right.anchoredPosition, endRight, t);
            lvlText.color = Color.Lerp(startColor, targetColor, ti);

            yield return null;
            
            
        }
        
    }
    IEnumerator ReturnObject(RectTransform left, RectTransform right, TextMeshProUGUI text)
    {
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        while (elapsedTime < unhidingTime)
        {
            elapsedTime += Time.deltaTime;

            // Вычисляем коэффициент интерполяции
            float t = Mathf.Clamp01(elapsedTime / unhidingTime);
            float ti = Mathf.Clamp01(elapsedTime / (unhidingTime - 6.5f));

            // Интерполируем позицию объекта между начальной и конечной точками
            lvlLeft.anchoredPosition = Vector2.Lerp(left.anchoredPosition, endLeft2, t);
            lvlRight.anchoredPosition = Vector2.Lerp(right.anchoredPosition, endRight2, t);
            lvlText.color = Color.Lerp(startColor, targetColor, ti);


            yield return null;

        }

    }
}
