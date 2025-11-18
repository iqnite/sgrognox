using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    public float minSize;
    public float maxSize;
    public float minSpeed;
    public float maxSpeed;
    public float maxSpinSpeed;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Random.insideUnitCircle;
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        rb.AddForce(randomDirection * randomSpeed);

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
