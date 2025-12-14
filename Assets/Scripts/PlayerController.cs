using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public GameObject TractorBeam;
    public GameObject ArrowKeyGuide;
    public TextMeshProUGUI HealthText;
    public Color DamagedColor;
    public float ColorStep;
    public float ThrustForce;
    public float MaxSpeed;
    public int MaxHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            int previousHealth = currentHealth;
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            UpdateHealthText();
            if (currentHealth < previousHealth) targetColor = DamagedColor;
        }
    }

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    int currentHealth;
    Color originalColor;
    Color targetColor;
    float spawnX;
    float spawnY;
    bool spaceKeyIsAlreadyPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CurrentHealth = MaxHealth;
        originalColor = spriteRenderer.color;
        targetColor = originalColor;
        spawnX = transform.position.x;
        spawnY = transform.position.y;
        ArrowKeyGuide.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ToggleTractorBeam();
        LimitSpeed();
        UpdateColor();
    }

    void Move()
    {
        Vector2 direction = Vector2.zero;
        if (Keyboard.current.upArrowKey.isPressed) direction += Vector2.up;
        if (Keyboard.current.downArrowKey.isPressed) direction += Vector2.down;
        if (Keyboard.current.leftArrowKey.isPressed) direction += Vector2.left;
        if (Keyboard.current.rightArrowKey.isPressed) direction += Vector2.right;
        if (direction != Vector2.zero)
        {
            rb.AddForce(ThrustForce * Time.deltaTime * transform.localScale.x * direction);
            ArrowKeyGuide.SetActive(false);
        }
    }

    void ToggleTractorBeam()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            if (spaceKeyIsAlreadyPressed) return;
            spaceKeyIsAlreadyPressed = true;
            TractorBeam.GetComponent<TractorBeamController>().Toggle();
        }
        else
        {
            spaceKeyIsAlreadyPressed = false;
        }
    }

    void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > MaxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * MaxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectMetadata objectMetadata;
        if (collision.gameObject.TryGetComponent(out objectMetadata))
        {
            CurrentHealth -= objectMetadata.PlayerDamage;
            if (CurrentHealth <= 0)
            {
                transform.position = new Vector3(spawnX, spawnY, transform.position.z);
                CurrentHealth = MaxHealth;
                TractorBeam.GetComponent<TractorBeamController>().Toggle(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Goal":
                CurrentHealth = MaxHealth;
                break;
            default:
                break;
        }
    }

    void UpdateHealthText()
    {
        HealthText.text = "Health: " + CurrentHealth + "%";
    }

    void UpdateColor()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, ColorStep * Time.deltaTime);
        if (spriteRenderer.color == targetColor && targetColor != originalColor)
        {
            targetColor = originalColor;
        }
    }
}
