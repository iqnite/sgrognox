using UnityEngine;

public class GoalController : MonoBehaviour
{
    public ProgressionData progressionData;
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
        if (collision.CompareTag("Asteroid"))
        {
            gameManager.score += progressionData.scorePerAsteroid;
            Destroy(collision.gameObject);
        }
    }
}
