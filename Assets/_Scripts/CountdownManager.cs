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

    // Text components.
    private TextMeshProUGUI startCountdownText;
    private TextMeshProUGUI roundTimerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Set default values to round number and countdown.
        roundCountdown = 30.0f;

        // Get text components for countdown timers.
        startCountdownText = startCountdownGO.GetComponent<TextMeshProUGUI>();
        startCountdownText.text = startCountdown.ToString();

        roundTimerText = roundTimerGO.GetComponent<TextMeshProUGUI>();
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
        while (roundCountdown > 0)
        {
            yield return new WaitForSeconds(1.0f);

            roundCountdown--;
            roundTimerText.text = roundCountdown.ToString();
        }

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
        roundCountdown = time;
    }
}
