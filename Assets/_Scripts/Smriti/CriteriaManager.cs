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
    private Dictionary<int, ItemCondition> ItemList = new Dictionary<int, ItemCondition>();
    private Dictionary<int, SupermarketCondition> SupermarketTextList = new Dictionary<int, SupermarketCondition>();
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

    public enum ItemCondition
    {
        Fruit,
        Drink,
        Single,
        Red,
        Orange,
        Yellow,
        Green,
        LessThan5Pounds,
        MoreThan5Pounds,
        LessThan3Pounds,
        MoreThan3Pounds,
        LessThan2Pounds,
        MoreThan2Pounds,
        LessThan1Pound,
        MoreThan1Pound
    }

    public enum SupermarketCondition
    {
        Groceries
    }

    public void populateItemDict()
    {
        ItemList.Clear();

        ItemList.Add(0, ItemCondition.Red);
        ItemList.Add(1, ItemCondition.Orange);
        ItemList.Add(2, ItemCondition.Yellow);
        ItemList.Add(3, ItemCondition.Green);
        ItemList.Add(4, ItemCondition.Fruit);
        ItemList.Add(5, ItemCondition.Drink);
        ItemList.Add(6, ItemCondition.Single);
        ItemList.Add(7, ItemCondition.LessThan5Pounds);
        ItemList.Add(8, ItemCondition.MoreThan5Pounds);
        ItemList.Add(9, ItemCondition.LessThan3Pounds);
        ItemList.Add(10, ItemCondition.MoreThan3Pounds);
        ItemList.Add(11, ItemCondition.LessThan2Pounds);
        ItemList.Add(12, ItemCondition.MoreThan2Pounds);
        ItemList.Add(13, ItemCondition.LessThan1Pound);
        ItemList.Add(14, ItemCondition.MoreThan1Pound);

    }


    public void populateSupermarketTextDict()
    {
        SupermarketTextList.Clear();

        SupermarketTextList.Add(0, SupermarketCondition.Groceries);
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

    public void IncreaseAmountOfCriteria() { criteriaNumber++; }

    public void selectCriteria()
    {
        int itemKey;
        itemKey = Random.Range(0, ItemList.Count);
       
        switch (criteriaNumber)
        {
            case 1:
                criteriaTextList[0] = itemKey;
                break;
        }

        if (criteriaTextList[0] != 0)
        {
            if (itemKey > 6)
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

   
        if (refundActive) // Nikolaos Comandariu.
        {
            _criteriaP1_1.text = "Groceries";
            _criteriaP2_1.text = "Groceries";
        }

        refundActive = false;

        OnCriteriaDecided.Invoke(criteriaTextList[0]); //criteriaTextList[1]);
    }

    public void displayCriteria()
    {
        populateItemDict();
        populateSupermarketTextDict();
        selectCriteria();
    }

    private void RefundActive()
    {
        refundActive = true;
        Debug.Log("Refund activated!");
    }
}