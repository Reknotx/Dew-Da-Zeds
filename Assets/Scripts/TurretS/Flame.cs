using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : Turret
{
    private int tickRate;
    private int tickDamage;

    protected override void Awake()
    {
        base.Awake();

        tickRate = ((FlaminStats)baseStats).TickRate;
        tickDamage = ((FlaminStats)baseStats).TickDamage;
    }

    protected override void Start()
    {
        base.Start();
        timer = new Timer(fireRate, DamageEnemy);
    }

    protected override void Update()
    {
        //UpdateUpgrade();
        base.Update();
        if (GameSystem.Instance.State == GameState.Paused) return;

        if (enemiesInRange.Count > 0 && enemiesInRange[0] != null)
        {
            //transform.parent.LookAt(enemiesInRange[0].transform.position);
            var dir = enemiesInRange[0].transform.position - transform.parent.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            TurretSpriteHolder.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            //timer.Tick(Time.deltaTime);
        }
    }

    public void DamageEnemy()
    {
        if (enemiesInRange.Count <= 0) return;

        if (enemiesInRange[0] != null)
        {
            enemiesInRange[0].IsBurning = true;
            enemiesInRange[0].FlameTickDamage = tickDamage;
            enemiesInRange[0].FlameTickRate = tickRate;
        }

        timer.Reset();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (enemiesInRange.Count > 1) return;

        DamageEnemy();

    }


    public override void Upgrade()
    {
        base.Upgrade();

        FlaminStats flaminStats = (FlaminStats) upgradeTree.GetCurrentStats();

        tickRate = flaminStats.TickRate;
        tickDamage = flaminStats.TickDamage;
        
    }
}
