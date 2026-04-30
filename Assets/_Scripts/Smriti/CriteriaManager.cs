using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CriteriaManager : MonoBehaviour
{
    [SerializeField] private List <TextMeshProUGUI> criteriaTextList = new List<TextMeshProUGUI>();

    /*[SerializeField] private TextMeshProUGUI _criteria1;
    [SerializeField] private TextMeshProUGUI _criteria2;
    [SerializeField] private TextMeshProUGUI _criteria3;
    [SerializeField] private TextMeshProUGUI _criteria4;*/



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
 
    private Dictionary<int, RoundCondition> CriteriaList = new Dictionary<int, RoundCondition>();
    public int criteriaNumber = 1;

    //populating dictionary
    public void populateDict()
    {
        CriteriaList.Clear();

        CriteriaList.Add(0, RoundCondition.Fruit);
        CriteriaList.Add(1, RoundCondition.Red);
        CriteriaList.Add(2, RoundCondition.Green);
        CriteriaList.Add(3, RoundCondition.Yellow);
        CriteriaList.Add(4, RoundCondition.Single);
        CriteriaList.Add(5, RoundCondition.Orange);
        CriteriaList.Add(6, RoundCondition.Drink);
        CriteriaList.Add(7, RoundCondition.NotFruit);
        CriteriaList.Add(8, RoundCondition.NotRed);
        CriteriaList.Add(9, RoundCondition.NotGreen);
        CriteriaList.Add(10, RoundCondition.NotYellow);
        CriteriaList.Add(11, RoundCondition.NotSingle);
        CriteriaList.Add(12, RoundCondition.NotOrange);
        CriteriaList.Add(13, RoundCondition.NotDrink);

    }

    public void IncreaseAmountOfCriteria() { criteriaNumber++; }

    public void selectCriteria()
    {
        for (int i =  0; i < criteriaNumber; i++)
        {
            int q;
            q = Random.Range(0, CriteriaList.Count);
            RoundCondition qRC = CriteriaList[key: q];
            Debug.Log(qRC.ToString());
            //_criteria1.text = "Criteria: " + qRC.ToString();
            criteriaTextList[i].text = "Criteria: " + qRC.ToString();
        }
        /*int q; 
        q = Random.Range(0, CriteriaList.Count);
        RoundCondition qRC = CriteriaList[key: q];
        Debug.Log(qRC.ToString());
        _criteria1.text = "Criteria: " + qRC.ToString(); */
        
 
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
