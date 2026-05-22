using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameChangerManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private Canvas rhythmCanvas;
    [SerializeField] private Canvas oppositeDayCanvas;
    [SerializeField] private Canvas refundCanvas;

    [Header("Variables")]
    [SerializeField] private int numOfGameChangers;

    //[Header("Game Objects")]
    //[SerializeField] private GameObject hitboxParent;

    // Events.
    public static event Action onGameChangerActivated;
    public static event Action onOppositeDayActivated;
    public static event Action onRhythmActivated;
    public static event Action onRefundActivated;
    public static event Action<int> onGameChangerGenerated;

    private bool isActive;
    private bool onCooldown;
    private bool canActivate;

    private int activeCanvas;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) // TODO: Remove after testing!
            StartGameChanger();
    }

    private void OnEnable()
    {
        GameManager.onGameChangerRound += StartGameChanger;
        GameManager.onGenerateGameChanger += GenerateGameChanger;
        GameManager.onNextRound += Reset;
    }

    private void OnDisable()
    {
        GameManager.onGameChangerRound -= StartGameChanger;
        GameManager.onGenerateGameChanger -= GenerateGameChanger;
        GameManager.onNextRound -= Reset;
    }

    private void Start()
    {
        isActive = false;
        onCooldown = false;
        canActivate = false;

        rhythmCanvas.enabled = false;
        oppositeDayCanvas.enabled = false;
        refundCanvas.enabled = false;
    }

    private void ActivateGameChanger()
    {
        // Check if player can activate game changer.
        if (isActive || onCooldown) return;

        canActivate = true;
        isActive = true;
        onCooldown = true;

         // To let game manager know.
    }

    private void GenerateGameChanger()
    {
        activeCanvas = Random.Range(0, numOfGameChangers);
        onGameChangerGenerated?.Invoke(activeCanvas);
    }

    /// <summary>
    /// Starts a random game changer and sets its canvas to active.
    /// </summary>
    public void StartGameChanger()
    {
        switch(activeCanvas)
        {
            case 0:
                onRhythmActivated?.Invoke();
                rhythmCanvas.enabled = true;
                break;
            case 1:
                onOppositeDayActivated?.Invoke();
                oppositeDayCanvas.enabled = true;
                break;
            case 2:
                onRefundActivated?.Invoke();
                refundCanvas.enabled = true;
                break;
        }
    }

    private void Reset()
    {
        rhythmCanvas.enabled = false;
        oppositeDayCanvas.enabled = false;
        refundCanvas.enabled = false;
    }
}