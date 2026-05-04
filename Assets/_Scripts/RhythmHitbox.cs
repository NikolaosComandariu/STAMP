using System;
using UnityEngine;

public class RhythmHitbox : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool isLeft;

    // Events.
    public static event Action<bool> onColliderEnteredP1;
    public static event Action<bool> onColliderEnteredP2;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(isLeft)
        {
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
            onColliderEnteredP1.Invoke(false);
        }
        else
        {
            onColliderEnteredP2.Invoke(false);
        }
    }
}
