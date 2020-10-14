using UnityEngine;


[CreateAssetMenu(fileName = "EnemyStats", menuName = "Create enemy stats container")]
public class EnemyStats : ScriptableObject
{
    [Header("The gold dropped on zombie death.")]
    public int goldOnDeath;

    [Header("The score gained on zombie death.")]
    public int scoreOnDeath;

    [Header("The speed of this lad.")]
    public float speed;

    [Header("The damage this lad does to you.")]
    public int damage;

    [Header("The health of this lad.")]
    public int health;

    [Header("The sprite of this lad.")]
    public Sprite enemySprite;
    
}
