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
        if (isPlayerInRange)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * Speed);
        }
        if (transform.position.x < MinX || transform.position.x > MaxX ||
            transform.position.y < MinY || transform.position.y > MaxY)
        {
            rb.AddForce(-rb.linearVelocity * 2);
        }
        if (rb.linearVelocity.magnitude > Speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * Speed;
        }
    }

    void LateUpdate()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(
            transform.position, AttackRadius, LayerMask.GetMask("Player"));
        isPlayerInRange = playerCollider != null;
    }
}
