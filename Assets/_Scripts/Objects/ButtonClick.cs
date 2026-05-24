using System;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class ButtonClick : MonoBehaviour
{ 
    // Nikolaos Comandariu.
    [Header("Input")]
    [SerializeField] private KeyCode acceptKey;
    [SerializeField] private KeyCode declineKey;

    [Header("Variables")]
    [SerializeField] private bool isPlayer1;

    private bool oppositeDay;
    private bool inputAllowed;
    private bool isAccept = false;
    private bool GameEnded = false;

    public static event Action onBothInputsPressed;
    public static event Action<bool, bool> onInputDetected;
    public static event Action<bool> onP1PressedButton;
    public static event Action<bool> onP2PressedButton;

    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        GameChangerManager.onOppositeDayActivated += OppositeDayActivated;
        GameManager.onNextRound += OppositeDayOver;
        GameManager.onGameOver += GameOver;
        
        if (isPlayer1)
        {
            ObjectSpawner.onInputAllowedP1 += InputAllowed;
        }
        else
        {
            ObjectSpawner.onInputAllowedP2 += InputAllowed;
        }
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        GameChangerManager.onOppositeDayActivated -= OppositeDayActivated;
        GameManager.onNextRound -= OppositeDayOver;
        GameManager.onGameOver -= GameOver;

        if (isPlayer1)
        {
            ObjectSpawner.onInputAllowedP1 -= InputAllowed;
        }
        else
        {
            ObjectSpawner.onInputAllowedP2 -= InputAllowed;
        }
    }

    private void Start()
    {
        oppositeDay = false;
        inputAllowed = true;
    }

    /// <summary>
    /// Check for player inputs.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(acceptKey))
        {
            //Debug.Log("Accept - TEST");
            OnAcceptPressed();
        }
        if (Input.GetKeyDown(declineKey))
        {
            //Debug.Log("Decline - TEST");
            OnDeclinePressed();
        }
    } // End of Nikolaos Comandariu.

    /// <summary>
    /// Checks if input is allowed, if it is, it checks if it's opposite day
    /// and either accepts the object or declines the object. 
    /// It also sends an event to the ButtonPress.cs script so it knows
    /// which player has pressed a button and whether it was accept or decline.
    /// </summary>
    public void OnAcceptPressed()
    {
        // If game over, send bool event to EndCanvas.cs and return;
        if(GameEnded)
        {
            if (isPlayer1)
                onP1PressedButton?.Invoke(true);
            else
                onP2PressedButton?.Invoke(true);

            return;
        }

        isAccept = true;
        Debug.Log("accepted");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();

        if (inputAllowed)
        {
            if (oppositeDay)
            {
                spawner.DeclineObject();
            }
            else
            {
                spawner.AcceptObject();
            }
        }

        onInputDetected?.Invoke(isPlayer1, isAccept); // Tells ButtonPress.cs that an input

        Debug.Log("Input allowed: " + inputAllowed);
    }

    /// <summary>
    /// Checks if input is allowed, if it is, it checks if it's opposite day
    /// and either accepts the object or declines the object. 
    /// It also sends an event to the ButtonPress.cs script so it knows
    /// which player has pressed a button and whether it was accept or decline.
    /// </summary>
    public void OnDeclinePressed() 
    {
        // If game over, send bool event to EndCanvas.cs and return;
        if (GameEnded)
        {
            if (isPlayer1)
                onP1PressedButton?.Invoke(true);
            else
                onP2PressedButton?.Invoke(true);

            return;
        }

        isAccept = false;
        Debug.Log("Declined");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();

        if (inputAllowed)
        {
            if (oppositeDay)
            {
                spawner.AcceptObject();
            }
            else
            {
                spawner.DeclineObject();
            }
        }

        onInputDetected?.Invoke(isPlayer1, isAccept);
    }

    private void OppositeDayActivated() // Nikolaos Comandariu.
    {
        oppositeDay = true;
    }

    private void OppositeDayOver() // Nikolaos Comandariu.
    {
        oppositeDay = false;
    }

    private void InputAllowed(bool allowed) // Josh
    {
        inputAllowed = allowed;
        Debug.Log("Input allowed was set to: " + inputAllowed);
    }

    private void GameOver()
    {
        GameEnded = true;
    }
}