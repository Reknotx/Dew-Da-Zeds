using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public List<Enemy> enemiesStackedOnBarrier = new List<Enemy>();

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
        }
    }
}
