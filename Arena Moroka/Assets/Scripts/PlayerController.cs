using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player settings")]
    public float moveSpeed = 10f;
    public float jumpForce;
    public float maxHealth;
    public float currentHealth;
    public float healInterval;
    public float exp;
    public float maxExp;
    public int level;
    public float heal = 0.25f;
    public Image abilityIMG;

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

    [Header("Ability")]
    public float abilityDuration = 5f; // ������������ �����������
    public float abilityCooldown = 15f; // ����� ����������� �����������

    private bool isAbilityActive = false; // ����, ������������, ������� �� �����������
    private float abilityTimer = 0f; // ������ ��� ������������ ������������ �����������
    private float cooldownTimer = 0f; // ������ ��� ������������ ����������� �����������


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
        GameManager gameManager = GameManager.Instance;
        timeSinceLastDamage += Time.deltaTime;
        
        if (exp >= maxExp)
        {
            gameManager.roubles += 1;
            level += 1;
            exp = 0;
            maxExp += 10;
            StartCoroutine(startAnim(lvlLeft, lvlRight, lvlText));

        }
        // ���� ������ ���������� �������, �������� ������
        if ((timeSinceLastDamage >= healInterval) && currentHealth < gameManager.health)
        {
            currentHealth += heal;
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


        // ���� ����������� �������, ��������� ������ ������������ �����������
        if (isAbilityActive)
        {
            abilityTimer -= Time.deltaTime;
            if (abilityTimer <= 0f)
            {
                // ����� ����������� �������, ������������ �����������
                isAbilityActive = false;

                heal = 0.25f;
                moveSpeed = 10f;
            }
        }
        else
        {
            // ���� ����������� ���������, ����������� ������ ����������� �����������
            cooldownTimer += Time.deltaTime;
            abilityIMG.color = Color.gray;
        }

        // ���������, ����� �� ������������ �����������
        if (cooldownTimer >= abilityCooldown)
        {
            abilityIMG.color = Color.white;

            // ����� ����� �������� ��� ��� ��������� �����������, ��������, ������� ������� ��� ������� �������
            if (Input.GetKeyDown(KeyCode.E))
            {
                ActivateAbility();
            }
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
        if (collision.gameObject.CompareTag("Torgash"))
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.nearTorgash = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Torgash"))
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.nearTorgash = false;
        }
    }
    private void Awake()
    {
        GameManager gameManager = GameManager.Instance;
        maxHealth = gameManager.health;
    }
    private void ActivateAbility()
    {
        
        isAbilityActive = true;
        abilityTimer = abilityDuration;
        cooldownTimer = 0f;

        heal = 0.5f;
        moveSpeed = 15f;
        abilityIMG.color = Color.red;
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

            // ��������� ����������� ������������
            float t = Mathf.Clamp01(elapsedTime / unhidingTime);
            float ti = Mathf.Clamp01(elapsedTime / (unhidingTime-5.5f));

            // ������������� ������� ������� ����� ��������� � �������� �������
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

            // ��������� ����������� ������������
            float t = Mathf.Clamp01(elapsedTime / unhidingTime);
            float ti = Mathf.Clamp01(elapsedTime / (unhidingTime - 6.5f));

            // ������������� ������� ������� ����� ��������� � �������� �������
            lvlLeft.anchoredPosition = Vector2.Lerp(left.anchoredPosition, endLeft2, t);
            lvlRight.anchoredPosition = Vector2.Lerp(right.anchoredPosition, endRight2, t);
            lvlText.color = Color.Lerp(startColor, targetColor, ti);


            yield return null;

        }

    }
}
