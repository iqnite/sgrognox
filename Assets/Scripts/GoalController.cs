using UnityEngine;

public class GoalController : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectMetadata objectMetadata;
        if (collision.gameObject.TryGetComponent(out objectMetadata))
        {
            gameManager.AddGoalObject(objectMetadata.objectName);
            Destroy(collision.gameObject);
        }
    }
}
