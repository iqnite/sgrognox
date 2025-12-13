using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
    public float MinSpawnX;
    public float MaxSpawnX;
    public float MinSpawnY;
    public float MaxSpawnY;
    public float MinSize;
    public float MaxSize;
    public float MinSpeed;
    public float MaxSpeed;
    public float MaxSpinSpeed;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomX = Random.Range(MinSpawnX, MaxSpawnX);
        float randomY = Random.Range(MinSpawnY, MaxSpawnY);
        transform.position = new Vector3(randomX, randomY, transform.position.z);

        float randomSize = Random.Range(MinSize, MaxSize);
        transform.localScale = new Vector3(randomSize, randomSize, transform.localScale.z);

        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Random.insideUnitCircle;
        float randomSpeed = Random.Range(MinSpeed, MaxSpeed) / randomSize;
        rb.AddForce(randomDirection * randomSpeed);

        float randomTorque = Random.Range(-MaxSpinSpeed, MaxSpinSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
