using UnityEngine;

public class TurretStats : ScriptableObject
{
    /// <summary> The rate at which this turret fires. </summary>
    public float fireRate;

    /// <summary> The damage this turret deals </summary>
    public int damage;

    /// <summary> The cost to buy this turret. </summary>
    public int cost;

    [Range(0.5f, 4f)]
    public float range;

    /// <summary> Represents what level the turret is. </summary>
    public int Level;

    public Sprite turretSprite;
    public Sprite baseSprite;

}
