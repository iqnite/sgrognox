using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        float limitedX = Mathf.Clamp(playerPosition.x, minX, maxX);
        float limitedY = Mathf.Clamp(playerPosition.y, minY, maxY);
        Vector3 newPosition = new(limitedX, limitedY, transform.position.z);
        transform.position = newPosition;
    }
}
