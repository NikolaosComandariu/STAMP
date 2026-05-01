using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 1f;

    private TextMeshPro tmp;
    private Color startColor;
    private float timer = 0f;

    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        startColor = tmp.color;

    }

    void Update()
    {

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        float t = timer / lifetime;

        tmp.color = new Color(startColor.r, startColor.g, startColor.b, 1f - t);

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}

