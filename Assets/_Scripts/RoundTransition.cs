using UnityEngine;
using System;
using System.Collections;

public class RoundTransition : MonoBehaviour
{
    [Header("Canvas Related")]
    [SerializeField] private Canvas canvasComponent;
    [SerializeField] private CanvasGroup canvasGroup;

    // Events.
    public static event Action onTransitionEnded;

    /// <summary>
    /// Get relevant components if they aren't there.
    /// </summary>
    private void Awake()
    {
        if(canvasComponent == null)
            canvasComponent = GetComponent<Canvas>();

        if(canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasComponent.enabled = false;
    }

    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onNextRound += StartRoundTransition;
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onNextRound -= StartRoundTransition;
    }

    private void StartRoundTransition()
    {
        canvasComponent.enabled = true;
        StartCoroutine(RoundTransitionCoroutine());
    }

    private IEnumerator RoundTransitionCoroutine()
    {
        yield return new WaitForSeconds(5);
        canvasComponent.enabled = false;
        onTransitionEnded?.Invoke();
    }
}