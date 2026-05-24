using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class EndCanvas : MonoBehaviour
{
    [Header("Text Game Objects")]
    [SerializeField] private TextMeshProUGUI p1ScoreText;
    [SerializeField] private TextMeshProUGUI p2ScoreText;
    [SerializeField] private TextMeshProUGUI whoWon;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Variables")]
    [SerializeField] private float waitTime;

    [Header("Positions")]
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;

    [Header("Particle Prefabs")]
    [SerializeField] private GameObject receiptParticleEffect;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;

    // Scores.
    private int p1Score;
    private int p2Score;

    // Booleans.
    private bool p1Receipt = false;
    private bool p2Receipt = false;
    private bool hasShownReceipt = false;

    private void Start()
    {
        p1Score = 0;
        p2Score = 0;

        gameObject.GetComponent<Canvas>().enabled = false;
        p1ScoreText.enabled = false;
        p2ScoreText.enabled = false;
        whoWon.enabled = false;
    }

    /// <summary>
    /// Subscribe to delegate.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onGameOver += HandleGameOver;
        ScoreManager.SendPlayerScores += SetScores;
        ButtonClick.onP1PressedButton += p1WantsReceipt;
        ButtonClick.onP2PressedButton += p2WantsReceipt;
    }

    /// <summary>
    /// Unsubscribe from delegate.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onGameOver -= HandleGameOver;
        ScoreManager.SendPlayerScores -= SetScores;
        ButtonClick.onP1PressedButton -= p1WantsReceipt;
        ButtonClick.onP2PressedButton -= p2WantsReceipt;
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
            whoWon.text = "Player 1 Won!";
        }
        else if (p1Score < p2Score)
        {
            whoWon.text = "Player 2 Won!";
        }
        else
        {
            whoWon.text = "Players Tied!";
        }

        p1ScoreText.text = p1Score + "\n";
        p2ScoreText.text = p2Score + "\n";
    }

    private void SetScores(int scoreP1, int scoreP2)
    {
        p1Score = scoreP1;
        p2Score = scoreP2;
    }

    private void p1WantsReceipt(bool hasPressed)
    {
        p1Receipt = hasPressed;

        Debug.Log("P1 wants receipt");

        if (!p1Receipt || !p2Receipt || hasShownReceipt) return;

        StartCoroutine(HandleReceipts());
    }

    private void p2WantsReceipt(bool hasPressed)
    {
        p2Receipt = hasPressed;

        Debug.Log("P2 wants receipt");

        if (!p1Receipt || !p2Receipt || hasShownReceipt) return;

        StartCoroutine(HandleReceipts());
    }

    private IEnumerator HandleReceipts()
    {
        hasShownReceipt = true;
        anim.SetTrigger("Print");

        Debug.Log("Handling receipts!");

        Instantiate(receiptParticleEffect, leftPos.transform.position, Quaternion.identity);
        Instantiate(receiptParticleEffect, rightPos.transform.position, Quaternion.identity);
        audioManager.PlaySFX(audioManager.printSFX);

        yield return new WaitForSeconds(waitTime);

        anim.SetTrigger("FinishedPrint");
        anim.ResetTrigger("Print");

        p1ScoreText.enabled = true;
        p2ScoreText.enabled = true;
        whoWon.enabled = true;

        yield return null;
    }
}