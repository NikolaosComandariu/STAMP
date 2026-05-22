using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndCanvas : MonoBehaviour
{
    [Header("Text Game Objects")]
    [SerializeField] private TextMeshProUGUI summaryP1;
    [SerializeField] private TextMeshProUGUI summaryP2;
    [SerializeField] private TextMeshProUGUI WinnerDeclaration;

    // Scores.
    private int p1Score;
    private int p2Score;

    // Text.
    private string whoWon;

    private void Start()
    {
        p1Score = 0;
        p2Score = 0;
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Subscribe to delegate.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onGameOver += HandleGameOver;
        ScoreManager.SendPlayerScores += SetScores;
    }

    /// <summary>
    /// Unsubscribe from delegate.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onGameOver -= HandleGameOver;
        ScoreManager.SendPlayerScores -= SetScores;
    }

    /// <summary>
    /// Set time scale to 0 and enable canvas component.
    /// </summary>
    private void HandleGameOver()
    {
        Time.timeScale = 0.0f;
        gameObject.GetComponent<Canvas>().enabled = true;
        CompareScores();
    }

    /// <summary>
    /// Set time scale to 1 and load main menu scene.
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary>
    /// Set time scale to 1 and reload active scene.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Gets player scores and compares them.
    /// Says who won at the end of the game.
    /// </summary>
    /// <param name="score"></param>
    private void CompareScores()
    {
        if (p1Score > p2Score)
        {
            whoWon = "Player 1 Won!";
        }
        else if (p1Score < p2Score)
        {
            whoWon = "Player 2 Won!";
        }
        else
        {
            whoWon = "Players Tied!";
        }

        summaryP1.text = p1Score + "\n";
        //summaryP1.text = "Player 1 Score: " + p1Score + "\n";
        summaryP2.text = p2Score + "\n";
        //summaryP2.text = "Player 2 Score: " + p2Score + "\n";
        WinnerDeclaration.text += whoWon;


    }

    private void SetScores(int scoreP1, int scoreP2)
    {
        p1Score = scoreP1;
        p2Score = scoreP2;
    }
}