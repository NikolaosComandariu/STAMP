using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private float MoveForce;
    [SerializeField] private List<int> criteriaList = new List<int>();
    [SerializeField] private RoundCondition roundCondition;
    [SerializeField] private GameObject currentObject;
    [SerializeField] private int score = 0;

    [Header("Game Objects")]
    [SerializeField] private List<GameObject> ObjectsPool = new List<GameObject>(); // Amount of objects in the round
    [SerializeField] private List<GameObject> AllPossibleObjects; // All prefabs possible to spawn

    [Header("Transforms")]
    [SerializeField] private Transform EndOfConveyor; // Stopping point of objects where they're ready to be accepted / declined
    [SerializeField] private Transform SpawnPos; // Off screen spawnpoint for objects to then scroll onto screen
    [SerializeField] private Transform DeclinedP1; //code by Smriti
    [SerializeField] private Transform AcceptedP1; //code by Smriti

    [Header("Text")] 
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Events")]
    public System.Action onAllObjectsProcessed; // Nikolaos Comandariu.

    // Buttons
    private Button Accept;
    private Button Decline;

    // Int
    private int NumOfObjToSpawn; // tally of items left to spawn
    private int objToSpawn;

    // Booleans
    private bool AllowObjSpawn;
    private bool isSpawning = false;
    private bool AllowDecision = false; //smriti added this

    private Item item; // This isn't used anywhere, can be removed.
    private Rigidbody2D rb2D;
    private int[] CriteriaGenerated;//smriti added this; possible to remove if not used
    private bool generatingNumber; //smriti added this; possible to remove if not used

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
        Single,
        Orange, //options added by smriti
        Drink,
        NotFruit,
        NotRed,
        NotGreen,
        NotYellow,
        NotSingle,
        NotOrange,
        NotDrink //end of options added by smrti
    }

    private void Start()
    {
        //NumOfObjToSpawn = ObjectsPool.Count;
        objToSpawn = 5;
        AllowObjSpawn = true;
        //StartCoroutine(SpawnObject());
    }

    private void OnEnable()
    {
        CriteriaManager.OnCriteriaDecided += SetCriteria;
    }

    private void OnDisable()
    {
        CriteriaManager.OnCriteriaDecided -= SetCriteria;
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
                    int n = Random.Range(0, ObjectsPool.Count);

                    currentObject = Instantiate(ObjectsPool[n], SpawnPos.position, ObjectsPool[n].transform.rotation);
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

        NumOfObjToSpawn = ObjectsPool.Count;
        Debug.Log("Num of obj to spawn: " + NumOfObjToSpawn);
    }
    
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

        /*
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
            //added code by smriti
            case RoundCondition.Orange:
                isMatch = proto.checkIsOrange();
                break;
            case RoundCondition.Drink:
                isMatch = proto.checkIsDrink();
                break;
            //end of added code by smriti
        }*/

        for (int i = 0; i < criteriaList.Count; i++)
        {
            int x = criteriaList[i];

            if (x <= 0) break;

            // Set current round condition
            roundCondition = (RoundCondition)x;

            switch (roundCondition)
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
                //added code by smriti
                case RoundCondition.Orange:
                    isMatch = proto.checkIsOrange();
                    break;
                case RoundCondition.Drink:
                    isMatch = proto.checkIsDrink();
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
                    currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position,
                        AcceptedP1.position, MoveForce * Time.deltaTime);
                    //Destroy(currentObject);

                    AllowDecision = false;

                    //end of code by Smriti
                }
                //AllowObjSpawn = true;
                //StartCoroutine(SpawnObject());
            }
        }

        Destroy(currentObject);
        currentObject = null;
        AllowObjSpawn = true;
        //StartCoroutine(SpawnObject());
        Debug.Log("Object should be destroyed");

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
      
        for (int i = 0; i < criteriaList.Count; i++)
        {
            int x = criteriaList[i];

            if (x <= 0) break;

            // Set current round condition
            roundCondition = (RoundCondition)x;

            switch (roundCondition)
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
                //added code by smriti
                case RoundCondition.Orange:
                    isMatch = proto.checkIsOrange();
                    break;
                case RoundCondition.Drink:
                    isMatch = proto.checkIsDrink();
                    break;
            } //end of added code by smriti

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
                    currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position,
                        AcceptedP1.position, MoveForce * Time.deltaTime);
                    //Destroy(currentObject);

                    AllowDecision = false;

                    //end of code by Smriti
                }
                //AllowObjSpawn = true;
                //StartCoroutine(SpawnObject());
            }
        }

        /*if (AllowDecision)
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

             if (currentObject.transform.position == DeclinedP1.position)
             {
                 currentObject.SetActive(false);
             }
            //Destroy(currentObject);
            AllowDecision = false;

            //end code by smriti
        } */

        Destroy(currentObject);
        currentObject = null;
        AllowObjSpawn = true;
        //StartCoroutine(SpawnObject());

        /*
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
        }*/

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

    private void SetCriteria(int crit1, int crit2, int crit3, int crit4)
    {
        criteriaList.Add(crit1);
        criteriaList.Add(crit2);
        criteriaList.Add(crit3);
        criteriaList.Add(crit4);
    }

    // End of code from Nikolaos Comandariu.
}