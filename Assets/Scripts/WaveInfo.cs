using UnityEngine;

[CreateAssetMenu(fileName = "Wave info", menuName = "Create new wave info")]
public class WaveInfo : ScriptableObject
{
    [Range(0, 100)]
    public int basicZeds;

    [Range(0, 100)]
    public int bigZeds;

    [Range(0, 100)]
    public int fastZeds;
}
