using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{

    public Transform pos;
    public GameObject[] objectsToInstatiate;
    public GameObject[] objectsToOutstantiate;
    public List<GameObject> spawnedObjectsList = new List<GameObject>();
    public float MoveForce;

    private bool firstObjSpawned;


    Rigidbody2D rb2D;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnObject();
        firstObjSpawned = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObject()
    {
            int n = Random.Range(0, objectsToInstatiate.Length);
            GameObject g = Instantiate(objectsToInstatiate[n], pos.position, objectsToInstatiate[n].transform.rotation);
            Rigidbody2D rb = g.GetComponent<Rigidbody2D>();

            rb.linearVelocity = transform.right * MoveForce;

            spawnedObjectsList.Add(g);
    }
}