using UnityEngine;

[CreateAssetMenu(fileName = "ProgressionData", menuName = "Scriptable Objects/ProgressionData")]
public class ProgressionData : ScriptableObject
{
    [System.Serializable]
    public class ObjectParamters
    {
        public string tag;
        public int score;
        public int damage;
    }

    public ObjectParamters[] objectParamters;
}
