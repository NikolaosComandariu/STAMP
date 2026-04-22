using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

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