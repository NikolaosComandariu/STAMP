using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
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

    private Button Accept;
    private Button Decline;

    private int NumOfObjToSpawn; // tally of items left to spawn
    private bool AllowObjSpawn;

    private GameObject currentObject;

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
                ObjectsPool.RemoveAt(n);
                NumOfObjToSpawn--;

                MoveToTarget mover = currentObject.GetComponent<MoveToTarget>();
                mover.target = EndOfConveyor;
                mover.speed = MoveForce;



                Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
                //rb.linearVelocity = transform.right * MoveForce;
                rb.transform.position = Vector2.MoveTowards(SpawnPos.position, EndOfConveyor.position, MoveForce * Time.deltaTime);

                AllowDecision = true;
                AllowObjSpawn = false;

                //yield return new WaitForSeconds(spawnDelay);
                //AllowObjSpawn = false;

                yield return null;

            }
        }
    }

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
        if (currentObject != null)
        {
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
    }
    public void DeclineObject()
    {
        if (currentObject != null)
        {
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
    }
}
