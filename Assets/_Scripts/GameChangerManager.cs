using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameChangerManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private Canvas rhythmCanvas;
    [SerializeField] private Canvas oppositeDayCanvas;

    [Header("Variables")]
    [SerializeField] private int numOfGameChangers;

    //[Header("Game Objects")]
    //[SerializeField] private GameObject hitboxParent;

    // Events.
    public static event Action onGameChangerActivated;
    public static event Action onOppositeDayActivated;
    public static event Action onRhythmActivated;

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
        GameManager.onNextRound += Reset;
    }

    private void OnDisable()
    {
        GameManager.onGameChangerRound -= StartGameChanger;
        GameManager.onNextRound -= Reset;
    }

    private void Start()
    {
        isActive = false;
        onCooldown = false;
        canActivate = false;

        rhythmCanvas.enabled = false;
        oppositeDayCanvas.enabled = false;
    }

    private void ActivateGameChanger()
    {
        // Check if player can activate game changer.
        if (isActive || onCooldown) return;

        canActivate = true;
        isActive = true;
        onCooldown = true;

        onGameChangerActivated.Invoke(); // To let game manager know.
    }

    /// <summary>
    /// Starts a random game changer and sets its canvas to active.
    /// </summary>
    public void StartGameChanger()
    {
        activeCanvas = Random.Range(0, numOfGameChangers);

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
        }
    }

    private void Reset()
    {
        //canvases[activeCanvas].enabled = false;
        //hitboxParent.SetActive(false);
    }
}