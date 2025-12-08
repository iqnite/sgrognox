using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Serializable]
    public class ObjectCount
    {
        public GameObject GameObject;
        public int Count;
    }

    [Serializable]
    public class GoalObjectCount : ObjectCount
    {
        public int CurrentCount;
    }

    public int LevelNumber;
    public float UfoScale;
    public ObjectCount[] SpawnObjects;
    public GoalObjectCount[] GoalObjects;
}
