using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private float startCountdown;
    [SerializeField] private float roundCountdown;

    [Header("Game Objects")]
    [SerializeField] private GameObject startCountdownGO;
    [SerializeField] private GameObject roundTimerGO;

    [Header("Events")]
    public System.Action onRoundTimerFinished;

    // Text components.
    private TextMeshProUGUI startCountdownText;
    private TextMeshProUGUI roundTimerText;

    // Coroutines, needed to stop a specific coroutine.
    private Coroutine startCountdownRoutine;
    private Coroutine roundCountdownRoutine;

    private void Awake()
    {
        startCountdownText = startCountdownGO.GetComponent<TextMeshProUGUI>();
        roundTimerText = roundTimerGO.GetComponent<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Set default values to round number and countdown.
        roundCountdown = 30.0f;
        
        if(startCountdownText != null)
            startCountdownText.text = startCountdown.ToString();

        if(roundTimerText != null)
            roundTimerText.text = roundCountdown.ToString();

        // Start countdown.
        //StartCoroutine(StartGameCountdown());
    }

    /// <summary>
    /// Starts game countdown, decreases the countdown
    /// every 1s. Unpauses the game once it's down, and
    /// starts the round countdown.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartGameCountdown()
    {
        while (startCountdown > 0)
        {
            yield return new WaitForSeconds(1.0f);

            startCountdown--;
            startCountdownText.text = startCountdown.ToString();
        }

        //PauseGame(false);

        // Start round countdown and store it.
        roundCountdownRoutine = StartCoroutine(StartRoundCountdown());

        yield return null;
    }

    /// <summary>
    /// Starts round countdown, countdown goes down
    /// gradually by 1 second.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartRoundCountdown()
    {
        while (roundCountdown > 0)
        {
            yield return new WaitForSeconds(1.0f);

            roundCountdown--;
            roundTimerText.text = roundCountdown.ToString();
        }

        Debug.Log("Round Countdown Expired!");
        onRoundTimerFinished?.Invoke();
        yield return null;
    }

    /// <summary>
    /// Sets the Time.timeScale based on the boolean.
    /// </summary>
    /// <param name="isPaused"></param>
    private void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        Debug.Log("Game paused: " + isPaused);
    }

    public void SetCountdownTimer(float time)
    {
        if (startCountdownRoutine != null)
            StopCoroutine(startCountdownRoutine);

        if (roundCountdownRoutine != null)
            StopCoroutine(roundCountdownRoutine);

        startCountdown = 3;
        roundCountdown = time;

        startCountdownText.text = startCountdown.ToString();
        roundTimerText.text = roundCountdown.ToString();

        startCountdownRoutine = StartCoroutine(StartGameCountdown());
    }
}
