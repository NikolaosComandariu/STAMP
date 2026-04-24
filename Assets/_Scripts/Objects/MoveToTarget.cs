using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target; // Can be turned to [SerializeField] private
    public float speed;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,target.position,speed * Time.deltaTime);
    }
}
