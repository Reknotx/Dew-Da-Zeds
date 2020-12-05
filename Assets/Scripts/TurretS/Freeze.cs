using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Turret
{
    private int freezeDuration;
    private int numberOfTargets;

    protected override void Awake()
    {
        base.Awake();

        FrostbiteStats frostStats = (FrostbiteStats)baseStats;
        freezeDuration = frostStats.freeezeDuration;
        numberOfTargets = frostStats.numZombiesToFreeze;
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timer = new Timer(fireRate, Freezey);
    }

    private void Freezey()
    {
        if (enemiesInRange.Count == 0) return;

        int enemiesFrozenCount = 0;

        foreach (Enemy enemy in enemiesInRange)
        {
            if (enemiesFrozenCount == 2) break;

            if (enemy.IsFrozen == false)
            {
                enemiesFrozenCount++;
                enemy.IsFrozen = true;
            }
        }

        timer.Reset();
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (enemiesInRange.Count > 1) return;

        Freezey();

    }

    public override void Upgrade()
    {
        base.Upgrade();

        FrostbiteStats frostStats = (FrostbiteStats) upgradeTree.GetCurrentStats();

        freezeDuration = frostStats.freeezeDuration;
        numberOfTargets = frostStats.numZombiesToFreeze;
    }

}
