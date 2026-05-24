using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class EndCanvas : MonoBehaviour
{
    [Header("Text Game Objects")]
    [SerializeField] private TextMeshProUGUI summaryP1;
    [SerializeField] private TextMeshProUGUI summaryP2;
    [SerializeField] private TextMeshProUGUI WinnerDeclaration;

    public static event Action onEndCanvasEnabled;




    // Scores.
    private int p1Score;
    private int p2Score;


    // Text.
    private string whoWon;

    public Animator animator;


    [SerializeField] private Transform P2ReceiptStartPos;
    [SerializeField] private Transform P1ReceiptStartPos;
    [SerializeField] private Transform P2ReceiptEndPos;
    [SerializeField] private Transform P1ReceiptEndPos;
    [SerializeField] private float ReceiptMoveSpeed;
    [SerializeField] private GameObject P1Receipt; 
    [SerializeField] private GameObject P2Receipt;
    [SerializeField] private GameObject PrinterAnimation;



    private void Start()
    {
        p1Score = 0;
        p2Score = 0;
        gameObject.GetComponent<Canvas>().enabled = false;
        PrinterAnimation.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            Debug.Log("KeyPressed - trying to move receipt");
            P1Receipt.transform.position = Vector2.MoveTowards(P1ReceiptStartPos.position, P1ReceiptEndPos.position, ReceiptMoveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Subscribe to delegate.
    /// </summary>
    private void OnEnable()
    {
        ButtonClick.onPrintReceiptP2 += PrintReceiptP2;
        GameManager.onGameOver += HandleGameOver;
        ButtonClick.onPrintReceipt += PrintReceipt;
        ScoreManager.SendPlayerScores += SetScores;
    }

    private void PrintReceiptP2()
    {
        PrinterAnimation.SetActive(true);
       
    }

    private void PrintReceipt()
    {
        
        

    }

    /// <summary>
    /// Unsubscribe from delegate.
    /// </summary>
    private void OnDisable()
    {
        ButtonClick.onPrintReceiptP2 -= PrintReceiptP2;
        ButtonClick.onPrintReceipt -= PrintReceipt;
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
        onEndCanvasEnabled?.Invoke();
        PrinterAnimation.SetActive(true);
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