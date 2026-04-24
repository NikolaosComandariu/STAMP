using System.Collections;
using UnityEditor.Toolbars;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Round Related")]
    [SerializeField] private int currentRoundNumber;

    [Header("Scripts")]
    [SerializeField] private CountdownManager countDownManager;
    [SerializeField] private ObjectSpawner objectSpawner;

    private int maxRoundNumber = 16;
    private int spawnCountdown = 3;

    private bool roundEnding = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Set default values to round number and countdown.
        currentRoundNumber = 0;

        // Call HandleRoundEnd() when these events are called.
        countDownManager.onRoundTimerFinished += HandleRoundEnd;
        objectSpawner.onAllObjectsProcessed += HandleRoundEnd;

        StartCoroutine(NextRound());
    }

    private void IncreaseDifficulty()
    {
        // These values can be changed!
        countDownManager.SetCountdownTimer(30 + currentRoundNumber);
        objectSpawner.ChangeNumberOfObjectsSpawned(10 + currentRoundNumber);

        // TODO: Increase criteria spawned once this functionality is in.
        // TODO (maybe): Increase speed of spawned objects, not necessary anymore.
    }

    /// <summary>
    /// Sets the Time.timeScale based on the boolean.
    /// </summary>
    /// <param name="isPaused"></param>
    private void PauseGame(bool isPaused)
    {
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        Debug.Log("Game paused: " + isPaused);
    }

    /// <summary>
    /// Start a new round! 
    /// </summary>
    /// <returns></returns>
    private IEnumerator NextRound()
    {
        //PauseGame(true);
        roundEnding = false;

        if (currentRoundNumber < 16)
            currentRoundNumber++;

        // If round number is a multiple of 5, increase difficulty.
        if(currentRoundNumber % 5 == 0)
            IncreaseDifficulty();

        // TODO: Reset Criteria and get new ones for the round.
        yield return StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        spawnCountdown = 3;

        objectSpawner.GenerateObjectsForRound();
        countDownManager.SetCountdownTimer(30f);
        //StartCoroutine(countDownManager.StartGameCountdown());

        while (spawnCountdown > 0)
        {
            yield return new WaitForSeconds(1.0f);

            spawnCountdown--;
        }

        StartCoroutine(objectSpawner.SpawnObject());

        yield return null;
    }

    private void HandleRoundEnd()
    {
        if (roundEnding) return;

        roundEnding = true;
        objectSpawner.ResetObjects();
        StartCoroutine(NextRound());
    }
}