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

    [Header("Variables")]
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float activeTime;

    // Events.
    public static event Action onTransitionEnded;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool gameChangerActive;

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
        //canvasComponent.enabled = false;
        //canvasGroup.alpha = 0.0f;

        spriteRenderer.enabled = false;
        gameChangerActive = false;
    }

    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onNextRound += StartRoundTransition;
        GameManager.onGameChangerRound += GameChangerRound;
        //GameChangerManager.onOppositeDayActivated +=;
        //GameChangerManager.onRhythmActivated +=;
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onNextRound -= StartRoundTransition;
        GameManager.onGameChangerRound -= GameChangerRound;
    }

    /// <summary>
    /// Enables canvas and starts coroutine for transition.
    /// </summary>
    private void StartRoundTransition()
    {
        //canvasComponent.enabled = true;
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
        StartCoroutine(FadeIn());

        //yield return new WaitForSeconds(1.0f);

        //anim.SetTrigger("ShowTransition"); // Triggers: ShowTransition, StopTransition
        //yield return new WaitForSeconds(3.5f);

        //anim.ResetTrigger("ShowTransition");
        //anim.SetTrigger("StopTransition");
        //yield return StartCoroutine(FadeOut());
        //yield return new WaitForSeconds(1.0f);

        //anim.ResetTrigger("StopTransition");
        //canvasComponent.enabled = false;
        //spriteRenderer.enabled = false;
        //onTransitionEnded?.Invoke();

        yield return null;
    }

    private IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeInTime;
            Debug.Log("Canvas alpha: " + canvasGroup.alpha);

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeInTime;

            yield return null;
        }
    }

    private void GameChangerRound()
    {
        gameChangerActive = true;

    }
}