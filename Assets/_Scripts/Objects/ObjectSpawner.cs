using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using System.Threading;
using System.Threading.Tasks;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private float MoveForce;
    [SerializeField] private List<GameObject> ObjectsPool = new List<GameObject>(); // Amount of objects in the round
    [SerializeField] private Transform EndOfConveyor; // Stopping point of objects where they're ready to be accepted / declined
    [SerializeField] private Transform SpawnPos; // Off screen spawnpoint for objects to then scroll onto screen

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    private Button Accept;
    private Button Decline;

    private int NumOfObjToSpawn; // tally of items left to spawn
    private bool AllowObjSpawn;

    public GameObject currentObject;

    public int score = 0;

    Item item;
    private bool AllowDecision = false; //smriti added this

    Rigidbody2D rb2D;

    void Start()
    {
        NumOfObjToSpawn = ObjectsPool.Count;

        AllowObjSpawn = true;
        StartCoroutine(SpawnObject());
    }

    void Update()
    {
        if (NumOfObjToSpawn == 0)
        {
            AllowObjSpawn = false;
        }
    }

    IEnumerator SpawnObject()
    {
        for (int i = 0; i < ObjectsPool.Count; i++)
        {
            while (AllowObjSpawn)
            {
                int n = Random.Range(0, ObjectsPool.Count);

                currentObject = Instantiate(ObjectsPool[n], SpawnPos.position, ObjectsPool[n].transform.rotation);
                item = currentObject.GetComponent<Item>();
                ObjectsPool.RemoveAt(n);
                NumOfObjToSpawn--;

                MoveToTarget mover = currentObject.GetComponent<MoveToTarget>();
                mover.target = EndOfConveyor;
                mover.speed = MoveForce;

                Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
                rb.transform.position = Vector2.MoveTowards(SpawnPos.position, EndOfConveyor.position, MoveForce * Time.deltaTime);

                AllowDecision = true;
                AllowObjSpawn = false;



                yield return null;

            }
        }
    }

    public enum RoundCondition
    {
        Fruit,
        Red,
        Green,
        Yellow,
        Single
    }

    public RoundCondition currentRoundCondition;
    //code by Smriti
    [SerializeField] private Transform DeclinedP1;
    [SerializeField] private Transform AcceptedP1;
    //public DeclinedTrigger trigger;
    //private Collision2D collision;

    /* public void OnCollisionEnter2D(Collision2D collision)
     {
         if(collision.gameObject.CompareTag("DeclinedTrigger"))
         {
             Debug.Log("Collision");
             //currentObject.SetActive(false);
         }
     }*/
    //end off code by Smriti

    public void AcceptObject()
    {
        print ("accept clicked");
        if (currentObject == null)
            return;

        ObjectPrototype_ proto = currentObject.GetComponent<ObjectPrototype_>();

        bool isMatch = false;

        switch (currentRoundCondition)
        {
            case RoundCondition.Fruit:
                isMatch = proto.checkIsFruit();
                break;
            case RoundCondition.Red:
                isMatch = proto.checkIsRed();
                break;
            case RoundCondition.Green:
                isMatch = proto.checkIsGreen();
                break;
            case RoundCondition.Yellow:
                isMatch = proto.checkIsYellow();
                break;
            case RoundCondition.Single:
                isMatch = proto.checkIsSingle();
                break;
        }

        if (isMatch)
        {
            Debug.Log("ACCEPT: Correct choice!");
            score += 1;
            UpdateScoreUI();
            Debug.Log("Correct! Score is now: " + score);
        }
        else
        {
            Debug.Log("Wrong choice!");
            score -= 1;
            UpdateScoreUI();
            Debug.Log("Wrong, Score is now: " + score);
            //code by Smriti
            if (AllowDecision)
            {
                MoveToTarget acceptedP1 = currentObject.GetComponent<MoveToTarget>();
                acceptedP1.target = AcceptedP1;
                acceptedP1.speed = MoveForce;
                currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position, AcceptedP1.position, MoveForce * Time.deltaTime);
                //Destroy(currentObject);
                
                AllowDecision = false;

                //end of code by Smriti
            }
            AllowObjSpawn = true;
            StartCoroutine(SpawnObject());
        }

        Destroy(currentObject);
        AllowObjSpawn = true;
        StartCoroutine(SpawnObject());
    }


    public void DeclineObject()
    {
        print("Decline clicked");
        if (currentObject == null)
            return;

        ObjectPrototype_ proto = currentObject.GetComponent<ObjectPrototype_>();

        bool isMatch = false;

        switch (currentRoundCondition)
        {
            case RoundCondition.Fruit:
                isMatch = proto.checkIsFruit();
                break;
            case RoundCondition.Red:
                isMatch = proto.checkIsRed();
                break;
            case RoundCondition.Green:
                isMatch = proto.checkIsGreen();
                break;
            case RoundCondition.Yellow:
                isMatch = proto.checkIsYellow();
                break;
            case RoundCondition.Single:
                isMatch = proto.checkIsSingle();
                break;
            if (AllowDecision)
            {
                //code by smriti
                MoveToTarget declinedP1 = currentObject.GetComponent<MoveToTarget>();
                declinedP1.target = DeclinedP1;
                declinedP1.speed = MoveForce;
                // Rigidbody2D testrb = currentObject.GetComponent<Rigidbody2D>();
                //testrb.transform.position = Vector2.MoveTowards(EndOfConveyor.position,DeclinedP1.position, MoveForce * Time.deltaTime);
                currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position, DeclinedP1.position, MoveForce * Time.deltaTime);
                //OnCollisionEnter2D(collision);
                //trigger.
                //GameObject oldObject = currentObject;
                //Thread.Sleep(5000);
                //currentObject.SetActive(false);

                /* if (currentObject.transform.position == DeclinedP1.position)
                 {
                     currentObject.SetActive(false);
                 }*/
                //Destroy(currentObject);
                AllowDecision= false;

                //end code by smriti
            }
            AllowObjSpawn = true;
            StartCoroutine(SpawnObject());
        }

        if (!isMatch)
        {
            Debug.Log("Correct choice");
            score += 1;
            UpdateScoreUI();
            Debug.Log("Correct, Score is now: " + score);
        }
        else
        {
            Debug.Log("Wrong choice!");
            score -= 1;
            UpdateScoreUI();
            Debug.Log("Wrong, Score is now: " + score);
        }

        Destroy(currentObject);
        AllowObjSpawn = true;
        StartCoroutine(SpawnObject());
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
