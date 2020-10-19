using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Firing : Turret
{
    //public Enemy target;

    /// <summary>
    /// The timer that executes the firing function.
    /// </summary>
    private Timer timer;
    //private float range = 2f;

    protected override void Start()
    {
        base.Start();
        timer = new Timer(fireRate, DamageEnemy);
    }

    private void Update()
    {
        UpdateUpgrade();
        if (GameSystem.Instance.State == GameState.Paused) return;

        if (enemiesInRange.Count > 0 && enemiesInRange[0] != null)
        {
            //transform.parent.LookAt(enemiesInRange[0].transform.position);
            var dir = enemiesInRange[0].transform.position - transform.parent.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.parent.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            timer.Tick(Time.deltaTime);
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

    //private void OnDrawGizmosSelected()
    //{
    //    if (baseStats == null) return;

    //    Gizmos.color = Color.red;

    //    //Gizmos.DrawWireSphere(transform.position, radius);

    //    CircleCollider2D c2d = GetComponent<CircleCollider2D>();
    //    if (c2d != null)
    //    {
    //        float newRadius = baseStats.range;

    //        c2d.radius = newRadius;
    //        Handles.color = new Color(0, 1, 0, .1f);
    //        Vector3 center = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
    //        Handles.DrawSolidDisc(center, Vector3.forward, newRadius);
    //    }
    //}
}
