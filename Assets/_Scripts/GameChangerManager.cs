using UnityEngine;

public class GameChangerManager : MonoBehaviour
{
    [Header("Events")]
    public System.Action onGameChangerActivated;

    private bool isActive;
    private bool onCooldown;
    private bool canActivate;

    private void Start()
    {
        // Subscribe to both inputs pressed event.
        ButtonClick.onBothInputsPressed += ActivateGameChanger;

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