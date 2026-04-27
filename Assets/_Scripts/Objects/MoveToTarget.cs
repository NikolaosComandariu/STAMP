using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Transform target; // Target position of item.

    [Header("Speed")]
    [SerializeField] private float speed; // Travel speed of item.

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // Added by Nikolaos Comandariu.
    public float GetSpeed()
    {
        return speed;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public void SetTarget(Transform Target)
    {
        target = Target;
    }

    public void SetSpeed(float Speed)
    {
        speed = Speed;
    }
    // End of code added by Nikolaos Comandariu.
}
