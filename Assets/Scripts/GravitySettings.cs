using UnityEngine;

[CreateAssetMenu(fileName = "GravitySettings", menuName = "Scriptable Objects/GravitySettings")]
public class GravitySettings : ScriptableObject
{
    [Tooltip("Height above which gravity becomes zero (space)")]
    public float spaceHeight;

    [Tooltip("Height where gravity is at full Earth strength")]
    public float groundHeight;

    [Tooltip("Maximum gravity scale at ground level")]
    public float maxGravityScale;
}
