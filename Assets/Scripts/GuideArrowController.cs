using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GuideArrowController : MonoBehaviour
{
    public Vector2 BorderOffset;

    [HideInInspector]
    public Transform Target;

    SpriteRenderer spriteRenderer;
    bool isCollidingWithTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Target = gameObject.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        isCollidingWithTarget = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (Target == gameObject.transform)
        {
            spriteRenderer.enabled = false;
            return;
        }
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(Target.position);
        Vector3 maxScreenPos = new(Screen.width, Screen.height, 0);
        Vector3 clampedPos = new(
            Mathf.Clamp(targetScreenPos.x, BorderOffset.x, maxScreenPos.x - BorderOffset.x),
            Mathf.Clamp(targetScreenPos.y, BorderOffset.y, maxScreenPos.y - BorderOffset.y),
            targetScreenPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(clampedPos);
        Vector2 direction = Target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        spriteRenderer.enabled = !isCollidingWithTarget;
    }

    public void PointAt(GameObject target)
    {
        PointAt(target.transform);
    }

    public void PointAt(Transform target)
    {
        Target = target;
    }

    public void ClearTarget()
    {
        Target = gameObject.transform;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform == Target)
            isCollidingWithTarget = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform == Target)
            isCollidingWithTarget = false;
    }
}
