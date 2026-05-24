using System;
using UnityEngine;

public class PrinterScript : MonoBehaviour
{
    private Animator animator;

    public static event Action PrinterAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

   /* public void OnEnable()
    {
        ButtonClick.onPrintReceipt += PlayAnim;
    }
    public void OnDisable()
    {
        ButtonClick.onPrintReceipt -= PlayAnim;
    }*/

    private void PlayAnim()
    {
        animator.Play("PRINTER");
        Debug.Log("Trying to animate");
    }
}
