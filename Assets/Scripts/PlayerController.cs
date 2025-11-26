using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public float thrustForce;
    public float maxSpeed;
    public float rotationAdjustSpeed;
    public int maxHealth;
    public int currentHealth;

    public GameObject tractorBeam;
    public ProgressionData progressionData;
    public TextMeshProUGUI healthText;

    Rigidbody2D rb;

    float spawnX;
    float spawnY;
    bool spaceKeyAlreadyPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        spawnX = transform.position.x;
        spawnY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ToggleTractorBeam();
        LimitSpeed();
        AdjustRotation();
    }

    void Move()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            rb.AddForce(Vector2.up * thrustForce);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            rb.AddForce(Vector2.down * thrustForce);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            rb.AddForce(Vector2.left * thrustForce);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            rb.AddForce(Vector2.right * thrustForce);
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

    void AdjustRotation()
    {
        float currentRotation = rb.rotation;
        float targetRotation = Mathf.MoveTowardsAngle(currentRotation, 0f, rotationAdjustSpeed * Time.deltaTime);
        rb.MoveRotation(targetRotation);
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
        switch (collision.gameObject.tag)
        {
            case "Asteroid":
                currentHealth -= progressionData.damagePerAsteroid;
                break;
            default:
                break;
        }

        if (currentHealth <= 0)
        {
            transform.position = new Vector3(spawnX, spawnY, transform.position.z);
            currentHealth = maxHealth;
            tractorBeam.GetComponent<TractorBeamController>().ReleaseAll();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Goal":
                currentHealth = maxHealth;
                break;
            default:
                break;
        }
    }

    void LateUpdate()
    {
        healthText.text = "Health: " + currentHealth + "%";
    }
}
