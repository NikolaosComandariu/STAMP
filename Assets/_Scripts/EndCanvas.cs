using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Subscribe to delegate.
    /// </summary>
    private void OnEnable()
    {
        GameManager.onGameOver += EnableCanvas;
    }

    /// <summary>
    /// Unsubscribe from delegate.
    /// </summary>
    private void OnDisable()
    {
        GameManager.onGameOver -= EnableCanvas;
    }

    /// <summary>
    /// Set time scale to 0 and enable canvas component.
    /// </summary>
    private void EnableCanvas()
    {
        Time.timeScale = 0.0f;
        gameObject.GetComponent<Canvas>().enabled = true;
    }

    /// <summary>
    /// Set time scale to 1 and load main menu scene.
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary>
    /// Set time scale to 1 and reload active scene.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}