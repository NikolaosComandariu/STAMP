using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ButtonPress : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button Q;
    [SerializeField] private Button E;
    [SerializeField] private Button I;
    [SerializeField] private Button P;

    [SerializeField] private float delay;

    private void OnEnable()
    {
        ButtonClick.onInputDetected += ChangeButtonColours;
    }

    private void OnDisable()
    {
        ButtonClick.onInputDetected -= ChangeButtonColours;
    }

    private void ChangeButtonColours(bool isP1, bool isAccept)
    {
        if (isP1)
        {
            if (isAccept)
            {
                Q.image.color = Q.colors.pressedColor;
                StartCoroutine(ResetColour(Q, delay));
            }
            else
            {
                E.image.color = Q.colors.pressedColor;
                StartCoroutine(ResetColour(E, delay));
            }
        }
        else
        {
            if (isAccept)
            {
                I.image.color = Q.colors.pressedColor;
                StartCoroutine(ResetColour(I, delay));
            }
            else
            {
                P.image.color = Q.colors.pressedColor;
                StartCoroutine(ResetColour(P, delay));
            }
        }
    }

    private IEnumerator ResetColour(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        button.image.color = Q.colors.normalColor;
    }
}
