using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ObjectPrototype_ myObjPrototype;
    [SerializeField] private ProduceOptionsManager myPOManager;

    [Header("Criteria")]
    [SerializeField] private Dictionary<int, string> criteria = new Dictionary<int, string>();
    //[SerializeField] private ObjectSpawner objectSpawner; // can remove if not used
    //[SerializeField] private CriteriaManager criteriaManager; // can remove if not used;

    [Header("TextGameObject")]
    [SerializeField] private TextMeshProUGUI p1Score;
    [SerializeField] private TextMeshProUGUI p2Score;

    public static ScoreManager Instance;

    public event Action<int> OnPlayer2ScoreChanged;
    public event Action<int> OnPlayer1ScoreChanged;

    //"encapsulation", private set so only the scores can be set in this script
    //better way to do get and set methods
    public int Player1Score { get; private set; }
    public int Player2Score { get; private set; }

    //getters
    public int getPlayer1Score()
    {
        return Player1Score;
    }
    public int getPlayer2Score()
    {
        return Player2Score;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void AddScore(bool isPlayer1, int amount)
    {
        //objectSpawner.UpdateScoreUI();
        p1Score.text = "Player 1 Score: " + Player1Score.ToString();
        UnityEngine.Debug.Log("AddScore called, P1={isPlayer1} , amount={amount}");
        if (isPlayer1)
        {
            Player1Score += amount;
            UnityEngine.Debug.Log("P1 score Is now: " + Player1Score);
            OnPlayer1ScoreChanged?.Invoke(Player1Score);
        }
        else
        {
            Player2Score += amount;
            UnityEngine.Debug.Log("P2 score Is now: " + Player2Score);
            OnPlayer2ScoreChanged?.Invoke(Player2Score);
        }
    }

    //changes scores
    public void changePlayer1Score(int score)
    {
        //objectSpawner.UpdateScoreUI();
        Player1Score = score;
        p1Score.text = "Player 1 Score: " + Player1Score.ToString();
    }

    public void changePlayer2Score(int score)
    {
        //objectSpawner.UpdateScoreUI();
        Player2Score = score;
        p2Score.text = "Player 2 Score : " + Player2Score.ToString();
    }
}