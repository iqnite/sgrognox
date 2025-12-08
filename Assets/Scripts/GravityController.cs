using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityController : MonoBehaviour
{
    public GravitySettings GravitySettings;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (GravitySettings == null) return;
        float currentHeight = transform.position.y;
        if (currentHeight >= GravitySettings.SpaceHeight)
        {
            rb.gravityScale = 0f;
        }
        else if (currentHeight > GravitySettings.GroundHeight)
        {
            float normalizedHeight = (currentHeight - GravitySettings.GroundHeight) / (GravitySettings.SpaceHeight - GravitySettings.GroundHeight);
            float minDistanceOffset = 0.1f;
            float distance = normalizedHeight + minDistanceOffset;
            float gravityFactor = 1f / (distance * distance);
            float groundGravity = 1f / (minDistanceOffset * minDistanceOffset);
            rb.gravityScale = GravitySettings.MaxGravityScale * (gravityFactor / groundGravity);
        }
        else
        {
            rb.gravityScale = GravitySettings.MaxGravityScale;
        }
    }
}
