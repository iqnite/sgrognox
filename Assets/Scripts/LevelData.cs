using NUnit.Framework.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public int requiredScore;
    public int asteroidCount;
}
