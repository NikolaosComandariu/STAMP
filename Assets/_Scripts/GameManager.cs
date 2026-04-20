using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Round Related")]
    [SerializeField] private int currentRoundNumber;
    
    [Header("Timers")]
    [SerializeField] private float startCountdown;
    [SerializeField] private float roundCountdown;

    [Header("Game Objects")]
    [SerializeField] private GameObject startCountdownGO;
    [SerializeField] private GameObject roundTimerGO;

    // Text components.
    private TextMeshProUGUI startCountdownText;
    private TextMeshProUGUI roundTimerText;

    private int maxRoundNumber = 16;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundCountdown = 30.0f;

        startCountdownText = startCountdownGO.GetComponent<TextMeshProUGUI>();
        startCountdownText.text = startCountdown.ToString();

        //roundTimerText = roundTimerGO.GetComponent<TextMeshProUGUI>();
        //roundTimerText.text = roundCountdown.ToString();

        StartCoroutine(StartGameCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Starts game countdown, decreases the countdown
    /// every 1s. Unpauses the game once it's down, and
    /// starts the round countdown.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartGameCountdown()
    {
        while (startCountdown > 0)
        {
          
            yield return new WaitForSeconds(1.0f);
            startCountdown--;
            startCountdownGO.GetComponent<TextMeshProUGUI>().text = startCountdown.ToString();
        }

        PauseGame(false);
        StartCoroutine(StartRoundCountdown());

        yield return null;
    }

    /// <summary>
    /// Starts round countdown, countdown goes down
    /// gradually by 1 second.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartRoundCountdown()
    {
        while (startCountdown > 0)
        {
            
            yield return new WaitForSeconds(1.0f);
            roundCountdown--;
            roundTimerGO.GetComponent<TextMeshProUGUI>().text = startCountdown.ToString();
        }

        yield return null;
    }

    private void IncreaseDifficulty()
    {
        // TODO: Implement only when other scripts are finished/relatively finished.
    }

    /// <summary>
    /// Sets the Time.timeScale based on the boolean.
    /// </summary>
    /// <param name="isPaused"></param>
    private void PauseGame(bool isPaused)
    {
        Debug.Log("Game paused: " + isPaused);

        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private IEnumerator NextRound()
    {
        yield return null;
    }
}