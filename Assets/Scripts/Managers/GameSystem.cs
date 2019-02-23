using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSystem : MonoBehaviour
{
    // Singleton
    public static GameSystem instance;

    // State of the game
    [HideInInspector] public static GameStates gameState = GameStates.Playing;

    [HideInInspector] public bool isNormalGravity;

    [Header("Player Stats")]
    public int coins;
    [SerializeField] private int score;

    [Header("Parameters")]
    [SerializeField] private float timeToIncreaseScore;

    [Header("InGame UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button pauseButton;

    [Header("Pause UI")]
    [SerializeField] private CanvasRenderer pauseMenu;

    [Header("End UI")]
    [SerializeField] private CanvasRenderer endMenu;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private static int bestScore;
    private PlayerMovement playerMovement;

    // Initialize Singleton
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Reset Game State
        gameState = GameStates.Playing;

        // Conponents Declaration
        playerMovement = FindObjectOfType<PlayerMovement>();

        // Initialize Event
        InputManager.instance.OnChangeGravity += ChangeGravity;

        // Reset Gravity Force
        Physics.gravity = new Vector3(0f, -Utilities.localGravityForce * playerMovement.gravityForceMultiplier, 0f);
        isNormalGravity = true;

        // Starting Coroutine
        StartCoroutine(IncreaseScore());

        // UI Management
        ShowGameUI(true);
        pauseMenu.gameObject.SetActive(false);
        endMenu.gameObject.SetActive(false);
    }

    void ChangeGravity()
    {
        if (PlayerMovement.playerState == PlayerStates.Gravitation)
        {
            // Change gravity vector direction
            Vector3 newGravity = Physics.gravity;
            newGravity *= -1;
            Physics.gravity = newGravity;

            // Update Gravity Status
            isNormalGravity = !isNormalGravity;
        }
    }

    // Increase Coins score and UI update
    public void CollectCoins(int value)
    {
        coins += value;
        coinsText.text = "Coins: " + coins.ToString();
    }

    // Increase Time score value and UI update
    IEnumerator IncreaseScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToIncreaseScore);

            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void EndGame()
    {
        // Update Game State
        gameState = GameStates.GameOver;

        // Stop Player Controls & Particules
        playerMovement.StopControls();
        playerMovement.HideParticules();

        // Reset Gravity
        isNormalGravity = true;

        // Stop increasing score
        StopAllCoroutines();

        // Display End Menu
        ShowGameUI(false);
        endMenu.gameObject.SetActive(true);

        // Best Score
        if (score > bestScore)
            bestScore = score;

        // Update text
        bestScoreText.text = "Best score " + bestScore.ToString();
        finalScoreText.text = "Your score " + score.ToString();
    }

    public void PauseGame()
    {
        if (gameState == GameStates.Playing)
        {
            gameState = GameStates.Pause;
            Time.timeScale = 0f;

            // Display Pause Menu
            ShowGameUI(false);
            pauseMenu.gameObject.SetActive(true);

        }
        else if (gameState == GameStates.Pause)
        {
            gameState = GameStates.Playing;
            Time.timeScale = 1f;

            // Hide Pause Menu
            pauseMenu.gameObject.SetActive(false);
            ShowGameUI(true);
        }
    }

    // Show or Hide In Game UI
    void ShowGameUI(bool value)
    {
        if (value)
        {
            scoreText.gameObject.SetActive(true);
            coinsText.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(true);
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            coinsText.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(false);
        }
    }
}
