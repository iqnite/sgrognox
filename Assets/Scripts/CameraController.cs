using UnityEngine;

public class CameraController : MonoBehaviour
{
    public UnityEngine.GameObject Player;
    public float SizeRatio;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = Player.transform.position;
        float limitedX = Mathf.Clamp(playerPosition.x, MinX, MaxX);
        float limitedY = Mathf.Clamp(playerPosition.y, MinY, MaxY);
        Vector3 newPosition = new(limitedX, limitedY, transform.position.z);
        transform.position = newPosition;
    }

    void LateUpdate()
    {
        Camera.main.orthographicSize = Player.transform.localScale.x * SizeRatio;
    }
}
