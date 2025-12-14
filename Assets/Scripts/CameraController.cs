using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float SizeRatio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 newPosition = new(playerPosition.x, playerPosition.y, transform.position.z);
        transform.position = newPosition;
        Camera.main.orthographicSize = Player.transform.localScale.x * SizeRatio;
    }
}
