using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{ 
    // Nikolaos Comandariu.

    //[SerializeField] private char acceptKey;
    //[SerializeField] private char declineKey;
    //[SerializeField] private bool isPlayer1;

    [Header("Input")]
    [SerializeField] private KeyCode acceptKey;
    [SerializeField] private KeyCode declineKey;

    [Header("Variables")]
    [SerializeField] private bool isPlayer1;

    private bool oppositeDay;
    private bool inputAllowed;
    private bool isAccept = false;

    public static event Action onBothInputsPressed;
    public static event Action<bool, bool> onInputDetected;

    AudioManager audioManager;

    private void OnEnable()
    {
        GameChangerManager.onOppositeDayActivated += OppositeDayActivated;
        GameManager.onNextRound += OppositeDayOver;

        if (isPlayer1)
        {
            ObjectSpawner.onInputAllowedP1 += InputAllowed;
        }
        else
        {
            ObjectSpawner.onInputAllowedP2 += InputAllowed;
        }
    }

    private void OnDisable()
    {
        GameChangerManager.onOppositeDayActivated -= OppositeDayActivated;
        GameManager.onNextRound -= OppositeDayOver;

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

    private void Update()
    {
        if (Input.GetKeyDown(acceptKey))
        {
            Debug.Log("Accept - TEST");
            OnAcceptPressed();
        }
        if (Input.GetKeyDown(declineKey))
        {
            Debug.Log("Decline - TEST");
            OnDeclinePressed();
        }
    } // End of Nikolaos Comandariu.

    public void OnAcceptPressed()
    {
        isAccept = true;
        Debug.Log("accepted");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();

        if (inputAllowed)
        {
            if (oppositeDay)
            {
                spawner.DeclineObject();
                //Debug.Log("Declined! TEST");
            }
            else
            {
                spawner.AcceptObject();
            }
        }

        onInputDetected?.Invoke(isPlayer1, isAccept);

        Debug.Log("Input allowed: " + inputAllowed);

        //audioManager.PlaySFX(audioManager.correctChoiceSFX);

        // Trigger accept event for P1 if P1, else do P2 accept
    }

    public void OnDeclinePressed() 
    {
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

    private void OppositeDayActivated()
    {
        oppositeDay = true;
    }

    private void OppositeDayOver()
    {
        oppositeDay = false;
    }

    private void InputAllowed(bool allowed)
    {
        inputAllowed = allowed;
        Debug.Log("Input allowed was set to: " + inputAllowed);
    }
}