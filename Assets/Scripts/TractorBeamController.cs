using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class TractorBeamController : MonoBehaviour
{
    public float maxOpacity;
    public float minOpacity;
    public float opacityStep;

    UnityEngine.GameObject capturedObject = null;
    Collider2D beamCollider;
    Material material;
    bool isActive;
    float targetOpacity;
    // Vector3 previousParentPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beamCollider = GetComponent<Collider2D>();
        material = GetComponent<SpriteRenderer>().material;
        // previousParentPosition = transform.parent.position;
        Toggle(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOpacity();
        CheckCollisions();
        UpdateCapturedObject();
        // previousParentPosition = transform.parent.position;
    }

    void CheckCollisions()
    {
        if (!isActive || capturedObject != null) return;
        Collider2D[] touchingColliders = new Collider2D[10];
        _ = beamCollider.Overlap(ContactFilter2D.noFilter, touchingColliders);
        foreach (Collider2D collider in touchingColliders)
        {
            if (collider == null) continue;
            if (collider.CompareTag("Goal")) continue;
            if (collider.bounds.size.x < beamCollider.bounds.size.x
                && collider.bounds.size.y < beamCollider.bounds.size.y)
            {
                capturedObject = collider.gameObject;
            }
        }
        if (capturedObject == null) return;
        Rigidbody2D capturedRb = capturedObject.GetComponent<Rigidbody2D>();
        capturedRb.linearVelocity = Vector2.zero;
        capturedRb.angularVelocity = 0f;
    }

    void UpdateCapturedObject()
    {
        if (capturedObject == null)
        {
            if (isActive && material.color.a == targetOpacity) Toggle(false);
            return;
        }
        if (!isActive)
        {
            // Vector3 velocity = (transform.parent.position - previousParentPosition) / Time.deltaTime;
            // Rigidbody2D capturedRb = capturedObject.GetComponent<Rigidbody2D>();
            // capturedRb.linearVelocity = velocity;
            capturedObject = null;
            return;
        }
        capturedObject.transform.position = transform.position;
    }

    public void Toggle(bool? value = null)
    {
        isActive = value ?? !isActive;
        targetOpacity = isActive ? maxOpacity : minOpacity;
    }

    void UpdateOpacity()
    {
        float currentOpacity = material.color.a;
        float step = opacityStep * Time.deltaTime * 10;
        if (Mathf.Abs(currentOpacity - targetOpacity) < step)
        {
            currentOpacity = targetOpacity;
        }
        else if (currentOpacity < targetOpacity)
        {
            currentOpacity += step;
        }
        else if (currentOpacity > targetOpacity)
        {
            currentOpacity -= step;
        }
        material.color = new Color(1.0f, 1.0f, 1.0f, currentOpacity);
    }
}
