using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelData[] levels;
    public GameObject asteroid;
    public TextMeshProUGUI scoreText;

    public int currentLevelIndex = 0;
    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int levelIndex)
    {
        LevelData levelToLoad = levels[levelIndex];
        SpawnAsteroids(levelToLoad.asteroidCount);
    }

    void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(asteroid);
        }
    }

    void LateUpdate()
    {
        scoreText.text = "Score: " + score;
    }
}
