using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CriteriaManager : MonoBehaviour
{
    [Header("Criterias")]
    [SerializeField] private List <int> criteriaTextList = new List<int>();

    [Header("Criteria Text Objects")]
    [SerializeField] private TextMeshProUGUI _criteria1;
    [SerializeField] private TextMeshProUGUI _criteria2;
    [SerializeField] private TextMeshProUGUI _criteria3;
    [SerializeField] private TextMeshProUGUI _criteria4;

    private Dictionary<int, RoundCondition> CriteriaList = new Dictionary<int, RoundCondition>();
    public int criteriaNumber = 1;

    public static event Action<int, int, int, int> OnCriteriaDecided;

    public enum RoundCondition
    {
        Fruit,
        Red,
        Green,
        Yellow,
        Single,
        Orange, 
        Drink,
        NotFruit,
        NotRed,
        NotGreen,
        NotYellow,
        NotSingle,
        NotOrange,
        NotDrink 
    }

    /*public enum CriteriaOnScreen
    {
        One,
        Two,
        Three,
        Four
    }*/

    //populating dictionary
    public void populateDict()
    {
        CriteriaList.Clear();

        CriteriaList.Add(1, RoundCondition.Fruit);
        CriteriaList.Add(2, RoundCondition.Red);
        CriteriaList.Add(3, RoundCondition.Green);
        CriteriaList.Add(4, RoundCondition.Yellow);
        CriteriaList.Add(5, RoundCondition.Single);
        CriteriaList.Add(6, RoundCondition.Orange);
        CriteriaList.Add(7, RoundCondition.Drink);
        CriteriaList.Add(8, RoundCondition.NotFruit);
        CriteriaList.Add(9, RoundCondition.NotRed);
        CriteriaList.Add(10, RoundCondition.NotGreen);
        CriteriaList.Add(11, RoundCondition.NotYellow);
        CriteriaList.Add(12, RoundCondition.NotSingle);
        CriteriaList.Add(13, RoundCondition.NotOrange);
        CriteriaList.Add(14, RoundCondition.NotDrink);
    }

    public void IncreaseAmountOfCriteria() { criteriaNumber++; }

    public void selectCriteria()
    {
        for (int i =  0; i < criteriaNumber; i++)
        {
            int criteria;
            criteria = Random.Range(1, CriteriaList.Count);
            //RoundCondition qRC = CriteriaList[key: amountOfCriteria];
            //Debug.Log(qRC.ToString());
            //_criteria1.text = "Criteria: " + qRC.ToString();

            criteriaTextList[i] = criteria;
        }
        /*int q; 
        q = Random.Range(0, CriteriaList.Count);
        RoundCondition qRC = CriteriaList[key: q];
        Debug.Log(qRC.ToString());
        _criteria1.text = "Criteria: " + qRC.ToString(); */

        _criteria1.text = "Criteria 1: " + CriteriaList[key: criteriaTextList[0]].ToString() + "\n";

        if (criteriaTextList[1] != 0)
            _criteria2.text = "Criteria 2: " + CriteriaList[key: criteriaTextList[1]].ToString() + "\n";

        if (criteriaTextList[2] != 0)
            _criteria2.text = "Criteria 2: " + CriteriaList[key: criteriaTextList[2]].ToString() + "\n";

        if (criteriaTextList[3] != 0)
            _criteria2.text = "Criteria 2: " + CriteriaList[key: criteriaTextList[2]].ToString() + "\n";
            
        OnCriteriaDecided.Invoke(criteriaTextList[0], criteriaTextList[1],
            criteriaTextList[2], criteriaTextList[3]);
    }

    public void displayCriteria()
    {
        //for (int i = 0; i <= criteriaNumber; i++)
        //{
            populateDict();
            selectCriteria();
            //i++;
        //}
    }
}