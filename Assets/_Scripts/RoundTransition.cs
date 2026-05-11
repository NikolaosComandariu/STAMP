using UnityEngine;
using System;
using System.Collections;

public class RoundTransition : MonoBehaviour
{
    [Header("Canvas Related")]
    [SerializeField] private Canvas canvasComponent;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Game Objects")]
    [SerializeField] private GameObject transitionObj;

    // Events.
    public static event Action onTransitionEnded;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Get relevant components if they aren't there.
    /// </summary>
    private void Awake()
    {
        if(canvasComponent == null)
            canvasComponent = GetComponent<Canvas>();

        if(canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        anim = transitionObj.GetComponent<Animator>();
        spriteRenderer = transitionObj.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        canvasComponent.enabled = false;
        spriteRenderer.enabled = false;
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

    /// <summary>
    /// Enables canvas and starts coroutine for transition.
    /// </summary>
    private void StartRoundTransition()
    {
        canvasComponent.enabled = true;
        spriteRenderer.enabled = true;
        StartCoroutine(RoundTransitionCoroutine());
    }

    /// <summary>
    /// Plays animation for transition, waits for it to end,
    /// plays stationary animation and starts game afterwards.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RoundTransitionCoroutine()
    {
        anim.SetTrigger("ShowTransition"); // Triggers: ShowTransition, StopTransition
        yield return new WaitForSeconds(3.5f);

        anim.ResetTrigger("ShowTransition");
        anim.SetTrigger("StopTransition");
        yield return new WaitForSeconds(1.0f);

        anim.ResetTrigger("StopTransition");
        canvasComponent.enabled = false;
        spriteRenderer.enabled = false;
        onTransitionEnded?.Invoke();
    }
}