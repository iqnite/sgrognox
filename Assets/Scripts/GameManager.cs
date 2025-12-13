using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI FinishText;
    public LevelData[] Levels;

    LevelData CurrentLevelData => Levels[currentLevelIndex];
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
            CurrentLevelData.GoalObjects, g => g.GameObject.GetComponent<ObjectMetadata>().ObjectName == objectName);
        if (obj == null)
        {
            return;
        }
        obj.CurrentCount++;
        if (System.Array.TrueForAll(
            CurrentLevelData.GoalObjects, g => g.CurrentCount >= g.Count))
        {
            NextLevel();
        }
        UpdateScoreText();
    }

    void NextLevel()
    {
        currentLevelIndex++;
        LoadCurrentLevel();
        if (currentLevelIndex >= Levels.Length - 1)
        {
            FinishText.gameObject.SetActive(true);
        }
    }

    void LoadCurrentLevel()
    {
        InitializeGoalProgress();
        SpawnObjects(CurrentLevelData);
        SetupPlayer(CurrentLevelData.UfoScale);
    }

    void InitializeGoalProgress()
    {
        goalObjectMetadata = new ObjectMetadata[CurrentLevelData.GoalObjects.Length];
        for (int i = 0; i < CurrentLevelData.GoalObjects.Length; i++)
        {
            LevelData.GoalObjectCount obj = CurrentLevelData.GoalObjects[i];
            obj.CurrentCount = 0;
            goalObjectMetadata[i] = obj.GameObject.GetComponent<ObjectMetadata>();
        }
        UpdateScoreText();
    }

    void SetupPlayer(float scale)
    {
        Player.transform.localScale = new Vector3(scale, scale, scale);
        PlayerController playerController = Player.GetComponent<PlayerController>();
        playerController.CurrentHealth = playerController.MaxHealth;
    }

    void SpawnObjects(LevelData levelData)
    {
        foreach (LevelData.ObjectCount entry in levelData.SpawnObjects)
        {
            GameObject obj = entry.GameObject;
            int count = entry.Count;
            for (int i = 0; i < count; i++)
            {
                Instantiate(obj);
            }
        }
    }

    void UpdateScoreText()
    {
        ScoreText.text = "Level " + CurrentLevelData.LevelName + "\n";
        for (int i = 0; i < CurrentLevelData.GoalObjects.Length; i++)
        {
            LevelData.GoalObjectCount obj = CurrentLevelData.GoalObjects[i];
            ObjectMetadata metadata = goalObjectMetadata[i];
            ScoreText.text += $"{metadata.HumanReadableName}: {obj.CurrentCount}/{obj.Count}\n";
        }
    }
}
