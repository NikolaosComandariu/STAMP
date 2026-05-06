using System;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{ 
    // Nikolaos Comandariu.

    //[SerializeField] private char acceptKey;
    //[SerializeField] private char declineKey;
    //[SerializeField] private bool isPlayer1;

    [Header("Input")]
    [SerializeField] private KeyCode acceptKey;
    [SerializeField] private KeyCode declineKey;

    private bool oppositeDay;

    public static event Action onBothInputsPressed;
    //AudioManager audioManager;

    [SerializeField] public ObjectSpawner objectSpawner;
    AudioManager audioManager;

    //private void Awake()
    //{
    //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //}

    private void OnEnable()
    {
        GameChangerManager.onOppositeDayActivated += OppositeDayActivated;
        GameManager.onNextRound += OppositeDayOver;
    }

    private void OnDisable()
    {
        GameChangerManager.onOppositeDayActivated -= OppositeDayActivated;
        GameManager.onNextRound -= OppositeDayOver;
    }

    private void Start()
    {
        oppositeDay = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(acceptKey))
        {
            if ( objectSpawner.InputAllowed)
            {
                //Debug.Log("Accept");
                OnAcceptPressed();
            }
            else
            {
                //play audio to signify decision blocked and player needs to wait
            }
        }
        if (Input.GetKeyDown(declineKey))
        {
            if ( objectSpawner.InputAllowed)
            {
                //Debug.Log("Decline");
                OnDeclinePressed();
            }
            else
            {
                //play audio to signify decision blocked and player needs to wait
            }
            //Debug.Log("Accept");
            OnAcceptPressed();
        }
        if (Input.GetKeyDown(declineKey))
        {
            //Debug.Log("Decline");
            OnDeclinePressed();
        }
    } // End of Nikolaos Comandariu.

    public void OnAcceptPressed()
    {
        Debug.Log("accepted");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();

        if(oppositeDay)
        {
            spawner.DeclineObject();
            //Debug.Log("Declined! TEST");
        }
        else
        {
            spawner.AcceptObject();
        }

        //audioManager.PlaySFX(audioManager.correctChoiceSFX);
    }

    public void OnDeclinePressed() 
    {
        Debug.Log("Declined");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();

        if (oppositeDay)
        {
            spawner.AcceptObject();
        }
        else
        {
            spawner.DeclineObject();
        }
    }

    private void OppositeDayActivated()
    {
        oppositeDay = true;
    }

    private void OppositeDayOver()
    {
        oppositeDay = false;
    }
}