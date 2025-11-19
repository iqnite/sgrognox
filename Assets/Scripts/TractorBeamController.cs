using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class TractorBeamController : MonoBehaviour
{
    public float maxOpacity;
    public float minOpacity;
    public float opacityStep;

    GameObject capturedObject = null;

    Collider2D beamCollider;
    Material material;

    bool isActive;
    float targetOpacity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beamCollider = GetComponent<Collider2D>();
        material = GetComponent<SpriteRenderer>().material;
        Toggle(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOpacity();
        CheckCollisions();
        if (capturedObject == null)
        {
            if (isActive && material.color.a == targetOpacity) Toggle(false);
        }
        UpdateCapturedObject();
    }

    void CheckCollisions()
    {
        if (!isActive || capturedObject != null) return;
        Collider2D[] touchingColliders = new Collider2D[10];
        int count = beamCollider.Overlap(ContactFilter2D.noFilter, touchingColliders);
        foreach (Collider2D collider in touchingColliders)
        {
            if (collider == null) continue;
            if (collider.transform.localScale.magnitude < transform.localScale.magnitude)
            {
                capturedObject = collider.gameObject;
            }
        }
        if (capturedObject == null) return;
        capturedObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        capturedObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    void UpdateCapturedObject()
    {
        if (capturedObject == null) return;
        capturedObject.transform.position = transform.position;
    }

    public void Toggle(bool? value = null)
    {
        isActive = value ?? !isActive;
        if (!isActive)
        {
            capturedObject = null;
        }
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

    public void ReleaseAll()
    {
        capturedObject = null;
        Toggle(false);
    }
}
