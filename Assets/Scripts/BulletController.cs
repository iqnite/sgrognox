using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    public float Speed;
    public int MaxLifetime;

    [HideInInspector]
    public Vector3 StartDirection;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, MaxLifetime);
        Vector3 direction = StartDirection.normalized;
        transform.rotation = Quaternion.Euler(direction);
        rb.AddForce(direction * Speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
