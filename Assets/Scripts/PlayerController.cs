using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public UnityEngine.GameObject tractorBeam;
    public TextMeshProUGUI healthText;
    public float thrustForce;
    public float maxSpeed;
    public float rotationAdjustSpeed;
    public int maxHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            UpdateHealthText();
        }
    }

    Rigidbody2D rb;
    int _currentHealth = 100;
    float spawnX;
    float spawnY;
    bool spaceKeyAlreadyPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHealth = maxHealth;
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
            rb.AddForce(thrustForce * Time.deltaTime * transform.localScale.x * direction);
        }
    }

    void ToggleTractorBeam()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            if (spaceKeyAlreadyPressed) return;
            spaceKeyAlreadyPressed = true;
            tractorBeam.GetComponent<TractorBeamController>().Toggle();
        }
        else
        {
            spaceKeyAlreadyPressed = false;
        }
    }

    void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectMetadata objectMetadata;
        if (collision.gameObject.TryGetComponent(out objectMetadata))
        {
            CurrentHealth -= objectMetadata.playerDamage;
            if (CurrentHealth <= 0)
            {
                transform.position = new Vector3(spawnX, spawnY, transform.position.z);
                CurrentHealth = maxHealth;
                tractorBeam.GetComponent<TractorBeamController>().Toggle(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Goal":
                CurrentHealth = maxHealth;
                break;
            default:
                break;
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + CurrentHealth + "%";
    }
}
