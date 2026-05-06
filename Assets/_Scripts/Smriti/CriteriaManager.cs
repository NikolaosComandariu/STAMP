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
    [SerializeField] private TextMeshProUGUI _criteriaP1_1;
    [SerializeField] private TextMeshProUGUI _criteriaP1_2;
    [SerializeField] private TextMeshProUGUI _criteriaP1_3;

    [SerializeField] private TextMeshProUGUI _criteriaP2_1;
    [SerializeField] private TextMeshProUGUI _criteriaP2_2;
    [SerializeField] private TextMeshProUGUI _criteriaP2_3;
    // [SerializeField] private TextMeshProUGUI _criteria4;

    private Dictionary<int, ColourCondition> ColourList = new Dictionary<int, ColourCondition>();
    private Dictionary<int, ItemCondition> ItemList = new Dictionary<int, ItemCondition>();
    private Dictionary<int, PriceCondition> PriceList = new Dictionary<int, PriceCondition>();
    public int criteriaNumber = 1;

    public static event Action<int, int, int> OnCriteriaDecided;
   // private bool duplicateOccured = false;

   /* public enum RoundCondition
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
    }*/


    public enum ColourCondition
    {
        Red,
        Orange,
        Yellow,
        Green,
        NotRed,
        NotOrange,
        NotYellow,
        NotGreen
    }

    public enum ItemCondition
    {
        Fruit,
        Drink,
        Single,
        NotFruit,
        NotDrink,
        NotSingle
    }

    public enum PriceCondition
    {
        LessThan5Pounds,
        MoreThan5Pounds,
        LessThan3Pounds,
        MoreThan3Pounds,
        LessThan1Pound,
        MoreThan1Pound,
        LessThan2Pounds,
        MoreThan2Pounds
        //More to be added later once properly sorted out
    }

    /*public enum CriteriaOnScreen
    {
        One,
        Two,
        Three,
        Four
    }*/

    //populating dictionary
    public void populateColourDict()
    {
        ColourList.Clear();

        ColourList.Add(1, ColourCondition.Red);
        ColourList.Add(2, ColourCondition.Orange);
        ColourList.Add(3, ColourCondition.Yellow);
        ColourList.Add(4, ColourCondition.Green);
        ColourList.Add(5, ColourCondition.NotRed);
        ColourList.Add(6, ColourCondition.NotOrange);
        ColourList.Add(7, ColourCondition.NotYellow);
        ColourList.Add(8, ColourCondition.NotGreen);
   
    }

    public void populateItemDict()
    {
        ItemList.Clear();

        ItemList.Add(1, ItemCondition.Fruit);
        ItemList.Add(2, ItemCondition.Drink);
        ItemList.Add(3, ItemCondition.Single);
        ItemList.Add(4, ItemCondition.NotFruit);
        ItemList.Add(5, ItemCondition.NotDrink);
        ItemList.Add(6, ItemCondition.NotSingle);
    }

    public void populatePriceDict()
    {
        PriceList.Clear();

        PriceList.Add(1, PriceCondition.LessThan5Pounds);
        PriceList.Add(2, PriceCondition.MoreThan5Pounds);
        PriceList.Add(3, PriceCondition.LessThan3Pounds);
        PriceList.Add(4, PriceCondition.MoreThan3Pounds);
        PriceList.Add(5, PriceCondition.LessThan1Pound);
        PriceList.Add(6, PriceCondition.MoreThan1Pound);
        PriceList.Add(7, PriceCondition.LessThan2Pounds);
        PriceList.Add(8, PriceCondition.MoreThan2Pounds);


        //TO BE ADDED
    }

    public void IncreaseAmountOfCriteria() { criteriaNumber++; }

    public void selectCriteria()
    {
        /*if (duplicateOccured != true)
       // {
            for (int i = 0; i < criteriaNumber;)
            {
                int criteria;
                criteria = Random.Range(1, ColourList.Count);
                //RoundCondition qRC = CriteriaList[key: amountOfCriteria];
                //Debug.Log(qRC.ToString());
                //_criteria1.text = "Criteria: " + qRC.ToString();

                criteriaTextList[i] = criteria;

                /*if(i != 0 && criteriaTextList[i] == criteriaTextList[i - 1])
                {
                    duplicateOccured = true;
                }
                else if(i == 2 && criteriaTextList[i] == criteriaTextList[i - 2])
                {
                    duplicateOccured = true;
                }
                else
                {
                    i++;
                }
            }
       // }

       /* else
        {
            duplicateOccured = false;
            selectCriteria();
        }*/

        //selectCriteria();


        /* for (int i =  0; i < criteriaNumber;)
         {
             int criteria;
             criteria = Random.Range(1, CriteriaList.Count);
             //RoundCondition qRC = CriteriaList[key: amountOfCriteria];
             //Debug.Log(qRC.ToString());
             //_criteria1.text = "Criteria: " + qRC.ToString();

             criteriaTextList[i] = criteria;
         }
         int q; 
         q = Random.Range(0, CriteriaList.Count);
         RoundCondition qRC = CriteriaList[key: q];
         Debug.Log(qRC.ToString());
         _criteria1.text = "Criteria: " + qRC.ToString(); */

        int colourKey;
        int itemKey;
        int priceKey;

        colourKey = Random.Range(1, ColourList.Count);
        itemKey = Random.Range(1, ItemList.Count);
        priceKey = Random.Range(1, PriceList.Count);

        if (criteriaNumber == 3)
        {
            criteriaTextList[0] = colourKey;
            criteriaTextList[1] = itemKey;
            criteriaTextList[2] = priceKey;
        }

        if (criteriaNumber == 2) 
        {
            criteriaTextList[0] = colourKey;
            criteriaTextList[1] = itemKey;
        }

        if (criteriaNumber == 1)
        {
            criteriaTextList[0] = colourKey;
        }

        if (criteriaTextList[0] != 0)
        {
            _criteriaP1_1.text = "Criteria 1: " + ColourList[key: criteriaTextList[0]].ToString() + "\n";
            _criteriaP2_1.text = "Criteria 1: " + ColourList[key: criteriaTextList[0]].ToString() + "\n";
        }

        //_criteriaP1_1.text = "Criteria 1: " + CriteriaList[key: criteriaTextList[0]].ToString() + "\n";
       // _criteriaP2_1.text = "Criteria 1: " + CriteriaList[key: criteriaTextList[0]].ToString() + "\n";

        if (criteriaTextList[1] != 0)
        {
            _criteriaP1_2.text = "Criteria 2: " + ItemList[key: criteriaTextList[1]].ToString() + "\n";
            _criteriaP2_2.text = "Criteria 2: " + ItemList[key: criteriaTextList[1]].ToString() + "\n";
        }
            

        if (criteriaTextList[2] != 0)
        {
            _criteriaP1_3.text = "Criteria 3: " + PriceList[key: criteriaTextList[2]].ToString() + "\n";
            _criteriaP2_3.text = "Criteria 3: " + PriceList[key: criteriaTextList[2]].ToString() + "\n";
        }
           // _criteria3.text = "Criteria 3: " + CriteriaList[key: criteriaTextList[2]].ToString() + "\n";

        //if (criteriaTextList[3] != 0)
           // _criteria4.text = "Criteria 4: " + CriteriaList[key: criteriaTextList[3]].ToString() + "\n";
            
        OnCriteriaDecided.Invoke(criteriaTextList[0], criteriaTextList[1],
            criteriaTextList[2]);
    }

    public void displayCriteria()
    {
        //for (int i = 0; i <= criteriaNumber; i++)
        //{
            populateColourDict();
            populateItemDict();
            populatePriceDict();
            selectCriteria();
            //i++;
        //}
    }
}