using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Serializable]
    public class ObjectCount
    {
        public GameObject obj;
        public int count;
    }

    public int levelNumber;
    public int requiredScore;
    public float ufoScale;
    public ObjectCount[] spawnObjects;
}
