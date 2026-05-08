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


    private Dictionary<int, ColourCondition> ColourList = new Dictionary<int, ColourCondition>();
    private Dictionary<int, ItemCondition> ItemList = new Dictionary<int, ItemCondition>();
    private Dictionary<int, PriceCondition> PriceList = new Dictionary<int, PriceCondition>();
    public int criteriaNumber = 1;

    public static event Action<int, int, int> OnCriteriaDecided;


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
        int colourKey;
        int itemKey;
        int priceKey;

        colourKey = Random.Range(1, ColourList.Count);
        itemKey = Random.Range(1, ItemList.Count);
        priceKey = Random.Range(1, PriceList.Count);

        switch (criteriaNumber)
        {
            case 1:
                criteriaTextList[0] = colourKey;
                break;
            case 2:
                criteriaTextList[0] = colourKey;
                criteriaTextList[1] = itemKey;
                break;
            case 3:
                criteriaTextList[0] = colourKey;
                criteriaTextList[1] = itemKey;
                criteriaTextList[2] = priceKey;
                break;
        }


        if (criteriaTextList[0] != 0)
        {
            _criteriaP1_1.text = ColourList[key: criteriaTextList[0]].ToString() + "\n";
            _criteriaP2_1.text = ColourList[key: criteriaTextList[0]].ToString() + "\n";
        }

        if (criteriaTextList[1] != 0)
        {
            _criteriaP1_2.text = ItemList[key: criteriaTextList[1]].ToString() + "\n";
            _criteriaP2_2.text = ItemList[key: criteriaTextList[1]].ToString() + "\n";
        }
            

        if (criteriaTextList[2] != 0)
        {
            _criteriaP1_3.text = PriceList[key: criteriaTextList[2]].ToString() + "\n";
            _criteriaP2_3.text = PriceList[key: criteriaTextList[2]].ToString() + "\n";
        }
            
        OnCriteriaDecided.Invoke(criteriaTextList[0], criteriaTextList[1],
            criteriaTextList[2]);


        Debug.Log("CriteriaManager: Criterias are: " +  criteriaTextList[0] + ", " + criteriaTextList[1] + ", " + criteriaTextList[2]);
    }

    public void displayCriteria()
    {
            populateColourDict();
            populateItemDict();
            populatePriceDict();
            selectCriteria();
    }
}