using UnityEngine;
using System.Collections;

public class CreditsManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] creditNames;

    [Header("Variables")]
    [SerializeField] private float stampTime;

    private int amountOfNames = 7;

    private IEnumerator Credits()
    {
        for(int i = 0; i < amountOfNames; i++)
        {
            creditNames[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(stampTime);
        }
        
    }
}
