using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : WayPoint
{

    [Range(1f, 4f)]
    [Header("Indicates the spawn rate of zombies.")]
    ///<summary> Determines how fast zombies spawn. Lower numbers indicate
    ///faster spawn rates.</summary>
    public float spawnRate = 1f;

    public static ZombieSpawner Instance;

    public List<Enemy> spawnables = new List<Enemy>();

    public GameObject BasicZed;
    public GameObject FastZed;
    public GameObject BigZed;
    public GameObject BabyZed;

    /// <summary> How many basic zeds will spawn this wave. </summary>
    private int _basicZeds;

    /// <summary> How many fast zeds will spawn this wave. </summary>
    private int _fastZeds;

    /// <summary> How many BigZeds will spawn this wave. </summary>
    private int _bigZeds;

    private int _babyZeds;

    private int currentWave = 0;

    private List<WaveInfo> waves = new List<WaveInfo>();

    /// <summary> The info for the zombies spawned during the fist wave. </summary>
    public WaveInfo _waveOne;

    /// <summary> The info for the zombies spawned during the second wave. </summary>
    public WaveInfo _waveTwo;

    /// <summary> The info for the zombies spawned during the third wave. </summary>
    public WaveInfo _waveThree;

    /// <summary> The info for the current wave. </summary>
    private WaveInfo currWaveInfo;

    //public WayPoint spawningPoint;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        
        
        //InvokeRepeating("Spawn", 2f, spawnRate);
        waves.Add(_waveOne);
        waves.Add(_waveTwo);
        waves.Add(_waveThree);

        StartCoroutine(WaitUntilGameSystemSet());
        //NewWave();
    }

    IEnumerator Spawn()
    {
        yield return new WaitUntil(() => GameSystem.Instance.State != GameState.Paused);
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            yield return new WaitUntil(() => GameSystem.Instance.State != GameState.Paused);
            //Instantiate(s)

            if (_basicZeds != 0)
            {
                GameObject zomb = Instantiate(BasicZed, transform.position, Quaternion.identity) as GameObject;
                zomb.GetComponent<Enemy>().dest = Link[0];
                _basicZeds--;
            }
            else if (_fastZeds != 0)
            {
                GameObject zomb = Instantiate(FastZed, transform.position, Quaternion.identity) as GameObject;
                zomb.GetComponent<Enemy>().dest = Link[0];
                _fastZeds--;
            }
            else if (_babyZeds != 0)
            {
                GameObject zomb = Instantiate(BabyZed, transform.position, Quaternion.identity) as GameObject;
                zomb.GetComponent<Enemy>().dest = Link[0];
                _babyZeds--;
            }
            else if (_bigZeds != 0)
            {
                GameObject zomb = Instantiate(BigZed, transform.position, Quaternion.identity) as GameObject;
                zomb.GetComponent<Enemy>().dest = Link[0];
                _bigZeds--;
            }


            if (_basicZeds == 0
                && _bigZeds == 0
                && _fastZeds == 0
                && _babyZeds == 0)
            {
                break;
            }
        }

        yield return new WaitUntil(() => GameSystem.Instance.ZombiesRemaining == 0);

        yield return new WaitForFixedUpdate();

        yield return new WaitUntil(() => GameSystem.Instance.State != GameState.Paused);

        //yield return new WaitForSeconds(5f);

        currentWave++;

        NewWave();
    }

    /// <summary>
    /// Sets up the spawning information for the next wave.
    /// </summary>
    void NewWave()
    {

        if (currentWave > 2)
        {
            GameSystem.Instance.Win();
            return;
        }


        currWaveInfo = waves[currentWave];

        _basicZeds = currWaveInfo.basicZeds;

        _bigZeds = currWaveInfo.bigZeds;

        _fastZeds = currWaveInfo.fastZeds;

        _babyZeds = currWaveInfo.babyZeds;

        int totalZeds = _basicZeds + _bigZeds + _fastZeds;

        GameSystem.Instance.ZombiesRemaining = totalZeds;

        StartCoroutine(Spawn());
    }

    IEnumerator WaitUntilGameSystemSet()
    {
        yield return new WaitUntil(() => GameSystem.Instance != null);

        NewWave();
    }
}
