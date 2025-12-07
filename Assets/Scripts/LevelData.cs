using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Serializable]
    public class ObjectCount
    {
        public GameObject gameObject;
        public int count;
    }

    [Serializable]
    public class GoalObjectCount : ObjectCount
    {
        public int currentCount;
    }

    public int levelNumber;
    public float ufoScale;
    public ObjectCount[] spawnObjects;
    public GoalObjectCount[] goalObjects;
}
