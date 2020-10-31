using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _gold = 30;
    private int _score = 0;
    [SerializeField] private int _lives;

    public int Gold
    {
        get { return _gold; }
        set
        {
           _gold = value;

            GameSystem.Instance.UpdateGold(_gold);
        }
    }

    /// <summary>
    /// Public property that holds the number of lives the player has remaining.
    /// Automatically updates the UI in the game system.
    /// </summary>
    public int Lives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            _lives = Mathf.Clamp(_lives, 0, 3);

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


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }
}
