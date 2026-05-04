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

    [Header("Events")]
    public System.Action onBothInputsPressed;
    //AudioManager audioManager;

    AudioManager audioManager;

    //private void Awake()
    //{
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //}

    private void Update()
    {
        if (Input.GetKeyDown(acceptKey))
        {
            Debug.Log("Accept");
            OnAcceptPressed();
        }
        if (Input.GetKeyDown(declineKey))
        {
            Debug.Log("Decline");
            OnDeclinePressed();
        }
        if (Input.GetKeyDown(acceptKey) && Input.GetKeyDown(declineKey))
        {
            onBothInputsPressed.Invoke();
        }

        /*if(isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Accept");
                OnAcceptPressed();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Decline");
                OnDeclinePressed();
            }
        }
        else if (!isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Accept");
                OnAcceptPressed();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Decline");
                OnDeclinePressed();
            }
        }*/
    } // End of Nikolaos Comandariu.

    public void OnAcceptPressed()
    {
        Debug.Log("accepted");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();
        spawner.AcceptObject();
        //audioManager.PlaySFX(audioManager.correctChoiceSFX);
        
    }

    public void OnDeclinePressed() 
    {
        Debug.Log("Declined");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();
        spawner.DeclineObject();
    }

}
