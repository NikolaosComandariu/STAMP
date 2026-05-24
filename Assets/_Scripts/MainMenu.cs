using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  /// <summary>
  /// PlayGame launches the Rundown Scene while QuitGame closes the application
  /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("TutorialScene");
        
    }

    public void GoToMainGame()
    {
        SceneManager.LoadScene("BenScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
