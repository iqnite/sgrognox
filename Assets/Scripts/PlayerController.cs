using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public float thrustForce;
    public float maxSpeed;
    public float rotationAdjustSpeed;

    public GameObject tractorBeam;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    bool spaceKeyAlreadyPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            for (float i = 0; i < 1f; i += 0.2f)
            {
                Invoke(nameof(MakeInvisible), i);
                Invoke(nameof(MakeVisible), i + 0.1f);
            }
        }
    }

    void MakeInvisible()
    {
        spriteRenderer.enabled = false;
    }

    void MakeVisible()
    {
        spriteRenderer.enabled = true;
    }
}