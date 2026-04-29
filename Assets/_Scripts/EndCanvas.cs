using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    private void OnEnable()
    {
        GameManager.onGameOver += EnableCanvas;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= EnableCanvas;
    }

    private void EnableCanvas()
    {
        Time.timeScale = 0.0f;
        gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}