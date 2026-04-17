using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 1.5f;
    [SerializeField] private float MoveForce;
    [SerializeField] private List<GameObject> ObjectsPool = new List<GameObject>();
    [SerializeField] private Transform EndOfConveyor;
    [SerializeField] private Transform SpawnPos;

    private Button Accept;
    private Button Decline;

    private int NumOfObjToSpawn;
    private bool AllowObjSpawn;



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

                GameObject g = Instantiate(ObjectsPool[n], SpawnPos.position, ObjectsPool[n].transform.rotation);
                ObjectsPool.RemoveAt(n);
                NumOfObjToSpawn--;

                MoveToTarget mover = g.GetComponent<MoveToTarget>();
                mover.target= EndOfConveyor;
                mover.speed =MoveForce;

                Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                //rb.linearVelocity = transform.right * MoveForce;
                rb.transform.position = Vector2.MoveTowards(SpawnPos.position, EndOfConveyor.position, MoveForce * Time.deltaTime);

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }


    //spawn one item then once button pressed then move item and only then spawn the next one and repeat

    // on click of either button item is then destroyed, when first object is instantiated set a variable to signify an obj is present and the destroy on button press then set variable back to allow next obj to spawn


    void AcceptOrDeclinePressed()
    {
        if (Accept == true)
        {

        }
    }
}