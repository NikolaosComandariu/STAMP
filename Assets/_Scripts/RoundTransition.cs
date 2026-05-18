using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class RoundTransition : MonoBehaviour
{
    [Header("Canvas Related")]
    [SerializeField] private Canvas canvasComponent;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Game Objects")]
    [SerializeField] private GameObject transitionObj;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI scoreTextP1 = null;
    [SerializeField] private TextMeshProUGUI scoreTextP2 = null;
    [SerializeField] private TextMeshProUGUI gameChangerText = null;
    [SerializeField] private TextMeshProUGUI roundText = null;

    [Header("Variables")]
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float activeTime;

    // Events.
    public static event Action onTransitionEnded;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool gameChangerActive;

    private int roundNum = 0;

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
        gameChangerActive = false;
    }

    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onNextRound += StartRoundTransition;
        GameChangerManager.onOppositeDayActivated += OppositeDayText;
        GameChangerManager.onRhythmActivated += RhythmText;
        //GameChangerManager.onSupermarketSweepActivated += SupermarketSweepText; // TODO: Re-enable once game changers are in!
        GameChangerManager.onRefundActivated += RefundText;
        ScoreManager.SendPlayerScores += SetScoreText;
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onNextRound -= StartRoundTransition;
        GameChangerManager.onOppositeDayActivated -= OppositeDayText;
        GameChangerManager.onRhythmActivated -= RhythmText;
        //GameChangerManager.onSupermarketSweepActivated -= SupermarketSweepText;
        GameChangerManager.onRefundActivated -= RefundText;
        ScoreManager.SendPlayerScores -= SetScoreText;
    }

    /// <summary>
    /// Enables canvas and starts coroutine for transition.
    /// </summary>
    private void StartRoundTransition()
    {
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

        yield return new WaitForSeconds(fadeInTime);

        anim.SetTrigger("ShowTransition"); // Triggers: ShowTransition, StopTransition
        yield return new WaitForSeconds(activeTime);

        anim.ResetTrigger("ShowTransition");
        anim.SetTrigger("StopTransition");
        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeOutTime);

        gameChangerText.text = null;
        anim.ResetTrigger("StopTransition");
        //canvasComponent.enabled = false;
        onTransitionEnded?.Invoke();

        yield return null;
    }

    private IEnumerator FadeIn()
    {
        roundNum++;
        roundText.text = "Round: " + roundNum;

        Color color;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeInTime;

            color = spriteRenderer.color;
            color.a += Time.deltaTime / fadeInTime;
            spriteRenderer.color = color;

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        Color color;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeInTime;

            color = spriteRenderer.color;
            color.a -= Time.deltaTime / fadeInTime;
            spriteRenderer.color = color;

            yield return null;
        }

        gameChangerText.text = null;
    }

    private void SetScoreText(int scoreP1, int scoreP2)
    {
        scoreTextP1.text = "Player 1 Score: " + scoreP1.ToString();
        scoreTextP2.text = "Player 2 Score: " + scoreP2.ToString();
    }

    private void OppositeDayText()
    {
        gameChangerText.text = null;
        gameChangerText.text = "Opposite Day: Controls are reversed!";
    }

    private void RhythmText()
    {
        gameChangerText.text = null;
        gameChangerText.text = "Rhythm: Accept/Decline objects in the hitbox for bonus points!";
    }

    private void SupermarketSweepText()
    {
        gameChangerText.text = null;
        gameChangerText.text = "Supermarket Sweep: You have up to 100 items to go through!";
    }

    private void RefundText()
    {
        gameChangerText.text = null;
        gameChangerText.text = "Refund: Decline items that do not belong here!";
    }
}