using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GravityController))]
public class JetController : MonoBehaviour
{
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float Speed;
    public float AttackRadius;

    Rigidbody2D rb;
    GravityController gravityController;
    GameObject player;
    bool isPlayerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityController = GetComponent<GravityController>();
        player = GameObject.FindWithTag("Player");
        gravityController.IsEnabled = false;
        isPlayerInRange = false;
        float randomX = Random.Range(MinX, MaxX);
        float randomY = Random.Range(MinY, MaxY);
        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float clampedX = Mathf.Clamp(transform.position.x, MinX, MaxX);
        float clampedY = Mathf.Clamp(transform.position.y, MinY, MaxY);
        if (clampedX != transform.position.x || clampedY != transform.position.y)
        {
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
        if (isPlayerInRange)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * Speed);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, Speed);
    }

    void LateUpdate()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(
            transform.position, AttackRadius, LayerMask.GetMask("Player"));
        isPlayerInRange = playerCollider != null;
    }
}
