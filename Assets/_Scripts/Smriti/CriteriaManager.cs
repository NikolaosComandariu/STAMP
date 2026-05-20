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

    [Header("Dictionaries")]
    //private Dictionary<int, ColourCondition> ColourList = new Dictionary<int, ColourCondition>();
    private Dictionary<int, ItemCondition> ItemList = new Dictionary<int, ItemCondition>();
   // private Dictionary<int, PriceCondition> PriceList = new Dictionary<int, PriceCondition>();
   // private Dictionary<int, string> PriceTextList = new Dictionary<int, string>();
    private Dictionary<int, SupermarketCondition> SupermarketTextList = new Dictionary<int, SupermarketCondition>();
    // private Dictionary<int, Conditions> ConditionsList = new Dictionary<int, Conditions>();
    public int criteriaNumber = 1;

    public static event Action<int> OnCriteriaDecided;

    // Start of Nikolaos Comandariu code.
    private bool refundActive = false;

    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        GameChangerManager.onRefundActivated += RefundActive;
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        GameChangerManager.onRefundActivated -= RefundActive;
    }

    // End of Nikolaos Comandariu code.
   //private ObjectPrototype_ myProto;

    /*public enum ColourCondition
    {
        Red,
        Orange,
        Yellow,
        Green,
        NotRed,
        NotOrange,
        NotYellow,
        NotGreen
    }*/

    public enum ItemCondition
    {
        Fruit,
        Drink,
        Single,
        NotFruit,
        NotDrink,
        NotSingle,
        Red,
        Orange,
        Yellow,
        Green,
        NotRed,
        NotOrange,
        NotYellow,
        NotGreen,
        LessThan5Pounds,
        MoreThan5Pounds,
        LessThan3Pounds,
        MoreThan3Pounds,
        LessThan2Pounds,
        MoreThan2Pounds,
        LessThan1Pound,
        MoreThan1Pound
    }

   /* public enum PriceCondition
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
    }*/

    public enum SupermarketCondition
    {
        SupermarketItems
    }

   /* public enum Conditions
    {
        Red,
        Orange,
        Yellow,
        Green,
        NotRed,
        NotOrange,
        NotYellow,
        NotGreen,
        Fruit,
        Drink,
        Single,
        NotFruit,
        NotDrink,
        NotSingle
    }*/

    //populating dictionary
    /*public void populateColourDict()
    {
        ColourList.Clear();

        ColourList.Add(0, ColourCondition.Red);
        ColourList.Add(1, ColourCondition.Orange);
        ColourList.Add(2, ColourCondition.Yellow);
        ColourList.Add(3, ColourCondition.Green);
        ColourList.Add(4, ColourCondition.NotRed);
        ColourList.Add(5, ColourCondition.NotOrange);
        ColourList.Add(6, ColourCondition.NotYellow);
        ColourList.Add(7, ColourCondition.NotGreen);
   
    }*/

    public void populateItemDict()
    {
        ItemList.Clear();

        ItemList.Add(0, ItemCondition.Red);
        ItemList.Add(1, ItemCondition.Orange);
        ItemList.Add(2, ItemCondition.Yellow);
        ItemList.Add(3, ItemCondition.Green);
        ItemList.Add(4, ItemCondition.NotRed);
        ItemList.Add(5, ItemCondition.NotOrange);
        ItemList.Add(6, ItemCondition.NotYellow);
        ItemList.Add(7, ItemCondition.NotGreen);
        ItemList.Add(8, ItemCondition.Fruit);
        ItemList.Add(9, ItemCondition.Drink);
        ItemList.Add(10, ItemCondition.Single);
        ItemList.Add(11, ItemCondition.NotFruit);
        ItemList.Add(12, ItemCondition.NotDrink);
        ItemList.Add(13, ItemCondition.NotSingle);
        ItemList.Add(14, ItemCondition.LessThan5Pounds);
        ItemList.Add(15, ItemCondition.MoreThan5Pounds);
        ItemList.Add(16, ItemCondition.LessThan3Pounds);
        ItemList.Add(17, ItemCondition.MoreThan3Pounds);
        ItemList.Add(18, ItemCondition.LessThan2Pounds);
        ItemList.Add(19, ItemCondition.MoreThan2Pounds);
        ItemList.Add(20, ItemCondition.LessThan1Pound);
        ItemList.Add(21, ItemCondition.MoreThan1Pound);

    }

   /* public void populatePriceDict()
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

    }*/

    /*public void populatePriceTextDict()
    {
        PriceTextList.Clear();

        PriceTextList.Add(0, getPriceTitle(PriceCondition.LessThan5Pounds));
        PriceTextList.Add(1, getPriceTitle(PriceCondition.MoreThan5Pounds));
        PriceTextList.Add(2, getPriceTitle(PriceCondition.LessThan3Pounds));
        PriceTextList.Add(3, getPriceTitle(PriceCondition.MoreThan3Pounds));
        PriceTextList.Add(4, getPriceTitle(PriceCondition.LessThan2Pounds));
        PriceTextList.Add(5, getPriceTitle(PriceCondition.MoreThan2Pounds));
        PriceTextList.Add(6, getPriceTitle(PriceCondition.LessThan1Pound));
        PriceTextList.Add(7, getPriceTitle(PriceCondition.MoreThan1Pound));
    }*/

    public void populateSupermarketTextDict()
    {
        SupermarketTextList.Clear();

        SupermarketTextList.Add(0, SupermarketCondition.SupermarketItems);
    }

    private string getPriceTitle(ItemCondition PC)
    {
        switch (PC)
        {
            case(ItemCondition.LessThan5Pounds):
                return "Price < £5";
                break;
            case (ItemCondition.MoreThan5Pounds):
                return "Price > £5";
                break;
            case (ItemCondition.LessThan3Pounds):
                return "Price < £3";
                break;
            case (ItemCondition.MoreThan3Pounds):
                return "Price > £3";
                break;
            case (ItemCondition.LessThan1Pound):
                return "Price < £1";
                break;
            case (ItemCondition.MoreThan1Pound):
                return "Price >£1";
                break;
            case (ItemCondition.LessThan2Pounds):
                return "Price < £2";
                break;
            case (ItemCondition.MoreThan2Pounds):
                return "Price > £2";
                break;
            default:
                return null;
                break;

        }
    }

    /*public void populateCriterias()
    {
        ConditionsList.Clear();

        ConditionsList.Add(0, Conditions.Red);
        ConditionsList.Add(1, Conditions.Orange);
        ConditionsList.Add(2, Conditions.Yellow);
        ConditionsList.Add(3, Conditions.Green);
        ConditionsList.Add(4, Conditions.NotRed);
        ConditionsList.Add(5, Conditions.NotOrange);
        ConditionsList.Add(6, Conditions.NotYellow);
        ConditionsList.Add(7, Conditions.NotGreen);
        ConditionsList.Add(8, Conditions.Fruit);
        ConditionsList.Add(9, Conditions.Drink);
        ConditionsList.Add(10, Conditions.Single);
        ConditionsList.Add(11, Conditions.NotFruit);
        ConditionsList.Add(12, Conditions.NotDrink);
        ConditionsList.Add(13, Conditions.NotSingle);
    }*/

    public void IncreaseAmountOfCriteria() { criteriaNumber++; } // this is no longer needed :(

    public void selectCriteria()
    {
        //int colourKey;
        int itemKey;
       // int priceKey;

        //colourKey = Random.Range(0, ColourList.Count);
        itemKey = Random.Range(0, ItemList.Count);
        //priceKey = Random.Range(0, PriceList.Count);
       // priceKey = Random.Range(0, PriceTextList.Count);

        switch (criteriaNumber)
        {
            case 1:
                //criteriaTextList[0] = colourKey; //+ 1;
                criteriaTextList[0] = itemKey;
                break;
          /* case 2:
                //criteriaTextList[0] = colourKey; //+ 1;
                criteriaTextList[0] = itemKey; //+ 6; // + 6 cause it's 1 list in objSpawner, -1 to translate
               // criteriaTextList[1] = priceKey;
                break;
            /*case 3:
                criteriaTextList[0] = colourKey;
                criteriaTextList[1] = itemKey; //+ 6;
                criteriaTextList[2] = priceKey;
                break;*/
        }

        if (criteriaTextList[0] != 0)
        {
            if (itemKey > 13)
            {
                _criteriaP1_1.text = getPriceTitle(ItemList[criteriaTextList[0]]).ToString() + "\n";
                _criteriaP2_1.text = getPriceTitle(ItemList[criteriaTextList[0]]).ToString() + "\n";
            }
            else
            {
                _criteriaP1_1.text = ItemList[key: criteriaTextList[0]].ToString() + "\n";
                _criteriaP2_1.text = ItemList[key: criteriaTextList[0]].ToString() + "\n";
            }

        }

      /*  if (criteriaTextList[1] != 0)
        {
            _criteriaP1_2.text = PriceTextList[key: criteriaTextList[1]].ToString() + "\n";
            _criteriaP2_2.text = PriceTextList[key: criteriaTextList[1]].ToString() + "\n";
        }

         if (criteriaTextList[2] != 0)
         {
             _criteriaP1_3.text = PriceTextList[key: criteriaTextList[2]].ToString() + "\n";
             _criteriaP2_3.text = PriceTextList[key: criteriaTextList[2]].ToString() + "\n";
         }*/

        /*if (criteriaTextList[1] != 0)
            criteriaTextList[1] = priceKey + 13;*/

        if (refundActive) // Nikolaos Comandariu.
        {
            _criteriaP1_1.text = "Supermarket Items";
            _criteriaP2_1.text = "Supermarket Items";
        }

        refundActive = false;

        OnCriteriaDecided.Invoke(criteriaTextList[0]); //criteriaTextList[1]);
    }

    public void displayCriteria()
    {
        //populateColourDict();
        populateItemDict();
       // populatePriceDict();
        //populatePriceTextDict();
        populateSupermarketTextDict();
        //populateCriterias();
        selectCriteria();
    }

    private void RefundActive()
    {
        refundActive = true;
        Debug.Log("Refund activated!");
    }
}