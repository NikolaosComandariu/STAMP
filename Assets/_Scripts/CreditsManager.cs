using UnityEngine;
using System.Collections;

public class CreditsManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] creditNames;

    [Header("Variables")]
    [SerializeField] private float stampTime;
    [SerializeField] private float speed;

    private int amountOfNames = 7;
    private bool keepMoving;

    private void Start()
    {
        StartCoroutine(Credits());
    }

    private void Update()
    {
        if (!keepMoving) return;

        creditNames[0].transform.position += speed * Time.deltaTime * Vector3.down;   
    }

    private IEnumerator Credits()
    {
        yield return new WaitForSeconds(stampTime);
        keepMoving = false;

        for(int i = 0; i < amountOfNames; i++)
        {
            keepMoving = false;
            creditNames[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(stampTime);
            keepMoving = true;
            yield return new WaitForSeconds(stampTime);
        }

        keepMoving = false;
    }
}
