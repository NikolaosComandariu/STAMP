using UnityEngine;
using System.Collections;

public class CreditsManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] creditNames;

    [Header("Variables")]
    [SerializeField] private float stampTime;
    [SerializeField] private float speed;

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

        for(int i = 0; i < creditNames.Length; i++)
        {
            keepMoving = false;
            yield return new WaitForSeconds(stampTime);
            creditNames[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            keepMoving = true;
            yield return new WaitForSeconds(stampTime);
        }

        keepMoving = false;
    }
}
