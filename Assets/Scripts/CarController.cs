using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CarController : MonoBehaviour
{
    public float[] lanes;
    public float laneY;
    public float leftX;
    public float rightX;
    public float speed;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    int direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Car");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Random.Range(0, 2) * 2 - 1; // -1 or 1
        transform.position = new Vector3(
            Random.Range(leftX, rightX),
            laneY,
            lanes[Random.Range(0, lanes.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0) return;
        spriteRenderer.flipX = direction == -1;
        rb.AddForce(rb.position + speed * direction * Time.deltaTime * Vector2.right);
        if ((direction == 1 && transform.position.x > rightX)
            || (direction == -1 && transform.position.x < leftX))
        {
            transform.position = new Vector3(
                direction == 1 ? leftX : rightX,
                transform.position.y,
                transform.position.z);
        }
    }
}
