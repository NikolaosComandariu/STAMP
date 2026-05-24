using Unity.VisualScripting;
using UnityEngine;

public class HideButtons : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject[] buttons;

    /// <summary>
    /// Subscribe to events.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onGameOver += DeactivateButtons;
    }

    /// <summary>
    /// Unsubscribe from events.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onGameOver -= DeactivateButtons;
    }

    /// <summary>
    /// Enable all buttons.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    /// <summary>
    /// Disable all buttons.
    /// </summary>
    private void DeactivateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }
}