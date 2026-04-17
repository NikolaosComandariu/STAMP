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


    /*  private bool critBoolValue()
      {
          bool correctCritVal = false;

          for (int i = 0; i < criteria.Count; i++)
          {
              if (i == 1 || i == 3 || i == 4 || i == 5 || i == 10 || i == 11 || i == 12 || i == 13 || i == 15)
              {
                  correctCritVal = true;
                  i++;
              }
              else if (i == 2 || i == 6 || i == 7 || i == 8 || i == 9 || i == 14)
              {
                  correctCritVal = false;
                  i++;
              }
              else
              {
                  return false;
              }

              return correctCritVal;
          }
          return correctCritVal;

      }
          */

    public int getPlayer1Score()
    {
        return Player1Score;
    }
    public int getPlayer2Score() 
    { 
        return Player2Score; 
    }

    public void changePlayer1Score(int score)
    {
        Player1Score += score;
    }

    public void changePlayer2Score(int score)
    {
        Player2Score += score;
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
