using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : Turret
{
    //public Enemy target;

    /// <summary>
    /// The timer that executes the firing function.
    /// </summary>
    //private float range = 2f;

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

    /// <summary> Damages the currently targetted enemy. </summary>
    public void DamageEnemy()
    {
        if (enemiesInRange.Count <= 0) return;

        if (enemiesInRange[0] != null) enemiesInRange[0].TakeDamage(damage); 

        timer.Reset();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (enemiesInRange.Count > 1) return;

        DamageEnemy();

    }
}
