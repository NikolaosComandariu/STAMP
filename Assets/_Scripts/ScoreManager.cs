using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        //objectSpawner.UpdateScoreUI();
        Player1Score += score;
        p1Score.text = "Player 1 Score: " + Player1Score.ToString();
    }

    public void changePlayer2Score(int score)
    {
        //objectSpawner.UpdateScoreUI();
        Player2Score += score;
        p2Score.text = "Player 2 Score : " + Player2Score.ToString();
    }


}