using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityController : MonoBehaviour
{
    public GravitySettings gravitySettings;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (gravitySettings == null) return;

        float currentHeight = transform.position.y;

        if (currentHeight >= gravitySettings.spaceHeight)
        {
            rb.gravityScale = 0f;
        }
        else if (currentHeight > gravitySettings.groundHeight)
        {
            float normalizedHeight = (currentHeight - gravitySettings.groundHeight) / (gravitySettings.spaceHeight - gravitySettings.groundHeight);
            float minDistanceOffset = 0.1f;
            float distance = normalizedHeight + minDistanceOffset;
            float gravityFactor = 1f / (distance * distance);
            float groundGravity = 1f / (minDistanceOffset * minDistanceOffset);
            rb.gravityScale = gravitySettings.maxGravityScale * (gravityFactor / groundGravity);
        }
        else
        {
            rb.gravityScale = gravitySettings.maxGravityScale;
        }
    }
}
