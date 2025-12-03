using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnContinueGameClicked()
    {
        SceneManager.LoadSceneAsync(SceneStateManager.instance.LastSceneName);
        Debug.Log("Game loaded the last known level saved...");
    }
    public void GoToMainMenu()
    {
        //SceneManager.LoadScene("Main Menu");
        Debug.Log("Entered main menu screen!");
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
