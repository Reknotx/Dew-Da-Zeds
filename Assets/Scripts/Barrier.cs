using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public List<Enemy> enemiesStackedOnBarrier = new List<Enemy>();

    public int Cost { get; } = 20;

    [Space]
    [Header("How many zombies need to be stacked on the barrier to break it.")]
    public int strength;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("Detected collision with: " + collision.gameObject.name);

        if (collision.gameObject.layer != 11) return;

        if (enemiesStackedOnBarrier.Contains(collision.gameObject.GetComponent<Enemy>()))
            return;
        else
            enemiesStackedOnBarrier.Add(collision.gameObject.GetComponent<Enemy>());

        if (enemiesStackedOnBarrier.Count >= strength)
        {
            foreach (Enemy zomb in enemiesStackedOnBarrier)
            {
                Debug.Log(zomb.name);
            }

            print("destroying barrier");
            ResetZombs();
            Destroy(gameObject);
        }
    }

    private void ResetZombs()
    {
        foreach (Enemy zomb in enemiesStackedOnBarrier)
        {
            if (zomb != null)
            {
                zomb.touchingBarrier = false;
            }
        }
    }
}
