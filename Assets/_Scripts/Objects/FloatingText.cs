using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float lifetime = 1f;

    private TextMeshPro tmp;
    private Color startColor;
    private float timer = 0f;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        startColor = tmp.color;
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        float t = timer / lifetime;

        tmp.color = new Color(startColor.r, startColor.g, startColor.b, 1f - t);

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}