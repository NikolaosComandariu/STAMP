using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;


public class ObjectPrototype_ : MonoBehaviour
{

    [SerializeField]private float speed;
    private Transform transform;
    [SerializeField]private float timer = 0.0f;
    [SerializeField] private Text pricetags;

    //GDD variables
    [SerializeField] private bool IsFruit;
    [SerializeField] private bool IsRed;
    [SerializeField] private bool IsGreen;
    [SerializeField] private bool IsYellow;
    [SerializeField] private bool IsSingle;

    //TODO: PRICE VARIABLES

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveRight();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10.0f)
        {
            Destroy(gameObject);
        }
    }

    private void moveRight()
    {
        //transform.position = new Vector3((transform.position.x 
        // + speed) * Time.deltaTime, transform.position.y,
        //transform.position.z);
        //transform.position = pos;

        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private float calculatePrice()
    {
        float randomPrice = 0.0f;

        randomPrice = UnityEngine.Random.Range(0.05f, 10.0f);

        return randomPrice;
    }

    private void UpdatePricetag()
    {
        float price = calculatePrice();
        pricetags.text = price.ToString("F2");
    }

    private bool checkIsFruit() { return IsFruit; }
    private bool checkIsRed() { return IsRed; }
    private bool checkIsGreen() { return IsGreen; }
    private bool checkIsYellow() { return IsYellow; }
    private bool checkIsSingle() { return IsSingle; }
    
}
