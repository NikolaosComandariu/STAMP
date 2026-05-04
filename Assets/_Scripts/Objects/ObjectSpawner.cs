using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using System;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private float MoveForce;
    [SerializeField] private RoundCondition currentRoundCondition;
    [SerializeField] public GameObject currentObject;
    [SerializeField] private List<int> criteriaList = new List<int>();
    [SerializeField] private RoundCondition roundCondition;
    //[SerializeField] private GameObject currentObject;
    [SerializeField] private int score = 0;
    [SerializeField] private bool IsPlayer1; //smriti added this

    [Header("Game Objects")]
    [SerializeField] private List<GameObject> ObjectsPool = new List<GameObject>(); // Amount of objects in the round
    [SerializeField] private List<GameObject> GlitchedItemsPool = new List<GameObject>(); // Amount of glitched objects 
    [SerializeField] private List<GameObject> AllPossibleObjects; // All prefabs possible to spawn
    [SerializeField] private ScoreManager scoreManager; //smriti added this
    [SerializeField] private GameObject ScoreTextFeedback;

    [Header("Transforms")]
    [SerializeField] private Transform EndOfConveyor; // Stopping point of objects where they're ready to be accepted / declined
    [SerializeField] private Transform SpawnPos; // Off screen spawnpoint for objects to then scroll onto screen
    [SerializeField] private Transform DeclinedP1; //code by Smriti
    [SerializeField] private Transform AcceptedP1; //code by Smriti

    [Header("Text")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI productPrice;

    [Header("Probability - Glitched items")]
    [SerializeField] private int upperLimit;

    [Header("Events")]
    public System.Action onAllObjectsProcessed; // Nikolaos Comandariu.
    public static event Action<int> OnTallyUpScores;

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
    private bool SpawnGlitchedItem = false;
    private bool rhythmPoints; // Nikolaos Comandariu.
    private bool generatingNumber; //smriti added this; possible to remove if not used

    private Vector3 CurrentObjLoc;

    private Item item; // This isn't used anywhere, can be removed.
    private Rigidbody2D rb2D;
    private int[] CriteriaGenerated;//smriti added this; possible to remove if not used

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
    
    // Nikolaos Comandariu.
    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onGameOver += TallyUpScores;

        if(IsPlayer1)
        {
            RhythmHitbox.onColliderEnteredP1 += AcceptRhythmPoints;
        }
        else
        {
            RhythmHitbox.onColliderEnteredP2 += AcceptRhythmPoints;
        }
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onGameOver -= TallyUpScores;

        if (IsPlayer1)
        {
            RhythmHitbox.onColliderEnteredP1 -= AcceptRhythmPoints;
        }
        else
        {
            RhythmHitbox.onColliderEnteredP2 -= AcceptRhythmPoints;
        }
    }

    // End of Nikolaos Comandariu.

    private void Start()
    {
        //NumOfObjToSpawn = ObjectsPool.Count;
        objToSpawn = 5;
        AllowObjSpawn = true;
        rhythmPoints = false;
        //StartCoroutine(SpawnObject());
    }

    private void Update()
    {
        if (currentObject != null)
        {
            CurrentObjLoc = currentObject.transform.position;
        }
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

                    currentObject = Instantiate(ObjectsPool[n], SpawnPos.position, 
                        ObjectsPool[n].transform.rotation, gameObject.transform);

                    productPrice.text = "£" + 
                        currentObject.GetComponent<ObjectPrototype_>().GetPrice().ToString();
                    //Debug.Log("spawn object");
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
    private void ChanceToSpawnGlitchedItem()
    {
        int chance = Random.Range(1, upperLimit);
        if (chance == 1)
        {
            SpawnGlitchedItem = true;
        }
        else
        {
            return;
        }
    }

    //public void DisplayTextFeedback( int amount, Transform currentObject)
    //{
    //    TextMeshPro scoreTextFeedback = Instantiate (ScoreTextFeedback);
    //    scoreTextFeedback.transform.position = currentObject.position;

    //}

    //public void DisplayTextFeedback(int amount, Vector3 CurrentObject)
    //{
    //    GameObject instance = Instantiate(ScoreTextFeedback, currentObject.transform.position, Quaternion.identity);
    //    TextMeshProUGUI tmp = instance.GetComponent<TextMeshProUGUI>();
    //    tmp.text = "+" + amount;
    //}

    //public void DisplayTextFeedback(int amount, Vector3 position)
    //{
    //    GameObject instance = Instantiate(ScoreTextFeedback, position, Quaternion.identity);

    //    TextMeshProUGUI tmp = instance.GetComponent<TextMeshProUGUI>();
    //    tmp.text = "+" + amount;
    //}

    public void DisplayTextFeedback(int amount, Vector3 position, Color color)
    {
        GameObject instance = Instantiate(ScoreTextFeedback, position, Quaternion.identity);

        TextMeshPro tmp = instance.GetComponent<TextMeshPro>();
        tmp.text = "+" + amount;
        tmp.color = color;
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
        //Debug.Log("Obj to spawn: " + objToSpawn);
        // Repopulate ObjectsPool.
        for (int i = 0; i < objToSpawn; i++)
        {
            //Debug.Log("Generating Objects for round");
            int randomIndex = Random.Range(0, AllPossibleObjects.Count);
            ObjectsPool.Add(AllPossibleObjects[randomIndex]);
            ChanceToSpawnGlitchedItem();
        }
        if (SpawnGlitchedItem == true)
        {
            int index = Random.Range(0, ObjectsPool.Count);
            int index2 = Random.Range(0, GlitchedItemsPool.Count);

            ObjectsPool[index] = GlitchedItemsPool[index2];
            SpawnGlitchedItem = false;
        }    

        NumOfObjToSpawn = ObjectsPool.Count;
        //Debug.Log("Num of obj to spawn: " + NumOfObjToSpawn);
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
        //Debug.Log("accept clicked");

        if (currentObject == null) 
            return;

        ObjectPrototype_ proto = currentObject.GetComponent<ObjectPrototype_>();

        bool isMatch = false;

        //code by Smriti

        for (int i = 0; i < criteriaList.Count; i++)
        {
            int x = criteriaList[i];
            Debug.Log("X: " + x);
            if (x < 0) break;

            // Set current round condition
            roundCondition = (RoundCondition)x-1;

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
                case RoundCondition.NotFruit:
                    isMatch = !proto.checkIsFruit();
                    break;
                case RoundCondition.NotRed:
                    isMatch = !proto.checkIsRed();
                    break;
                case RoundCondition.NotGreen:
                    isMatch = !proto.checkIsGreen();
                    break;
                case RoundCondition.NotYellow:
                    isMatch = !proto.checkIsYellow();
                    break;
                case RoundCondition.NotSingle:
                    isMatch = !proto.checkIsSingle();
                    break;
                case RoundCondition.NotOrange:
                    isMatch = !proto.checkIsOrange();
                    break;
                case RoundCondition.NotDrink:
                    isMatch = !proto.checkIsDrink();
                    break;
            }

            //Debug.Log("Is match: " + isMatch);
            //Debug.Log("Round condition: " + roundCondition);

            if (isMatch)
            {
                //Debug.Log("ACCEPT: Correct choice!");

                if(rhythmPoints) // Nikolaos Comandariu.
                {
                    score += 1;
                }

                score += 1; // Score should not be in ObjectSpawner ideally, might need to refactor later.
                DisplayTextFeedback(+1, CurrentObjLoc, Color.green);
                UpdateScoreUI();
                Debug.Log("Correct! Score is now: " + score);
            }
            else if(!isMatch)
            {
                //Debug.Log("Wrong choice!");
                score -= 1;
                DisplayTextFeedback(-1, CurrentObjLoc, Color.red);
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
        //Debug.Log("Object should be destroyed");

        // Nikolaos Comandariu
        if (NumOfObjToSpawn <= 0 && currentObject == null)
        {
            //Debug.Log("All Objects Processed Event");
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
        //print("Decline clicked");
        if (currentObject == null)
            return;

        ObjectPrototype_ proto = currentObject.GetComponent<ObjectPrototype_>();

        bool isMatch = false;
      
        for (int i = 0; i < criteriaList.Count; i++)
        {
            int x = criteriaList[i];

            if (x <= 0) break;

            // Set current round condition
            roundCondition = (RoundCondition)x-1;

            switch (roundCondition)
            {
                case RoundCondition.Fruit:
                    isMatch = !proto.checkIsFruit();
                    break;
                case RoundCondition.Red:
                    isMatch = !proto.checkIsRed();
                    break;
                case RoundCondition.Green:
                    isMatch = !proto.checkIsGreen();
                    break;
                case RoundCondition.Yellow:
                    isMatch = !proto.checkIsYellow();
                    break;
                case RoundCondition.Single:
                    isMatch = !proto.checkIsSingle();
                    break;
                //added code by smriti
                case RoundCondition.Orange:
                    isMatch = !proto.checkIsOrange();
                    break;
                case RoundCondition.Drink:
                    isMatch = !proto.checkIsDrink();
                    break;
                case RoundCondition.NotFruit:
                    isMatch = proto.checkIsFruit();
                    break;
                case RoundCondition.NotRed:
                    isMatch = proto.checkIsRed();
                    break;
                case RoundCondition.NotGreen:
                    isMatch = proto.checkIsGreen();
                    break;
                case RoundCondition.NotYellow:
                    isMatch = proto.checkIsYellow();
                    break;
                case RoundCondition.NotSingle:
                    isMatch = proto.checkIsSingle();
                    break;
                case RoundCondition.NotOrange:
                    isMatch = proto.checkIsOrange();
                    break;
                case RoundCondition.NotDrink:
                    isMatch = proto.checkIsDrink();
                    break;
            } //end of added code by smriti

            Debug.Log("Is match: " + isMatch);
            Debug.Log("Round condition: " + roundCondition);

            if (isMatch)
            {
                //Debug.Log("ACCEPT: Correct choice!");

                if (rhythmPoints) // Nikolaos Comandariu.
                {
                    score += 1;
                }

                score += 1; // Score should not be in ObjectSpawner ideally, might need to refactor later.
                DisplayTextFeedback(+1, CurrentObjLoc, Color.green);
                UpdateScoreUI();
                Debug.Log("Correct! Score is now: " + score);
            }
            else if (!isMatch)
            {
                //Debug.Log("Wrong choice!");
                score -= 1;
                DisplayTextFeedback(-1, CurrentObjLoc, Color.red);
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
            DisplayTextFeedback(+1, CurrentObjLoc, Color.green);
            score += 1;
            UpdateScoreUI();
            Debug.Log("Correct, Score is now: " + score);
        }
        else
        {
            Debug.Log("Wrong choice!");
            DisplayTextFeedback(-1, CurrentObjLoc, Color.red);
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
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            //code added by smriti
            if (IsPlayer1)
            {
                scoreText.text = "Score: " + score;
                scoreManager.changePlayer1Score(score);
            }
            else
            {
                scoreText.text = "Score: " + score;
                scoreManager.changePlayer2Score(score);
            }
            //scoreText.text = "Score: " + score;
            //scoreManager.changePlayer1Score(score); //smriti added this
            //scoreManager.changePlayer2Score(score); // smriti added this
        }
            //scoreText.text = "Score: " + score;
            //end of code added by smriti
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

    private void TallyUpScores()
    {
        OnTallyUpScores?.Invoke(score);
    }

    private void AcceptRhythmPoints(bool canAccept)
    {
        rhythmPoints = canAccept;
    }
    
    private void SetCriteria(int crit1, int crit2, int crit3, int crit4)
    {
        criteriaList.Add(crit1);
        criteriaList.Add(crit2);
        criteriaList.Add(crit3);
        criteriaList.Add(crit4);

        Debug.Log("Criterias: " + crit1 + crit2 + crit3 + crit4);
    }

    // End of code from Nikolaos Comandariu.
}