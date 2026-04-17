using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{

    public Transform pos;
    public GameObject[] objectsToInstatiate;
    public GameObject[] objectsToOutstantiate;
    public List<GameObject> spawnedObjectsList = new List<GameObject>();
    public float MoveForce;
    private bool AllowObjSpawn;

    public float spawnDelay = 1.5f;

    private bool firstObjSpawned;

    Rigidbody2D rb2D;

    void Start()
    {

        AllowObjSpawn = true;
        StartCoroutine(SpawnObject());



    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnObject()
    {
        while (AllowObjSpawn)
        {
            int n = Random.Range(0, objectsToInstatiate.Length);

            GameObject g = Instantiate(objectsToInstatiate[n], pos.position, objectsToInstatiate[n].transform.rotation);
            Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
            rb.linearVelocity = transform.right * MoveForce;

            spawnedObjectsList.Add(g);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

}


//public IEnumerator WaitUntilTrue(bool AllowObjSpawn)
//{
//    while (AllowObjSpawn == false)
//    {
//        yield return null;
//    }
//}

//void SpawnObject()
//{
//    for (int i = 0; i < objectsToInstatiate.Length; i++)
//    {
//        if (AllowObjSpawn == true)
//        {
//            yield return WaitUntilTrue(AllowObjSpawn);
//            int n = Random.Range(0, objectsToInstatiate.Length);
//            GameObject g = Instantiate(objectsToInstatiate[n], pos.position, objectsToInstatiate[n].transform.rotation);
//            Rigidbody2D rb = g.GetComponent<Rigidbody2D>();

//            rb.linearVelocity = transform.right * MoveForce;

//            spawnedObjectsList.Add(g);
//        }
//    }
//}

