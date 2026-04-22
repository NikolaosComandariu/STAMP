using System.Collections;
using UnityEditor.Toolbars;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Round Related")]
    [SerializeField] private int currentRoundNumber;

    [Header("Game Objects")]
    [SerializeField] private GameObject countDownManager;
    [SerializeField] private GameObject objectSpawner;

    private int maxRoundNumber = 16;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set default values to round number and countdown.
        currentRoundNumber = 1;
    }

    private void IncreaseDifficulty()
    {
        // These values can be changed!
        countDownManager.GetComponent<CountdownManager>().SetCountdownTimer(30 + currentRoundNumber);
        objectSpawner.GetComponent<ObjectSpawner>().ChangeNumberOfObjectsSpawned(10 + currentRoundNumber);

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
        if(currentRoundNumber < 16)
            currentRoundNumber++;

        // TODO: If round is 5, 10 or 15, increase difficulty.

        //StartCoroutine(StartGameCountdown());

        // TODO: Add more logic once every script is linked.

        yield return null;
    }
}