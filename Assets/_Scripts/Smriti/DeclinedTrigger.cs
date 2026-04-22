using UnityEngine;

public class DeclinedTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ProduceObj"))
        {
            Debug.Log("Collision Triggered");
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
