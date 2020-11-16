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

    public void OnCollisionEnter(Collision collision)
    {
        foreach (Enemy zomb in enemiesStackedOnBarrier)
        {
            if (zomb == collision.gameObject.GetComponent<Enemy>())
            {
                return;
            }
            else
            {
                enemiesStackedOnBarrier.Add(zomb);
            }

            zomb.touchingBarrier = true;
        }

        if (enemiesStackedOnBarrier.Count >= strength)
        {
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
