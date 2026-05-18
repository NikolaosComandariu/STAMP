using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Hashing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private float MoveForce;
    [SerializeField] private RoundCondition currentRoundCondition;
    [SerializeField] public GameObject currentObject;
    [SerializeField] private List<int> criteriaList = new List<int>();
    [SerializeField] private RoundCondition roundCondition;
    [SerializeField] private bool IsPlayer1; //smriti added this

    [Header("Game Objects")]
    [SerializeField] private List<GameObject> ObjectsPool = new List<GameObject>(); // Amount of objects in the round
    [SerializeField] private List<GameObject> GlitchedItemsPool = new List<GameObject>();
    [SerializeField] private List<GameObject> AllPossibleObjects; // All prefabs possible to spawn
    [SerializeField] private GameObject ScoreTextFeedback;

    [Header("Transforms")]
    [SerializeField] private Transform EndOfConveyor; // Stopping point of objects where they're ready to be accepted / declined
    [SerializeField] private Transform SpawnPos; // Off screen spawnpoint for objects to then scroll onto screen
    [SerializeField] private Transform DeclinedP1; //code by Smriti
    [SerializeField] private Transform AcceptedP1; //code by Smriti

    [Header("Text")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI productPrice;

    [Header("Input Delay")]
    [SerializeField] private float InputDelayTime;

    [Header("Game Objects")]
    [SerializeField] private GameObject correctParticles;
    [SerializeField] private GameObject wrongParticles;
    [SerializeField] private AudioManager audioManager;

    [Header("Events")]
    public System.Action onAllObjectsProcessed; // Nikolaos Comandariu.
    public static event Action IncrementP1Score;
    public static event Action IncrementP2Score;
    public static Action<bool> onInputAllowedP1;
    public static Action<bool> onInputAllowedP2;

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
    private bool NotMatch = false;
    private bool IsMatch = false;
    private bool refundActive = false;

    public bool InputAllowed;
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
        Red,
        Orange,
        Yellow,
        Green,
        NotRed,
        NotOrange,
        NotYellow,
        NotGreen,
        Fruit,
        Drink,
        Single,
        NotFruit,
        NotDrink,
        NotSingle,
        Glitched,
        LessThan5, //options added by smriti
        MoreThan5,
        LessThan3,
        MoreThan3,
        LessThan2,
        MoreThan2,
        LessThan1,
        MoreThan1 //end of options added by smrti
    }
    
    // Nikolaos Comandariu.
    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        CriteriaManager.OnCriteriaDecided += SetCriteria;
        GameChangerManager.onRefundActivated += RefundActive;

        if (IsPlayer1)
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
        CriteriaManager.OnCriteriaDecided -= SetCriteria;
        GameChangerManager.onRefundActivated -= RefundActive;

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
        objToSpawn = 5;
        AllowObjSpawn = true;
        rhythmPoints = false;
    }

    private void Update()
    {
        if (currentObject != null)
        {
            CurrentObjLoc = currentObject.transform.position;
        }
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

                    InputAllowed = false;

                    if(IsPlayer1)
                    {
                        onInputAllowedP1?.Invoke(InputAllowed);
                    }
                    else
                    {
                        onInputAllowedP2?.Invoke(InputAllowed);
                    } 

                    productPrice.text = "£" + 
                        currentObject.GetComponent<ObjectPrototype_>().GetPrice().ToString();

                    ObjectsPool.RemoveAt(n);
                    NumOfObjToSpawn--;

                    MoveToTarget mover = currentObject.GetComponent<MoveToTarget>();
                    mover.SetTarget(EndOfConveyor);
                    mover.SetSpeed(MoveForce);

                    Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
                    rb.transform.position = Vector2.MoveTowards(SpawnPos.position, EndOfConveyor.position, MoveForce * Time.deltaTime);

                    AllowDecision = true;
                    AllowObjSpawn = false;

                    yield return new WaitForSeconds(InputDelayTime);

                    InputAllowed = true;

                    if (IsPlayer1)
                    {
                        onInputAllowedP1?.Invoke(InputAllowed);
                    }
                    else
                    {
                        onInputAllowedP2?.Invoke(InputAllowed);
                    }

                    yield return null;
                }
            }
        }
    }

    public void DisplayTextFeedback(int amount, Vector3 position, Color color)
    {
            GameObject instance = Instantiate(ScoreTextFeedback, position, Quaternion.identity);

            TextMeshPro tmp = instance.GetComponent<TextMeshPro>();
            tmp.text = amount.ToString();
            tmp.color = color;
    }

    /// <summary>
    /// Clears object pool, gets random objects from
    /// all possible objects to spawn.
    /// </summary>
    public void GenerateObjectsForRound() 
    {
        ObjectsPool.Clear();

        // Repopulate ObjectsPool.
        for (int i = 0; i < objToSpawn; i++)
        {
            int refundItem = Random.Range(0, 2); // 1 in 3 chance.

            if (refundActive && refundItem == 0) // Nikolaos Comandariu.
            {
                int randomIndex = Random.Range(0, GlitchedItemsPool.Count);
                ObjectsPool.Add(GlitchedItemsPool[randomIndex]);
            }
            else
            {
                int randomIndex = Random.Range(0, AllPossibleObjects.Count);
                ObjectsPool.Add(AllPossibleObjects[randomIndex]);
            }  
        }

        refundActive = false;

        NumOfObjToSpawn = ObjectsPool.Count;
    }

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

        for (int i = 0; i < criteriaList.Count; i++)
        {
            int x = criteriaList[i];
            if (x <= 0) break;

            // Set current round condition
            roundCondition = (RoundCondition)x;

            if (proto.checkIsGlitched())//code added/edited by smriti
            {
                isMatch = false;
            }
            else
            {
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
                    case RoundCondition.Glitched: //code by smriti
                        isMatch = !proto.checkIsGlitched();
                        break; //end code by smriti
                    //code added by smriti
                    case RoundCondition.LessThan5:
                        if (proto.GetPrice() < 5) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                    case RoundCondition.MoreThan5:
                        if (proto.GetPrice() > 5) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                    case RoundCondition.LessThan3:
                        if (proto.GetPrice() < 3) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                    case RoundCondition.MoreThan3:
                        if (proto.GetPrice() > 3) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                    case RoundCondition.LessThan2:
                        if (proto.GetPrice() < 2) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                    case RoundCondition.MoreThan2:
                        if (proto.GetPrice() > 2) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                    case RoundCondition.LessThan1:
                        if (proto.GetPrice() < 1) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                    case RoundCondition.MoreThan1:
                        if (proto.GetPrice() > 1) { isMatch = true; }
                        else { isMatch = false; }
                        break;
                }

                Debug.Log("Is match: " + isMatch);
                Debug.Log("Round condition: " + roundCondition);
            }

            if (isMatch)
            {
                if (rhythmPoints) // Nikolaos Comandariu.
                {
                    IncrementPlayerScores();
                    DisplayTextFeedback(+1, CurrentObjLoc, Color.green);
                }

                IncrementPlayerScores();
                Instantiate(correctParticles, CurrentObjLoc, Quaternion.identity);
                audioManager.PlaySFX(audioManager.correctChoiceSFX);
            }
            else if(!isMatch)
            {
                Instantiate(wrongParticles, CurrentObjLoc, Quaternion.identity);
                audioManager.PlaySFX(audioManager.incorrectChoiceSFX);
                NotMatch = true;

                //code by Smriti
                if (AllowDecision)
                {
                    NotMatch = true;

                    if (AllowDecision)
                    {
                        MoveToTarget acceptedP1 = currentObject.GetComponent<MoveToTarget>();
                        acceptedP1.SetTarget(AcceptedP1);
                        acceptedP1.SetSpeed(MoveForce);
                        currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position,
                        AcceptedP1.position, MoveForce * Time.deltaTime);

                        AllowDecision = false;
                    }
                }
            }
            //end of code added/edited by smriti/edited by Nikolaos.
        }

        Destroy(currentObject);
        currentObject = null;
        AllowObjSpawn = true;

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
        if (currentObject == null)
            return;

        ObjectPrototype_ proto = currentObject.GetComponent<ObjectPrototype_>();

        bool isMatch = false;

        for (int i = 0; i < criteriaList.Count; i++)
        {
            int x = criteriaList[i];
            if (x <= 0) break;

            roundCondition = (RoundCondition)x;

            if (proto.checkIsGlitched()) // code added and edited by smriti
            {
                isMatch = true;
            }
            else
            { 
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
                    case RoundCondition.Glitched: //code added by smriti
                        isMatch = proto.checkIsGlitched();
                        break;
                    //code added by smriti
                    case RoundCondition.LessThan5:
                        if(proto.GetPrice() < 5) { isMatch = false; }
                        else{ isMatch = true; }
                        break;
                    case RoundCondition.MoreThan5:
                        if (proto.GetPrice() > 5) { isMatch = false; }
                        else { isMatch = true; }
                        break;
                    case RoundCondition.LessThan3:
                        if (proto.GetPrice() < 3) { isMatch = false; }
                        else { isMatch = true; }
                        break;
                    case RoundCondition.MoreThan3:
                        if (proto.GetPrice() > 3) { isMatch = false; }
                        else { isMatch = true; }
                        break;
                    case RoundCondition.LessThan2:
                        if (proto.GetPrice() < 2) { isMatch = false; }
                        else { isMatch = true; }
                        break;
                    case RoundCondition.MoreThan2:
                        if (proto.GetPrice() > 2) { isMatch = false; }
                        else { isMatch = true; }
                        break;
                    case RoundCondition.LessThan1:
                        if (proto.GetPrice() < 1) { isMatch = false; }
                        else { isMatch = true; }
                        break;
                    case RoundCondition.MoreThan1:
                        if (proto.GetPrice() > 1) { isMatch = false; }
                        else { isMatch = true; }
                        break;
                } //end of added code by smriti

                Debug.Log("Is match: " + isMatch);
                Debug.Log("Round condition: " + roundCondition);
            }

            if (isMatch)
            {
                Debug.Log("Rhythm Points: " + rhythmPoints);
                if (rhythmPoints) // Nikolaos Comandariu.
                {
                    IncrementPlayerScores();
                    DisplayTextFeedback(+1, CurrentObjLoc, Color.green);
                }
                isMatch = true;

                IncrementPlayerScores();
                DisplayTextFeedback(+1, CurrentObjLoc, Color.green);
                Instantiate(correctParticles, CurrentObjLoc, Quaternion.identity);
                audioManager.PlaySFX(audioManager.correctChoiceSFX);
            }           
            else if (!isMatch)
            {
                Instantiate(wrongParticles, CurrentObjLoc, Quaternion.identity);
                audioManager.PlaySFX(audioManager.incorrectChoiceSFX);
                NotMatch = true;

                //code by Smriti
                if (AllowDecision)
                {
                    NotMatch = true;

                    //code by Smriti
                    if (AllowDecision)
                    {
                        MoveToTarget acceptedP1 = currentObject.GetComponent<MoveToTarget>();
                        acceptedP1.SetTarget(AcceptedP1);
                        acceptedP1.SetSpeed(MoveForce);
                        currentObject.transform.position = Vector2.MoveTowards(EndOfConveyor.position,
                        AcceptedP1.position, MoveForce * Time.deltaTime);
                        AllowDecision = false;
                    }
                }
            }
            
            //end of code added/edited by smriti
        }

        Destroy(currentObject);
        currentObject = null;
        AllowObjSpawn = true;

        // Nikolaos Comandariu
        if (NumOfObjToSpawn <= 0 && currentObject == null)
        {
            onAllObjectsProcessed?.Invoke();
        } // End of code added.
        else
        {
            StartCoroutine(SpawnObject());
        }
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
        ObjectsPool.Clear();
        NumOfObjToSpawn = 0;
        AllowObjSpawn = true;
        refundActive = false;
        criteriaList.Clear();

        Destroy(currentObject);
        currentObject = null;
    }

    private void IncrementPlayerScores()
    {
        if(IsPlayer1)
        {
            IncrementP1Score?.Invoke();
        }
        else
        {
            IncrementP2Score?.Invoke();
        }  
    }

    private void AcceptRhythmPoints(bool canAccept)
    {
        rhythmPoints = canAccept;
    }
    
    private void SetCriteria(int crit1, int crit2)
    {
        criteriaList.Clear();

        if(refundActive)
        {
            criteriaList.Add(14);
            criteriaList.Add(14);
        }
        else
        {
            criteriaList.Add(crit1);
            criteriaList.Add(crit2);
        }
    }

    private void RefundActive()
    {
        refundActive = true;
    }

    // End of code from Nikolaos Comandariu.
}