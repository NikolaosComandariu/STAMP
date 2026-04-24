using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int Player1Score;
    private int Player2Score;

    [Header("TextGameObject")]
    [SerializeField] private TextMeshProUGUI p1Score;
    [SerializeField] private TextMeshProUGUI p2Score;

    [Header("Spawners")]
    [SerializeField] private ObjectSpawner leftSpawner;
    [SerializeField] private ObjectSpawner rightSpawner;

    private void Start()
    {
        leftSpawner.onScoreDecreased += decreasePlayer1Score;
        rightSpawner.onScoreDecreased += decreasePlayer2Score;
        leftSpawner.onScoreIncreased += increasePlayer1Score;
        rightSpawner.onScoreIncreased += increasePlayer2Score;
    }

    //getters
    public int getPlayer1Score()
    {
        return Player1Score;
    }
    public int getPlayer2Score()
    {
        return Player2Score;
    }

    //changes scores
   /* public void changePlayer1Score()
    {
        Player1Score += score;
        p1Score.text = Player1Score.ToString();
    }

    public void changePlayer2Score()
    {
        Player2Score += score;
        p2Score.text = "Player 2 Score : " + Player2Score.ToString();
    }*/

    public void increasePlayer1Score()
    {
        Player1Score++;
        p1Score.text = "Player1 Score: " + Player1Score.ToString();
    }
    public void increasePlayer2Score()
    {
        Player2Score++;
        p2Score.text = "Player2 Score: " + Player2Score.ToString();
    }

    public void decreasePlayer1Score()
    {
        Player1Score--;
        p1Score.text = "Player1 Score: " + Player1Score.ToString();
    }

    public void decreasePlayer2Score()
    {
        Player2Score--;
        p2Score.text= "Player2 Score: " + Player2Score.ToString();
    }
}