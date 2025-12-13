#nullable enable
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GuideArrowController : MonoBehaviour
{
    public Vector2 BorderOffset;

    [HideInInspector]
    public Transform? Target;

    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (Target == null)
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
        spriteRenderer.enabled = true;
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
        Target = null;
    }
}
