using System.Collections;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] private int timer = 5;

    private void Start()
    {
        StartCoroutine(Timer());
        timer = 5;
    }

    private IEnumerator Timer()
    {
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }

        Destroy(gameObject);
    }
}