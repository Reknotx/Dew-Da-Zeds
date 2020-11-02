using UnityEngine;

[CreateAssetMenu(fileName = "FrostbiteStats.asset", menuName = "Turret Stats/Frostbite")]
public class FrostbiteStats : TurretStats
{

    [Space]
    [Header("The duration that zombies stay frozen for.")]
    /// <summary> The length of time this turret freezes zombies. </summary>
    public int freeezeDuration;

    [Space]
    [Header("The number of zombies frozen on turret firing.")]
    /// <summary> The number of zombies that will be frozen at one time. </summary>
    public int numZombiesToFreeze;

}
