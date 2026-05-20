using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{

    [SerializeField] private Button Q;
    [SerializeField] private Button E;
    [SerializeField] private Button I;
    [SerializeField] private Button P;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        // Subscribe to events. += FunctionName()
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Q.image.color = Q.colors.pressedColor;
        }
        else
        {
            Q.image.color = Q.colors.normalColor;
        }
        if (Input.GetKey(KeyCode.E))
        {
            E.image.color = E.colors.pressedColor;
        }
        else
        {
            E.image.color = E.colors.normalColor;
        }
        if (Input.GetKey(KeyCode.I))
        {
            I.image.color = I.colors.pressedColor;
        }
        else
        {
            I.image.color = I.colors.normalColor;
        }
        if (Input.GetKey(KeyCode.P))
        {
            P.image.color = P.colors.pressedColor;
        }
        else
        {
            P.image.color = P.colors.normalColor;
        }
    }

    private void ButtonFunctionTemp()
    {
        // ButtonColour changes
    }
}
