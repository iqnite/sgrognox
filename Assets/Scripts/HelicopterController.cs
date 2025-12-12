using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GravityController))]
public class HelicopterController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float Speed;
    public float AttackRadius;
    public int MinShootCooldown;
    public int MaxShootCooldown;
    public float SeparationDistance;
    public float SeparationForce;

    Rigidbody2D rb;
    GravityController gravityController;
    GameObject player;
    float nextShootTime;
    bool isPlayerInRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityController = GetComponent<GravityController>();
        player = GameObject.FindWithTag("Player");
        gravityController.IsEnabled = false;
        isPlayerInRange = false;
        nextShootTime = Time.time + Random.Range(MinShootCooldown, MaxShootCooldown);
        float randomX = Random.Range(MinX, MaxX);
        float randomY = Random.Range(MinY, MaxY);
        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        ClampPosition();
        SeparateHelicopters();
        if (isPlayerInRange)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * Speed);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, Speed);

        // Check if it's time to shoot
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + Random.Range(MinShootCooldown, MaxShootCooldown);
        }
    }

    void ClampPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, MinX, MaxX);
        float clampedY = Mathf.Clamp(transform.position.y, MinY, MaxY);
        if (clampedX != transform.position.x || clampedY != transform.position.y)
        {
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    void Shoot()
    {
        if (!isPlayerInRange) return;
        Vector3 shootDirection = (player.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.StartDirection = shootDirection;
    }

    void SeparateHelicopters()
    {
        Collider2D[] nearbyHelicopters = Physics2D.OverlapCircleAll(
            transform.position, SeparationDistance, LayerMask.GetMask("Helicopter"));
        Vector2 separationVector = Vector2.zero;
        int count = 0;
        foreach (Collider2D other in nearbyHelicopters)
        {
            if (other.gameObject == gameObject) continue;
            Vector2 directionAway = transform.position - other.transform.position;
            float distance = directionAway.magnitude;
            if (distance > 0 && distance < SeparationDistance)
            {
                separationVector += directionAway.normalized / distance;
                count++;
            }
        }
        if (count > 0)
        {
            separationVector /= count;
            rb.AddForce(separationVector * SeparationForce);
        }
    }

    void LateUpdate()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(
            transform.position, AttackRadius, LayerMask.GetMask("Player"));
        isPlayerInRange = playerCollider != null;
    }
}
