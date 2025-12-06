using System;
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
        foreach (ProgressionData.ObjectParamters objParams in progressionData.objectParamters)
        {
            if (collision.CompareTag(objParams.tag))
            {
                gameManager.AddScore(objParams.score);
                Destroy(collision.gameObject);
                break;
            }
        }
    }
}
