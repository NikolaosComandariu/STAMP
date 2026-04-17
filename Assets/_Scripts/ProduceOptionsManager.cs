using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class ProduceOptionsManager : MonoBehaviour
{
    [Header("Left Option Game Objects")]
    [SerializeField] private GameObject leftOptionsHolder;
    [SerializeField] private GameObject[] leftOptions;
    
    [Header("Right Option Game Objects")]
    [SerializeField] private GameObject rightOptionsHolder;
    [SerializeField] private GameObject[] rightOptions;

    [Header("Arrow Game Objects")]
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    [Header("Criteria")]
    [SerializeField] private Dictionary<int, string> criteria = new Dictionary<int, string>();

    [Header("Variables")]
    [SerializeField] private float timeBetweenOptions;

    [Header("Text")]
    [SerializeField] private TMP_Text leftText1;
    [SerializeField] private TMP_Text leftText2;
    [SerializeField] private TMP_Text leftText3;
    [SerializeField] private TMP_Text rightText1;
    [SerializeField] private TMP_Text rightText2;
    [SerializeField] private TMP_Text rightText3;

    // Random criteria selected for each player.
    private string leftCriteria;
    private string rightCriteria;

    // Keep track of current selected option for each player.
    private GameObject currentLeftOption;
    private GameObject currentRightOption;

    // Keep track of current option index.
    private int currentLeftIndex;
    private int currentRightIndex;

    private void Start()
    {
        // Set current options to starting values.
        currentLeftOption = leftOptions[0];
        currentRightOption = rightOptions[^1]; // Index operator allows you to access things from the end.

        // Set current index to starting values.
        currentLeftIndex = 0;
        currentRightIndex = rightOptions.Length - 1;

        RepopulateCriteriaDictionary();
        SelectCriteria();
    }

    private void Update()
    {
        ChangeCurrentSelectedOption();
    }

    /// <summary>
    /// Adds all criteria to the dictionary.
    /// </summary>
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

    /// <summary>
    /// Populates the options containers
    /// </summary>
    private void PopulateOptionsContainers()
    {
        int leftChildCount = leftOptionsHolder.transform.childCount;
        int rightChildCount = rightOptionsHolder.transform.childCount;

        for (int i = 0; i < leftChildCount; i++)
        {
            Transform child = leftOptionsHolder.transform.GetChild(i);
            //leftOptions.
        }
    }

    /// <summary>
    /// Returns a float between 0.05 and 10.
    /// </summary>
    /// <returns></returns>
    private float CalculatePrice()
    {
        float randomPrice = 0;

        randomPrice = Random.Range(0.05f, 10.0f);

        return randomPrice;
    }

    /// <summary>
    /// Selects random criteria from the dictionary.
    /// TODO: Replace this, this is hardcoded currently.
    /// </summary>
    private void SelectCriteria()
    {
        for(int i = 0; i < 6; i++)
        {
            int randNumber = Random.Range(1, criteria.Count);
            leftCriteria = criteria[randNumber];

            switch (i)
            {
                case 0:
                    leftText1.text = leftCriteria.ToString();
                    break;
                case 1:
                    leftText2.text = leftCriteria.ToString();
                    break;
                case 2:
                    leftText3.text = leftCriteria.ToString();
                    break;
                case 3:
                    rightText1.text = leftCriteria.ToString();
                    break;
                case 4:
                    rightText2.text = rightCriteria.ToString();
                    break;
                case 5:
                    rightText3.text = rightCriteria.ToString();
                    break;
            }
        }
    }

    /// <summary>
    /// Changes sprite colour to parameter's colour.
    /// This will be used to tell the player if they got something wrong
    /// or right.
    /// </summary>
    /// <param name="colour"></param>
    private void ChangeLeftOptionColour(Color colour)
    {
        var LeftSpriteRender = currentLeftOption.GetComponent<SpriteRenderer>();

        LeftSpriteRender.material.color = colour;
    }

    /// <summary>
    /// Changes sprite colour to parameter's colour.
    /// This will be used to tell the player if they got something wrong
    /// or right.
    /// </summary>
    /// <param name="colour"></param>
    private void ChangeRightOptionColour(Color colour)
    {
        var RightSpriteRender = currentLeftOption.GetComponent<SpriteRenderer>();

        RightSpriteRender.material.color = colour;
    }

    private void MoveArrow()
    {
        Debug.Log("Move Arrow");
        leftArrow.transform.position = new Vector3(currentLeftOption.transform.position.x,
            leftArrow.transform.position.y, leftArrow.transform.position.z);
        rightArrow.transform.position = new Vector3(currentRightOption.transform.position.x,
            rightArrow.transform.position.y, rightArrow.transform.position.z);
    }

    /// <summary>
    /// Change currently selected option every x seconds.
    /// </summary>
    private void ChangeCurrentSelectedOption()
    {
        timeBetweenOptions = timeBetweenOptions - Time.deltaTime;

        if (timeBetweenOptions < 0)
        {
            currentLeftIndex++;
            currentRightIndex--;

            // Change back to starting position.
            if (currentLeftIndex > leftOptions.Length - 1)
                currentLeftIndex = 0;

            // Change back to starting position.
            if(currentRightIndex < 0)
                currentRightIndex = rightOptions.Length - 1;

            currentLeftOption = leftOptions[currentLeftIndex];
            currentRightOption = rightOptions[currentRightIndex];

            timeBetweenOptions = 1;
            MoveArrow();
        }
    }
}