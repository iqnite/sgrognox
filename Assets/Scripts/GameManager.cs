using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public LevelData[] levels;

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
        SpawnObjects(CurrentLevel);
    }

    void SetupPlayer(float scale)
    {
        player.transform.localScale = new Vector3(scale, scale, scale);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.currentHealth = playerController.maxHealth;
    }

    void SpawnObjects(LevelData levelData)
    {
        foreach (LevelData.ObjectCount entry in levelData.spawnObjects)
        {
            GameObject obj = entry.obj;
            int count = entry.count;
            for (int i = 0; i < count; i++)
            {
                Instantiate(obj);
            }
        }
    }

    void LateUpdate()
    {
        scoreText.text = "Score: " + score.ToString() + (
            CurrentLevel.requiredScore == 0 ? "" : " / " + CurrentLevel.requiredScore.ToString());
    }
}
