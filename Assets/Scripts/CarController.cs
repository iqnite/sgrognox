using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CarController : MonoBehaviour
{
    public float[] Lanes;
    public float LaneY;
    public float LeftX;
    public float RightX;
    public float Speed;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    int direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Car");
        direction = Random.Range(0, 2) * 2 - 1; // -1 or 1
        transform.position = new Vector3(
            Random.Range(LeftX, RightX),
            LaneY,
            Lanes[Random.Range(0, Lanes.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0) return;
        spriteRenderer.flipX = direction == -1;
        rb.AddForce(rb.position + Speed * direction * Time.deltaTime * Vector2.right);
        if ((direction == 1 && transform.position.x > RightX)
            || (direction == -1 && transform.position.x < LeftX))
        {
            transform.position = new Vector3(
                direction == 1 ? LeftX : RightX,
                transform.position.y,
                transform.position.z);
        }
    }
}
