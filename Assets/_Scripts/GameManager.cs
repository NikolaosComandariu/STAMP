using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Round Related")]
    [SerializeField] private int currentRoundNumber;

    [Header("Scripts")]
    [SerializeField] private CountdownManager countDownManager;
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private ObjectSpawner rightObjSpawner;
    [SerializeField] private roundManager roundManager;
    [SerializeField] private CriteriaManager criteriaManager; //added by smriti

    [Header("Variables")]
    [SerializeField] private int roundCountdownIncrease; // How many seconds a round increases by when difficulty increases.
    [SerializeField] private int roundItemsIncrease; // How many additional items spawn when difficulty increases.

    private int maxRoundNumber = 16;
    private int spawnCountdown = 3;
    private int roundTimer;
    private int objectsToSpawn;

    private bool roundEnding = false;
    private bool p1Finished;
    private bool p2Finished;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Set default values to round number and countdown.
        currentRoundNumber = 0;
        roundTimer = 20;
        objectsToSpawn = 5;

        p1Finished = false;
        p2Finished = false;

        // Call HandleRoundEnd() when these events are called.
        countDownManager.onRoundTimerFinished += HandleTimeRunningOut;
        objectSpawner.onAllObjectsProcessed += HandleLeftPlayerFinish;
        rightObjSpawner.onAllObjectsProcessed += HandleRightPlayerFinish;

        objectSpawner.ChangeNumberOfObjectsSpawned(objectsToSpawn);
        rightObjSpawner.ChangeNumberOfObjectsSpawned(objectsToSpawn);

       // criteriaManager.populateDict(); //code added by smriti
        //rightObjSpawner.populateDict();
        /*for (int i = 0; i <= criteriaManager.criteriaNumber; i++) 
        { 
            criteriaManager.selectCriteria(); 
        }*/

        criteriaManager.displayCriteria();

         //rightObjSpawner.selectCriteria(); //end of code added by smriti

         StartCoroutine(NextRound());
    }

    private void IncreaseDifficulty()
    {
        roundTimer += roundCountdownIncrease;
        objectsToSpawn += roundItemsIncrease;

        countDownManager.SetCountdownTimer(roundTimer);
        objectSpawner.ChangeNumberOfObjectsSpawned(objectsToSpawn);
        rightObjSpawner.ChangeNumberOfObjectsSpawned(objectsToSpawn);

        criteriaManager.IncreaseAmountOfCriteria(); //smriti added this
       // rightObjSpawner.IncreaseAmountOfCriteria(); //smriti added this

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
        if (!p1Finished && !p2Finished) yield return null;

        //PauseGame(true);
        roundEnding = false;

        if (currentRoundNumber < 16)
            currentRoundNumber++;

        // Update text displaying current round number.
        roundManager.UpdateRound(currentRoundNumber);

        // If round number is a multiple of 5, increase difficulty.
        if(currentRoundNumber % 5 == 0)
            IncreaseDifficulty();

        // TODO: Reset Criteria and get new ones for the round.
        yield return StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        spawnCountdown = 3;

        p1Finished = false;
        p2Finished = false;

        objectSpawner.GenerateObjectsForRound();
        rightObjSpawner.GenerateObjectsForRound();
        countDownManager.SetCountdownTimer(roundTimer);
        //StartCoroutine(countDownManager.StartGameCountdown());

        while (spawnCountdown > 0)
        {
            yield return new WaitForSeconds(1.0f);

            spawnCountdown--;
        }

        StartCoroutine(objectSpawner.SpawnObject());
        StartCoroutine(rightObjSpawner.SpawnObject());

        yield return null;
    }

    private void HandleLeftPlayerFinish()
    {
        p1Finished = true;
        HandleRoundEnd();
    }

    private void HandleRightPlayerFinish()
    {
        p2Finished = true;
        HandleRoundEnd();
    }

    private void HandleTimeRunningOut()
    {
        p1Finished = true;
        p2Finished = true;
        HandleRoundEnd();
    }

    private void HandleRoundEnd()
    {
        if (roundEnding || !p1Finished || !p2Finished) return;

        p1Finished = false;
        p2Finished = false;
        roundEnding = true;

        objectSpawner.ResetObjects();
        rightObjSpawner.ResetObjects();
        StartCoroutine(NextRound());
    }
}