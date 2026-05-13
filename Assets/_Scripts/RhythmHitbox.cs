using System;
using UnityEngine;

public class RhythmHitbox : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool isLeft;

    // Events.
    public static event Action<bool> onColliderEnteredP1;
    public static event Action<bool> onColliderEnteredP2;

    private void OnEnable()
    {
        GameManager.onNextRound += DisableHitbox;
        GameChangerManager.onRhythmActivated += EnableHitbox;

        //GameChangerManager.onRushHourActivated += FloodObjectPool;
    }

    private void OnDisable()
    {
        GameManager.onNextRound -= DisableHitbox;
        GameChangerManager.onRhythmActivated -= EnableHitbox;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(isLeft)
        {
            //Debug.Log("Player 1 entered");
            onColliderEnteredP1.Invoke(true);
        }
        else
        {
            onColliderEnteredP2.Invoke(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (isLeft)
        {
           // Debug.Log("Player 1 exited");
            onColliderEnteredP1.Invoke(false);
        }
        else
        {
            onColliderEnteredP2.Invoke(false);
        }
    }

    private void DisableHitbox()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void EnableHitbox()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}