using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health = 10;

    public EnemyStats stats;

    [HideInInspector] public WayPoint dest;

    [HideInInspector] public bool atEnd = false;

    private int _goldDropOnDeath;

    private int _scoreOnDeath;

    public int Health
    {
        get { return _health; }
    }

    public float Speed { get; set; }
    public int Power { get; set; }

    private bool _isFrozen = false;
    public bool IsFrozen
    {
        get
        {
            return _isFrozen;
        }
        set
        {
            if (value == true)
            {
                //Speed = 0f;
                _isFrozen = true;
                StartCoroutine(Frozen());
            }
            else
            {
                //Speed = stats.speed;
                _isFrozen = false;
            }
        }
    }

    //public Transform destination;

    //public bool resetTime = true;
    //public float timeStart;
    //public float timeDuration = 1f;


    private void Start()
    {
        _health = stats.health;

        Speed = stats.speed;
        Power = stats.damage;
        _goldDropOnDeath = stats.goldOnDeath;
        _scoreOnDeath = stats.scoreOnDeath;

        if (stats.enemySprite != null)
            GetComponent<SpriteRenderer>().sprite = stats.enemySprite;
    }


    void Update()
    {
        //if (resetTime)
        //{
        //    timeStart = Time.time;
        //}
        if (GameSystem.Instance.State == GameState.Paused) return;
        if (!atEnd) Move();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            PlayerStats.Instance.Gold += _goldDropOnDeath;
            GameSystem.Instance.ZombiesRemaining--;
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        if (IsFrozen) return;

        transform.position = Vector2.MoveTowards(transform.position, dest.transform.position, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, dest.transform.position) <= 0.01f)
        {
            if (dest.name == "End Point" || dest.Link.Count == 0)
            {
                atEnd = true;
                DealDamageToPlayer();
                Destroy(this.gameObject);
                return;
            }

            if (dest.Link[0] != null) dest = dest.Link[Random.Range(0, dest.Link.Count)];
        }
    }

    void DealDamageToPlayer()
    {
        PlayerStats.Instance.Lives -= Power;
        GameSystem.Instance.ZombiesRemaining--;
    }

    IEnumerator Frozen()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitForFixedUpdate();

        IsFrozen = false;
    }
}
