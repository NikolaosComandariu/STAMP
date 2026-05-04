using System;
using UnityEngine;

public class RhythmHitbox : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool isLeft;

    // Events.
    public static event Action<bool> onColliderEnteredP1;
    public static event Action<bool> onColliderEnteredP2;

    public void OnCollisionEnter2D(Collision2D collision)
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

    public void OnCollisionExit2D(Collision2D collision)
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
