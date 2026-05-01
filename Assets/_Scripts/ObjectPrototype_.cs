using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectPrototype_ : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]private float speed = 1.0f;
    [SerializeField]private float timer = 0.0f;
    [SerializeField] private float centreDiff = 5.0f; //can be reomved after testing
    [SerializeField] private float ySpd = 0.03f; //these serialised fields can be removed after testing
    [SerializeField] private float xSpd = 0.01f; //these serialised fields can be removed after testing
    [SerializeField] private bool isAccepted; //trying out movement for rejcted items
    [SerializeField] private float price;

    //[Header("Text")]
    //[SerializeField] private TextMeshProUGUI priceTag;

    [Header("GDD Variables")]
    [SerializeField] private bool IsFruit;
    [SerializeField] private bool IsRed;
    [SerializeField] private bool IsGreen;
    [SerializeField] private bool IsYellow;
    [SerializeField] private bool IsSingle;
    [SerializeField] private bool IsOrange;
    [SerializeField] private bool IsDrink;

    private Transform transform2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //priceTag.text = "£" + price.ToString();
        transform2 = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveRight();
    }

    /*private void Update()
    {
        //timer += Time.deltaTime;
        //if (timer > 10.0f)
        //{
            //Destroy(gameObject);
        //}
    }*/

    private void moveRight()
    {
        Vector3 pos = new Vector3(transform.position.x + speed,transform.position.y,
            transform.position.z);
        transform.position = pos;
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
       // priceTag.text = price.ToString("F2");
    }

    public bool checkIsFruit() { return IsFruit; }
    public bool checkIsRed() { return IsRed; }
    public bool checkIsGreen() { return IsGreen; }
    public bool checkIsYellow() { return IsYellow; }
    public bool checkIsSingle() { return IsSingle; }
    public bool checkIsOrange() {  return IsOrange; }
    public bool checkIsDrink() {  return IsDrink; }

    //testing new fucntions
    private void moveUp()
    {
        //needs to see if the object is coming in from the left or the right
        if (transform.position.x < 0)
        {
            xSpd *= 1.0f; //object is moving to the right
        }
        else
        {
            xSpd *= -1.0f; //object is moving to the left
        }

        if (!isAccepted) return;

        Vector3 declinedPos = new Vector3(transform.position.x + xSpd, transform.position.y 
            + ySpd, transform.position.z);
    }

    public float GetPrice()
    {
        return price;
    }
}
