using System.Collections.Generic;
using UnityEngine;

public class ProduceOptionsManager : MonoBehaviour
{
    [Header("Left Option Game Objects")]
    [SerializeField] private GameObject leftOption1;
    [SerializeField] private GameObject leftOption2;
    [SerializeField] private GameObject leftOption3;

    [Header("Right Option Game Objects")]
    [SerializeField] private GameObject rightOption1;
    [SerializeField] private GameObject rightOption2;
    [SerializeField] private GameObject rightOption3;

    [Header("Arrow Game Objects")]
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    [Header("Criteria")]
    [SerializeField] private Dictionary<int, string> criteria = new Dictionary<int, string>();

    private string randomCriteria1;

    private void Start()
    {
        RepopulateCriteriaDictionary();
        SelectCriteria();
    }

    private void RepopulateCriteriaDictionary()
    {
        criteria.Clear();

        float randomPrice = CalculatePrice();

        criteria.Add(1, "Fruit");
        criteria.Add(2, "Not Fruit");
        criteria.Add(3, "<" + randomPrice);
        criteria.Add(4, "=" + randomPrice);
        criteria.Add(5, ">" + randomPrice);
        criteria.Add(6, "Not Red");
        criteria.Add(7, "Not Yellow");
        criteria.Add(8, "Not Green");
        criteria.Add(9, "Not Drink");
        criteria.Add(10, "Red");
        criteria.Add(11, "Yellow");
        criteria.Add(12, "Green");
        criteria.Add(13, "Drink");
        criteria.Add(14, "Not single");
        criteria.Add(15, "Single");
    }

    private float CalculatePrice()
    {
        float randomPrice = 0;

        randomPrice = Random.Range(0.05f, 10.0f);

        return randomPrice;
    }

    private void SelectCriteria()
    {
        int randNumber = Random.Range(0, criteria.Count);
        randomCriteria1 = criteria[randNumber];

        Vector3 pos = new Vector3(leftOption1.transform.position.x, 1.0f, 0.0f);
        
    }
}