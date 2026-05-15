using UnityEngine;
using System.Collections;

public class CreditsManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] creditNames;
    [SerializeField] private GameObject creditAnim;

    [Header("Variables")]
    [SerializeField] private float stampTime;
    [SerializeField] private float speed;
    [SerializeField] private float initialWaitTime;

    private bool keepMoving;

    private void Start()
    {
        StartCoroutine(Credits());
    }

    private void Update()
    {
        if (!keepMoving) return;

        //creditNames[0].transform.position += speed * Time.deltaTime * Vector3.down;

        foreach(var credit in creditNames)
        {
            credit.transform.position += speed * Time.deltaTime * Vector3.down;
        }
    }

    private IEnumerator Credits()
    {
        yield return new WaitForSeconds(initialWaitTime);
        keepMoving = false;

        for(int i = 0; i < creditNames.Length; i++)
        {
            keepMoving = false;
            yield return new WaitForSeconds(stampTime);

            creditNames[i].GetComponent<SpriteRenderer>().enabled = true;
            keepMoving = true;

            if (i == creditNames.Length - 1)
            {
                keepMoving = false; 
                creditAnim.GetComponent<Animator>().SetTrigger("Finished");
            }

            yield return new WaitForSeconds(stampTime);
        }

        keepMoving = false;
    }
}