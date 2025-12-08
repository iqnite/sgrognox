using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public GameObject TractorBeam;
    public TextMeshProUGUI HealthText;
    public float ThrustForce;
    public float MaxSpeed;
    public float RotationAdjustSpeed;
    public int MaxHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            UpdateHealthText();
        }
    }

    Rigidbody2D rb;
    int currentHealth = 100;
    float spawnX;
    float spawnY;
    bool spaceKeyIsAlreadyPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        spawnX = transform.position.x;
        spawnY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ToggleTractorBeam();
        LimitSpeed();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
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
}
