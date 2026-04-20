using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Round Related")]
    [SerializeField] private int currentRoundNumber;
    
    [Header("Timers")]
    [SerializeField] private float startCountdown;
    [SerializeField] private float roundCountdown;

    [Header("Game Objects")]
    [SerializeField] private GameObject startCountdownTimer;
    [SerializeField] private GameObject roundTimer;

    private int maxRoundNumber = 16;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartGameCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartGameCountdown()
    {
        while (startCountdown > 0)
        {
            yield return new WaitForSeconds(1.0f);
            startCountdown--;
        }

        PauseGame();

        yield return null;
    }

    private void StartRoundCountdown()
    {

    }

    private void IncreaseDifficulty()
    {

    }
    private void PauseGame()
    {
        Debug.Log("Game Paused!");
    }    
}