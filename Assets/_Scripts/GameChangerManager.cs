using System;
using UnityEngine;

public class GameChangerManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas[] canvases;

    [Header("Game Objects")]
    [SerializeField] private GameObject hitboxParent;

    // Events.
    public static event Action onGameChangerActivated;

    private bool isActive;
    private bool onCooldown;
    private bool canActivate;

    private int activeCanvas;

    private void OnEnable()
    {
        GameManager.onGameChangerRound += StartGameChanger;
        //ButtonClick.onBothInputsPressed += ActivateGameChanger;
        GameManager.onNextRound += Reset;
    }

    private void OnDisable()
    {
        GameManager.onGameChangerRound -= StartGameChanger;
        //ButtonClick.onBothInputsPressed -= ActivateGameChanger;
        GameManager.onNextRound -= Reset;
    }

    private void Start()
    {
        isActive = false;
        onCooldown = false;
        canActivate = false;

        canvases[activeCanvas].enabled = false;
        hitboxParent.SetActive(false);
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

    public void StartGameChanger()
    {
        canvases[activeCanvas].enabled = true;
        hitboxParent.SetActive(true);
    }

    private void Reset()
    {
        canvases[activeCanvas].enabled = false;
        hitboxParent.SetActive(false);
    }
}