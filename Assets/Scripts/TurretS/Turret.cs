using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Turret : MonoBehaviour
{
    /// <summary>  The damage this turret deals to enemies. </summary>
    protected int damage = 0;

    /// <summary> The rate at which this turret damages enemies. </summary>
    protected float fireRate = 0;

    /// <summary> The money required to buy this turret. </summary>
    public int Cost { get; set; } = 15;

    ///// <summary> This turret's current level in the game. </summary>
    //private int currLevel = 0;

    /// <summary> The list representing how many enemies are within firing range. </summary>
    public List<Enemy> enemiesInRange = new List<Enemy>();

    public GameObject TurretSpriteHolder;

    private int _totalCost;

    [Header("Turret stats and upgrades.")]
    
    
    [SerializeField] protected TurretStats baseStats;

    /// <summary> The stats of this turret at level 1. </summary>
    [SerializeField] protected TurretStats Level1;

    /// <summary> The stats of this turret at level 2. </summary>
    [SerializeField] protected TurretStats Level2;

    /// <summary> The stats of this turret at level 3. </summary>
    [SerializeField] protected TurretStats Level3;
    [Space]
    public GameObject upgradeCanvas;

    public Button upgradeButton;

    /// <summary>
    /// 
    /// </summary>
    private TurretStats currentStats;

    private UpgradeTree upgradeTree;

    protected Timer timer;


    private void Awake()
    {
        fireRate = baseStats.fireRate;
        damage = baseStats.damage;
        Cost = baseStats.cost;
        currentStats = baseStats;

        //if (baseStats.turretSprite != null) transform.parent.GetComponent<SpriteRenderer>().sprite = baseStats.turretSprite;

        _totalCost = baseStats.cost;
        upgradeCanvas.SetActive(false);
    }

    protected virtual void Start()
    { 

        List<TurretStats> temp = new List<TurretStats>();

        temp.Add(baseStats);
        temp.Add(Level1);
        temp.Add(Level2);
        temp.Add(Level3);

        upgradeTree = new UpgradeTree(temp);
    }

    protected virtual void Update()
    {
        UpdateUpgrade();

        if (GameSystem.Instance.State == GameState.Paused) return;

        if (enemiesInRange.Count > 0 && enemiesInRange[0] != null)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    /// <summary> Updates the upgrade button to represent the current cost of buying. </summary>
    public void UpdateUpgrade()
    {
        if (upgradeCanvas.activeSelf == false) return;

        if (upgradeTree.MaxLevel()) return;

        upgradeButton.GetComponentInChildren<Text>().text = "Upgrade " + upgradeTree.CostOfNextUpgrade() + " gold";

        if (PlayerStats.Instance.Gold >= upgradeTree.CostOfNextUpgrade())
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (this.enabled == false) return;

        if (other.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("Enemy in range");
            enemiesInRange.Add(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (this.enabled == false) return;

        if (other.gameObject.GetComponent<Enemy>())
        {
            //Debug.Log("Enemy left range.");
            enemiesInRange.Remove(other.gameObject.GetComponent<Enemy>());
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (baseStats == null) return;

        Gizmos.color = Color.red;

        //Gizmos.DrawWireSphere(transform.position, radius);

        CircleCollider2D c2d = GetComponent<CircleCollider2D>();
        if (c2d != null)
        {
            float newRadius = baseStats.range;

            c2d.radius = newRadius;
            Handles.color = new Color(0, 1, 0, .1f);
            Vector3 center = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
            Handles.DrawSolidDisc(center, Vector3.forward, newRadius);
        }
    }
#endif

    #region Upgrades and Selling
    /// <summary> Upgrades the turret to the next level. </summary>
    public void Upgrade()
    {
        TurretStats temp = upgradeTree.UpgradeTurret();

        damage = temp.damage;
        fireRate = temp.fireRate;
        PlayerStats.Instance.Gold -= temp.cost;
        _totalCost += temp.cost;
    }

    /// <summary> Downgrades the turret to the previous level. </summary>
    public void Downgrade()
    {
        TurretStats temp = upgradeTree.DowngradeTurret();

        damage = temp.damage;
        fireRate = temp.fireRate;
        //PlayerStats.Instance.Gold += temp.sellCost;
    }

    /// <summary> Sells the turret, refunding the player their gold. </summary>
    public void SellTurret()
    {
        PlayerStats.Instance.Gold += _totalCost;
        Destroy(transform.parent.gameObject);
    }

    /// <summary>
    /// Private class that is unique to the turret class for the storing
    /// of upgrades. Provides functions that assist in upgrading and 
    /// downgrading turrets.
    /// </summary>
    private class UpgradeTree
    {
        List<Node> upgradeList;
        int currLevel = 0;

        public UpgradeTree()
        {
            upgradeList = new List<Node>();
        }

        /// <summary>
        /// Constructor for UpgradeTree that's passed an array of TurretStats.
        /// </summary>
        /// <param name="stats">The List of turret stats in order of buying.</param>
        public UpgradeTree(List<TurretStats> stats)
        {
            upgradeList = new List<Node>();

            foreach (TurretStats stat in stats)
            {
                Node temp = new Node(stat);
                upgradeList.Add(temp);
            }
        }

        /// <summary>
        /// Adds a new upgrade to the upgrade tree.
        /// </summary>
        /// <param name="stats">The stats to add to the tree.</param>
        public void AddNewUpgrade(TurretStats stats)
        {
            Node temp = new Node(stats);

            upgradeList.Add(temp);

        }

        /// <summary>
        /// Obtains the stats for the next level of this turret.
        /// </summary>
        /// <returns>A reference to the scriptable object holding the stats for the next level.</returns>
        public TurretStats UpgradeTurret()
        {
            if (currLevel >= upgradeList.Count) return null;

            currLevel++;

            TurretStats result = null;

            if (upgradeList[currLevel].GetStats().Level != currLevel)
            {
                foreach (Node stats in upgradeList)
                {
                    if (stats.GetStats().Level == currLevel)
                    {
                        result = stats.GetStats();
                        break;
                    }
                }
            }
            else
            {
                result = upgradeList[currLevel].GetStats();
            }

            return result;
        }
        
        /// <summary>
        /// Obtains the stats for the previous level of this turret.
        /// </summary>
        /// <returns>A reference to the scriptable object holding the stats for the previous level.</returns>
        public TurretStats DowngradeTurret()
        {
            if (currLevel == 0) return null;

            currLevel--;

            TurretStats result = null;

            if (upgradeList[currLevel].GetStats().Level != currLevel)
            {
                foreach (Node stats in upgradeList)
                {
                    if (stats.GetStats().Level == currLevel)
                    {
                        result = stats.GetStats();
                        break;
                    }
                }
            }
            else
            {
                result = upgradeList[currLevel].GetStats();
            }

            return result;
        }

        /// <summary>
        /// Returns the cost of the next Upgrade in the list.
        /// </summary>
        /// <returns></returns>
        public int CostOfNextUpgrade()
        {
            int level = currLevel + 1;
            return upgradeList[level].GetStats().cost;
        }

        /// <summary>
        /// Checks to see if the turret is at max level.
        /// </summary>
        /// <returns>True if the upgrade is at max level, false otherwise.</returns>
        public bool MaxLevel()
        {
            if (currLevel >= upgradeList.Count) return true;
            else return false;
        }

        private class Node
        {
            TurretStats Stats { get; set; }
            TurretStats Previous { get; set; }
            TurretStats Next { get; set; }

            public Node(TurretStats stats)
            {
                Stats = stats;
            }

            public TurretStats GetStats()
            {
                return Stats;
            }
        }

    }
    #endregion
}
