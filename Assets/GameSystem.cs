using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameState
{
    Play,
    Paused,
}

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    public GameState State { get; set; } = GameState.Paused;

    public bool gameWon = false;

    public Text goldText, scoreText, livesText, endText, remainingZombiesText;

    public Button playButton, pauseButton, normalSpeedButton, fastSpeedButton;

    private int _zombiesRemaining;

    /// <summary> The total number of remaining zombies this wave. </summary>
    [HideInInspector] public int ZombiesRemaining
    {
        get { return _zombiesRemaining; }

        set
        {
            _zombiesRemaining = value;
            remainingZombiesText.text = "Remaining Zombies: " + _zombiesRemaining;
        }
    }

    public List<Button> shopButtons = new List<Button>();

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        StartUp();
        
    }

    /// <summary>
    /// Updates the text component representing our current gold.
    /// </summary>
    /// <param name="gold">The amount of gold we have.</param>
    public void UpdateGold(int gold)
    {
        goldText.text = "Gold: " + gold;
    }

    /// <summary>
    /// Updates the text component representing our current score.
    /// </summary>
    /// <param name="score">The score we've accumulated.</param>
    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Updates the text component representing our remaining lives.
    /// </summary>
    /// <param name="lives">The amount of lives we have remaining.</param>
    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;

        if (lives <= 0) Lose();
    }

    private void StartUp()
    {
        if (goldText == null) goldText = GameObject.Find("Gold Text").GetComponent<Text>();
        if (scoreText == null) scoreText = GameObject.Find("Score Text").GetComponent<Text>();

        UpdateGold(30);

        UpdateScore(0);
        UpdateLives(3);

        PauseGame();
    }

    public void PlayGame()
    {
        State = GameState.Play;
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        State = GameState.Paused;
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Plays the game at normal speed.
    /// </summary>
    public void NormalSpeed()
    {
        Time.timeScale = 1f;
        normalSpeedButton.gameObject.SetActive(true);
        fastSpeedButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Plays the game at double speed.
    /// </summary>
    public void FastSpeed()
    {
        Time.timeScale = 2f;
        normalSpeedButton.gameObject.SetActive(false);
        fastSpeedButton.gameObject.SetActive(true);
    }

    /// <summary> Displays the lose game text. </summary>
    public void Lose()
    {
        endText.text = "You Lose!";

        endText.gameObject.SetActive(true);
    }

    /// <summary> Displays the win game text. </summary>
    public void Win()
    {
        endText.text = "You Win!";

        endText.gameObject.SetActive(true);
    }

    private List<Timer> timerList;

    public void UpdateTimers()
    {
        
    }

}
