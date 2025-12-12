using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    public float Speed;
    public int MaxLifetime;
    public Vector3 RotationOffset;
    public Vector3 StartDirection;

    Rigidbody2D rb;
    int lifetime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifetime = MaxLifetime;
        Vector3 direction = StartDirection + RotationOffset;
        transform.rotation = Quaternion.Euler(direction);
        rb.AddForce(direction * Speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= 1;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
