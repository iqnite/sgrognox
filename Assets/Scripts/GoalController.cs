using UnityEngine;

public class GoalController : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject TractorBeam;

    GameManager gameManager;
    TractorBeamController tractorBeamController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.GetComponent<GameManager>();
        tractorBeamController = TractorBeam.GetComponent<TractorBeamController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("TractorBeam")) return;
        if (tractorBeamController.CapturedObject == null) return;
        ObjectMetadata objectMetadata;
        if (tractorBeamController.CapturedObject.TryGetComponent(out objectMetadata))
        {
            gameManager.AddGoalObject(objectMetadata.ObjectName);
            Destroy(tractorBeamController.CapturedObject);
            tractorBeamController.CapturedObject = null;
        }
    }
}
