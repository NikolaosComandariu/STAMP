using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Unity.VisualScripting;

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

                AllowObjSpawn = false;

                //yield return new WaitForSeconds(spawnDelay);
                //AllowObjSpawn = false;

                yield return null;

            }
        }
    }

    //testing stuff
    [SerializeField] private Transform DeclinedP1;

    public void AcceptObject()
    {
        if (currentObject != null)
        {
            Destroy(currentObject);
            AllowObjSpawn = true;
            StartCoroutine(SpawnObject());
        }
    }
    public void DeclineObject()
    {
        if (currentObject != null)
        {
            //edited testing
            Rigidbody2D testrb = currentObject.GetComponent<Rigidbody2D>();
            testrb.transform.position = Vector3.MoveTowards(EndOfConveyor.position,DeclinedP1.position, MoveForce * Time.deltaTime);
           // Destroy(currentObject);
            AllowObjSpawn = true;
            StartCoroutine(SpawnObject());
        }
    }
}