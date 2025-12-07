using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public LevelData[] levels;

    LevelData CurrentLevelData { get => levels[currentLevelIndex]; }
    ObjectMetadata[] goalObjectMetadata;
    int currentLevelIndex = 0;

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

    public void AddGoalObject(string objectName)
    {
        LevelData.GoalObjectCount obj = System.Array.Find(
            CurrentLevelData.goalObjects, g => g.gameObject.GetComponent<ObjectMetadata>().objectName == objectName);
        if (obj == null)
        {
            Debug.LogWarning($"Goal object with name {objectName} not found.");
            return;
        }
        obj.currentCount++;
        if (System.Array.TrueForAll(
            CurrentLevelData.goalObjects, g => g.currentCount >= g.count))
        {
            currentLevelIndex = (currentLevelIndex + 1) % levels.Length;
            LoadCurrentLevel();
        }
        UpdateScoreText();
    }

    void LoadCurrentLevel()
    {
        InitializeGoalProgress();
        SpawnObjects(CurrentLevelData);
        SetupPlayer(CurrentLevelData.ufoScale);
    }

    private void InitializeGoalProgress()
    {
        goalObjectMetadata = new ObjectMetadata[CurrentLevelData.goalObjects.Length];
        for (int i = 0; i < CurrentLevelData.goalObjects.Length; i++)
        {
            LevelData.GoalObjectCount obj = CurrentLevelData.goalObjects[i];
            obj.currentCount = 0;
            goalObjectMetadata[i] = obj.gameObject.GetComponent<ObjectMetadata>();
        }
        UpdateScoreText();
    }

    void SetupPlayer(float scale)
    {
        player.transform.localScale = new Vector3(scale, scale, scale);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.CurrentHealth = playerController.maxHealth;
    }

    void SpawnObjects(LevelData levelData)
    {
        foreach (LevelData.ObjectCount entry in levelData.spawnObjects)
        {
            GameObject obj = entry.gameObject;
            int count = entry.count;
            for (int i = 0; i < count; i++)
            {
                Instantiate(obj);
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "";
        for (int i = 0; i < CurrentLevelData.goalObjects.Length; i++)
        {
            LevelData.GoalObjectCount obj = CurrentLevelData.goalObjects[i];
            ObjectMetadata metadata = goalObjectMetadata[i];
            scoreText.text += $"{metadata.humanReadableName}: {obj.currentCount}/{obj.count}\n";
        }
    }
}
