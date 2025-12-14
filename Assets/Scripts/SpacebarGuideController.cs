using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpacebarGuideController : MonoBehaviour
{
    public GameObject TractorBeam;

    SpriteRenderer spriteRenderer;
    TractorBeamController tractorBeamController;
    bool isDismissed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tractorBeamController = TractorBeam.GetComponent<TractorBeamController>();
        spriteRenderer.enabled = false;
        isDismissed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDismissed) return;
        if (tractorBeamController.IsActive)
        {
            isDismissed = true;
            spriteRenderer.enabled = false;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDismissed) return;
        if (collider.CompareTag("Player") || collider.CompareTag("Goal")) return;
        spriteRenderer.enabled = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (isDismissed) return;
        if (collider.CompareTag("Player")) return;
        spriteRenderer.enabled = false;
    }
}
