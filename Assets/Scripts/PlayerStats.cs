using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _gold = 30;
    private int _score = 0;
    private int _lives = 0;

    public int Gold
    {
        get { return _gold; }
        set
        {
           _gold = value;

            GameSystem.Instance.UpdateGold(_gold);
        }
    }
    public int Lives
    {
        get { return _lives; }
        set
        {
            _lives = value;

            GameSystem.Instance.UpdateLives(_lives);
        }
    }

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }

    public static PlayerStats Instance;


    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }
}
