using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class CreditsManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] creditNames;
    [SerializeField] private GameObject creditAnim;

    [Header("Game Objects")]
    [SerializeField] private Vector3[] creditsPos;

    [Header("Variables")]
    [SerializeField] private float stampTime;
    [SerializeField] private float speed;
    [SerializeField] private float initialWaitTime;

    private bool keepMoving;
    private bool hasPlayedAlready = false;
    private bool hasReset = true;

    private void Start()
    {
        //StartCoroutine(Credits());
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
        hasReset = false;

        yield return new WaitForSeconds(initialWaitTime);
        keepMoving = false;
        creditAnim.GetComponent<Animator>().SetBool("HasPlayed", false);

        for (int i = 0; i < creditNames.Length; i++)
        {
            keepMoving = false;
            yield return new WaitForSeconds(stampTime);

            creditNames[i].GetComponent<SpriteRenderer>().enabled = true;
            keepMoving = true;

            if (i == creditNames.Length - 1)
            {
                keepMoving = false; 
                creditAnim.GetComponent<Animator>().SetTrigger("Finished");
                creditAnim.GetComponent<Animator>().SetBool("HasPlayed", true);
            }

            yield return new WaitForSeconds(stampTime);
        }

        keepMoving = false;
        hasPlayedAlready = true;  
    }

    public void StartCredits()
    {
        if (hasPlayedAlready || creditAnim.GetComponent<Animator>().GetBool("HasPlayed") == true) return;

        /*if(hasReset)
        {
            for (int i = 0; i < creditNames.Length; i++)
            {
                creditsPos.SetValue(i, );
            }
        }*/

        Debug.Log("Started credits");
        StartCoroutine(Credits());
    }

    public void ResetCredits()
    {
        keepMoving = false;
        hasPlayedAlready = false;

        

        StartCredits();
    }
}