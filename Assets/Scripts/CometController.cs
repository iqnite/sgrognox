using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CometController : MonoBehaviour
{
    public float LeftX;
    public float RightX;
    public float MinSpawnY;
    public float MaxSpawnY;
    public float MinSize;
    public float MaxSize;
    public float MinSpeed;
    public float MaxSpeed;
    public float MaxSpinSpeed;
    public float MaxStartAngle;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float randomAngle = Random.Range(-MaxStartAngle, MaxStartAngle) * Mathf.Sign(Random.Range(-1f, 1f));
        Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad),
                                        Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;

        float spawnX = randomAngle > 0 ? LeftX : RightX;
        float randomY = Random.Range(MinSpawnY, MaxSpawnY);
        transform.position = new Vector3(spawnX, randomY, transform.position.z);

        float randomSize = Random.Range(MinSize, MaxSize);
        transform.localScale = new Vector3(randomSize, randomSize, transform.localScale.z);

        float randomSpeed = Random.Range(MinSpeed, MaxSpeed) / randomSize;
        rb.AddForce(direction * randomSpeed);

        float randomTorque = Random.Range(-MaxSpinSpeed, MaxSpinSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < LeftX - 1f)
            transform.position = new Vector3(RightX + 1f, transform.position.y, transform.position.z);
        else if (transform.position.x > RightX + 1f)
            transform.position = new Vector3(LeftX - 1f, transform.position.y, transform.position.z);
        else return;

        if (transform.position.y < MinSpawnY)
            transform.position = new Vector3(transform.position.x, MaxSpawnY, transform.position.z);
        else if (transform.position.y > MaxSpawnY)
            transform.position = new Vector3(transform.position.x, MinSpawnY, transform.position.z);
        else return;

        rb.totalForce = Vector2.zero;
        rb.linearVelocity = rb.linearVelocity.normalized;
        rb.AddForce(rb.linearVelocity * Random.Range(MinSpeed, MaxSpeed));
    }
}
