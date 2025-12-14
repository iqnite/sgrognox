using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class TractorBeamController : MonoBehaviour
{
    public GameObject Goal;
    public GameObject GuideArrow;
    public float MaxOpacity;
    public float MinOpacity;
    public float OpacityStep;

    [HideInInspector]
    public GameObject CapturedObject = null;
    [HideInInspector]
    public bool IsActive;

    Collider2D beamCollider;
    Material material;
    GuideArrowController guideArrowController;
    float targetOpacity;
    // Vector3 previousParentPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beamCollider = GetComponent<Collider2D>();
        material = GetComponent<SpriteRenderer>().material;
        guideArrowController = GuideArrow.GetComponent<GuideArrowController>();
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
        if (!IsActive || CapturedObject != null) return;
        Collider2D[] touchingColliders = new Collider2D[10];
        _ = beamCollider.Overlap(ContactFilter2D.noFilter, touchingColliders);
        foreach (Collider2D collider in touchingColliders)
        {
            if (collider == null) continue;
            if (collider.CompareTag("Goal")) continue;
            if (collider.bounds.size.x < beamCollider.bounds.size.x
                && collider.bounds.size.y < beamCollider.bounds.size.y)
            {
                CapturedObject = collider.gameObject;
            }
        }
        if (CapturedObject == null) return;
        Rigidbody2D capturedRb = CapturedObject.GetComponent<Rigidbody2D>();
        capturedRb.linearVelocity = Vector2.zero;
        capturedRb.angularVelocity = 0f;
        guideArrowController.PointAt(Goal);
    }

    void UpdateCapturedObject()
    {
        if (CapturedObject == null)
        {
            if (IsActive && material.color.a == targetOpacity) Toggle(false);
            guideArrowController.ClearTarget();
            return;
        }
        if (!IsActive)
        {
            // Vector3 velocity = (transform.parent.position - previousParentPosition) / Time.deltaTime;
            // Rigidbody2D capturedRb = capturedObject.GetComponent<Rigidbody2D>();
            // capturedRb.linearVelocity = velocity;
            CapturedObject = null;
            guideArrowController.ClearTarget();
            return;
        }
        CapturedObject.transform.position = transform.position;
    }

    public void Toggle(bool? value = null)
    {
        IsActive = value ?? !IsActive;
        beamCollider.enabled = IsActive;
        targetOpacity = IsActive ? MaxOpacity : MinOpacity;
    }

    void UpdateOpacity()
    {
        float currentOpacity = material.color.a;
        float step = OpacityStep * Time.deltaTime * 10;
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
