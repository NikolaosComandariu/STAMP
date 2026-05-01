using UnityEngine;
using TMPro;


public class ScoreFeedback : MonoBehaviour
{
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private ObjectSpawner objectSpawner2;

    public float floatSpeed = 1f;
    public float lifetime = 1f;
    public TextMeshProUGUI text;
    public GameObject floatingTextPrefab;
    private TextMeshProUGUI floatingText;

    void Start()
    {
        //Destroy(gameObject, lifetime);
    }



    //public void SpawnText()
    //{
    //    Vector3 spawnPos = objectSpawner.currentObject.transform.position;

    //    //TextMeshProUGUI floatText = Instantiate(text, spawnPos, Quaternion.identity);
    //    //floatText.SetText("+1");

    //    GameObject floatText = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity);
    //    floatingText = floatText.GetComponent<TextMeshProUGUI>;
    //}


    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void SetText(string value)
    {
        text.text = value;
    }
}
