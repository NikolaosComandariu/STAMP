using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonClick : MonoBehaviour
{

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            OnAcceptPressed();
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            OnDeclinePressed();
        }    
    }

    public void OnAcceptPressed()
    {
        Debug.Log("accepted");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();
        spawner.AcceptObject();
        
    }

    public void OnDeclinePressed() 
    {
        Debug.Log("Declined");
        ObjectSpawner spawner = GetComponent<ObjectSpawner>();
        spawner.DeclineObject();
    }

}
