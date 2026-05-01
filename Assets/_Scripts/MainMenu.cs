using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  /// <summary>
  /// PlayGame launches the Rundown Scene while QuitGame closes the application
  /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("RundownScene");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
