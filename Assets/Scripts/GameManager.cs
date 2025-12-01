using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelData[] levels;
    public GameObject player;
    public GameObject asteroid;
    public TextMeshProUGUI scoreText;

    LevelData CurrentLevel { get => levels[currentLevelIndex]; }
    int currentLevelIndex = 0;
    int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadCurrentLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (CurrentLevel.requiredScore > 0 && score >= CurrentLevel.requiredScore)
        {
            currentLevelIndex = (currentLevelIndex + 1) % levels.Length;
            LoadCurrentLevel();
        }
    }

    void LoadCurrentLevel()
    {
        SetupPlayer(CurrentLevel.ufoScale);
        SpawnAsteroids(CurrentLevel.asteroidCount);
    }

    void SetupPlayer(float scale)
    {
        player.transform.localScale = new Vector3(scale, scale, scale);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.currentHealth = playerController.maxHealth;
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
