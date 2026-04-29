using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private float MoveForce;
    [SerializeField] private RoundCondition currentRoundCondition;
    [SerializeField] private GameObject currentObject;
    [SerializeField] private int score = 0;

    [Header("Game Objects")]
    [SerializeField] private List<GameObject> ObjectsPool = new List<GameObject>(); // Amount of objects in the round
    [SerializeField] private List<GameObject> AllPossibleObjects; // All prefabs possible to spawn
    [SerializeField] private List<GameObject> AllGlitchedObjects; // All prefabs possible to spawn

    [Header("Transforms")]
    [SerializeField] private Transform EndOfConveyor; // Stopping point of objects where they're ready to be accepted / declined
    [SerializeField] private Transform SpawnPos; // Off screen spawnpoint for objects to then scroll onto screen
    [SerializeField] private Transform DeclinedP1; //code by Smriti
    [SerializeField] private Transform AcceptedP1; //code by Smriti

    [Header("Text")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI productPrice;

    [Header("Events")]
    public System.Action onAllObjectsProcessed; // Nikolaos Comandariu.

    // Buttons
    private Button Accept;
    private Button Decline;

    // Int
    private int NumOfObjToSpawn; // tally of items left to spawn
    private int objToSpawn;
    private int TotalItemsSpawned; // Amount of items spawned in current game in totality - never resets

    // Booleans
    private bool AllowObjSpawn;
    private bool isSpawning = false;
    private bool AllowDecision = false; //smriti added this
    private bool P2LastGlitchedItem = false;
    private bool P1LastGlitchedItem = false;

    private Item item; // This isn't used anywhere, can be removed.
    private Rigidbody2D rb2D;

    /// <summary>
    /// Used to compare the criteria to the object to see if the
    /// player has gotten their choice right.
    /// </summary>
    public enum RoundCondition
    {
        Fruit,
        Red,
        Green,
        Yellow,
        Single
    }

    private void Start()
    {
        //NumOfObjToSpawn = ObjectsPool.Count;
        objToSpawn = 5;
        AllowObjSpawn = true;
        //StartCoroutine(SpawnObject());
    }

    private void Update()
    {
        /*
        // If number of objects to spawn is 0, restart spawning.
        if (NumOfObjToSpawn == 0)
        {
            //roundNumber++;
            GenerateObjectsForRound();
            AllowObjSpawn = true;
            StartCoroutine(SpawnObject());
        }*/

    }

    /// <summary>
    /// While objects can spawn and there are still some objects that need to spawn,
    /// Get a random object from the object pool. Reduce number of objects to spawn
    /// and apply force to the last spawned object to move it down the conveyor belt.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnObject()
    {
        for (int i = 0; i < ObjectsPool.Count; i++)
        {
            while (AllowObjSpawn)
            {
                if (NumOfObjToSpawn != 0)
                {
                    TotalItemsSpawned++;

                    int n = Random.Range(0, ObjectsPool.Count);

                    currentObject = Instantiate(ObjectsPool[n], SpawnPos.position, 
                        ObjectsPool[n].transform.rotation, gameObject.transform);
                    productPrice.text = "£" + 
                        currentObject.GetComponent<ObjectPrototype_>().GetPrice().ToString();
                    Debug.Log("spawn object");
                    //item = currentObject.GetComponent<Item>(); // Can be removed, item does not seem to be used anywhere.
                    ObjectsPool.RemoveAt(n);
                    NumOfObjToSpawn--;

                    MoveToTarget mover = currentObject.GetComponent<MoveToTarget>();
                    mover.SetTarget(EndOfConveyor);
                    mover.SetSpeed(MoveForce);

                    Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
                    rb.transform.position = Vector2.MoveTowards(SpawnPos.position, EndOfConveyor.position, MoveForce * Time.deltaTime);

                    AllowDecision = true;
                    AllowObjSpawn = false;

                    yield return null;
                }
            }
        }
    }

    /// <summary>
    /// Clears object pool, gets random objects from
    /// all possible objects to spawn.
    /// </summary>
    public void GenerateObjectsForRound() 
    {
        ObjectsPool.Clear();

        //int itemsThisRound = objToSpawn;
        // round 1 = 3 items
        // Round 2 = 5 items
        // etc
        Debug.Log("Obj to spawn: " + objToSpawn);
        // Repopulate ObjectsPool.
        for (int i = 0; i < objToSpawn; i++)
        {
            Debug.Log("Generating Objects for round");
            int randomIndex = Random.Range(0, AllPossibleObjects.Count);
            ObjectsPool.Add(AllPossibleObjects[randomIndex]);
        }
        if (TotalItemsSpawned == 5)
        {
            Debug.Log("Attempting to spawn glitched item");
            DecidePlayerForGlitchedItem();
        }

            NumOfObjToSpawn = ObjectsPool.Count;
        Debug.Log("Num of obj to spawn: " + NumOfObjToSpawn);
    }

    private void DecidePlayerForGlitchedItem()
    {
        if (!P1LastGlitchedItem && !P2LastGlitchedItem)
        {
            int random5050 = Random.Range(0, 1);


            if (random5050 > .5)
            {
                //GivePlayer1GlitchedItem();
            }
            else
            {
                GivePlayer2GlitchedItem();
            }


        }
    }

    private void GivePlayer2GlitchedItem()
    {

        if (AllGlitchedObjects.Count == 0)
        {
            Debug.LogError("No objects with tag 'Glitched' found in the scene!");
            return;
        }

        int ranGlitchedItem = Random.Range(0, AllGlitchedObjects.Count);
        GameObject glitchedItem = AllGlitchedObjects[ranGlitchedItem];


        int randomListIndex = Random.Range(0, ObjectsPool.Count);
        var removed = ObjectsPool[randomListIndex];
        //ObjectsPool.Remove(removed);
        ObjectsPool[randomListIndex] = glitchedItem;

    }

    //private void GivePlayer1GlitchedItem()
    //{
    //    GameObject[] glitchedObjects = GameObject.FindGameObjectsWithTag("Glitched");

    //    int ranGlitchedItem = Random.Range(0, glitchedObjects.Length);
    //    GameObject glitchedItem = glitchedObjects[ranGlitchedItem];


    //    int randomListIndex = Random.Range(0, ObjectsPool.Count);
    //    var removed = ObjectsPool[randomListIndex];
    //    //ObjectsPool.Remove(removed);
    //    ObjectsPool[randomListIndex] = glitchedItem;
    //}




       // optional: store removed item


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

    /// <summary>
    /// It compares the object that was accepted against the criteria,
    /// if it's a match it increases the score by 1, otherwise it decreases
    /// the score by 1. It then destroys the object and either spawns another one
    /// or tells the GameManager that the round is over through a Unity Action.
    /// </summary>
    public void AcceptObject()
    {
        Debug.Log("accept clicked");

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
            score += 1; // Score should not be in ObjectSpawner ideally, might need to refactor later.
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
                acceptedP1.SetTarget(AcceptedP1);
                acceptedP1.SetSpeed(MoveForce);
                //currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position, 
                    //AcceptedP1.position, MoveForce * Time.deltaTime);

                //Destroy(currentObject);
                
                AllowDecision = false;

                //end of code by Smriti
            }
            //AllowObjSpawn = true;
            //StartCoroutine(SpawnObject());
        }

        Destroy(currentObject);
        currentObject = null;
        AllowObjSpawn = true;
        //StartCoroutine(SpawnObject());

        // Nikolaos Comandariu
        if (NumOfObjToSpawn <= 0 && currentObject == null) // Can be turned into an inverted if statement.
        {
            Debug.Log("All Objects Processed Event");
            onAllObjectsProcessed?.Invoke();
        } // End of code added.
        else
        {
            StartCoroutine(SpawnObject());
        }
    }

    /// <summary>
    /// It compares the object that was declined against the criteria,
    /// if it's a match it increases the score by 1, otherwise it decreases
    /// the score by 1. It then destroys the object and either spawns another one
    /// or tells the GameManager that the round is over through a Unity Action.
    /// </summary>
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
        }

        if (AllowDecision)
        {
            //code by smriti
            MoveToTarget declinedP1 = currentObject.GetComponent<MoveToTarget>();
            declinedP1.SetTarget(DeclinedP1);
            declinedP1.SetSpeed(MoveForce);

            // Rigidbody2D testrb = currentObject.GetComponent<Rigidbody2D>();
            //testrb.transform.position = Vector2.MoveTowards(EndOfConveyor.position,DeclinedP1.position, MoveForce * Time.deltaTime);

            currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position, 
                DeclinedP1.position, MoveForce * Time.deltaTime);

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
            AllowDecision = false;

            //end code by smriti
        }

        Destroy(currentObject);
        currentObject = null;
        AllowObjSpawn = true;
        //StartCoroutine(SpawnObject());

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

        //AllowObjSpawn = true;
        // Nikolaos Comandariu
        if (NumOfObjToSpawn <= 0 && currentObject == null) // Can be turned into an inverted if statement.
        {
            onAllObjectsProcessed?.Invoke();
        } // End of code added.
        else
        {
            StartCoroutine(SpawnObject());
        }
    }

    /// <summary>
    /// Updates scoreText to show the current score.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    // Code from Nikolaos Comandariu.

    /// <summary>
    /// Changes number of objects spawned, will be used when
    /// increasing difficulty.
    /// </summary>
    /// <param name="num"></param>
    public void ChangeNumberOfObjectsSpawned(int num)
    {
        objToSpawn = num;
    }

    /// <summary>
    /// Changes boolean to allow objects to spawn.
    /// </summary>
    /// <param name="allow"></param>
    public void ChangeAllowToSpawn(bool allow)
    {
        AllowObjSpawn = allow;
    }

    public void ResetObjects()
    {
        Destroy(currentObject);
        currentObject = null;
        GenerateObjectsForRound();
    }

    // End of code from Nikolaos Comandariu.
}