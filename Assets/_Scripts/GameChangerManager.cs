using System;
using UnityEngine;

public class GameChangerManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas canvas;

    // Events.
    public static event Action onGameChangerActivated;

    private bool isActive;
    private bool onCooldown;
    private bool canActivate;

    private void OnEnable()
    {
        GameManager.onGameChangerRound += StartGameChanger;
        ButtonClick.onBothInputsPressed += ActivateGameChanger;
    }

    private void OnDisable()
    {
        GameManager.onGameChangerRound -= StartGameChanger;
        ButtonClick.onBothInputsPressed -= ActivateGameChanger;
    }

    private void Start()
    {
        isActive = false;
        onCooldown = false;
        canActivate = false;
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
        if (!canActivate) return;

        
    }
}