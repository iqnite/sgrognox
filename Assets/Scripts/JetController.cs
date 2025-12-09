using UnityEngine;

[RequireComponent(typeof(GravityController))]
public class JetController : MonoBehaviour
{
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float Speed;

    GravityController gravityController;
    GameObject player;
    bool isPlayerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityController = GetComponent<GravityController>();
        player = GameObject.FindWithTag("Player");
        gravityController.IsEnabled = false;
        isPlayerInRange = false;
        float randomX = Random.Range(MinX, MaxX);
        float randomY = Random.Range(MinY, MaxY);
        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            isPlayerInRange = false;
        }
    }
}
