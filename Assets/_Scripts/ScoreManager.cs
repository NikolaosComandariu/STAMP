using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    //"encapsulation", private set so only the scores can be set in this script
    //better way to do get and set methods
    public int Player1Score { get; private set; }
    public int Player2Score { get; private set; }


    // events
    public event Action<int> OnPlayer1ScoreChanged;
    public event Action<int> OnPlayer2ScoreChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(bool isPlayer1, int amount)
    {
        UnityEngine.Debug.Log("AddScore called, P1={isPlayer1} , amount={amount}");
        if (isPlayer1)
        {

            Player1Score += amount;
            //UnityEngine.Debug.Log("P1 score Is now: " + Player1Score);
            OnPlayer1ScoreChanged?.Invoke(Player1Score);
        }
        else
        {
            Player2Score += amount;
            //UnityEngine.Debug.Log("P2 score Is now: " + Player2Score);
            OnPlayer2ScoreChanged?.Invoke(Player2Score);
        }
    }
}