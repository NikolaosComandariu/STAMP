using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public ObjectPrototype_ myObjPrototype;
    public ProduceOptionsManager myPOManager;
    [Header("Criteria")]
    [SerializeField] private Dictionary<int, string> criteria = new Dictionary<int, string>();

    private int Player1Score;
    private int Player2Score;

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
    public void changePlayer1Score(int score)
    {
        Player1Score += score;
    }

    public void changePlayer2Score(int score)
    {
        Player2Score += score;
    }
}