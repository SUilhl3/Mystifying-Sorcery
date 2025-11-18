using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void OnContinueGameClicked()
    {
        //SceneManager.LoadSceneAsync(SceneManagement.GetInstance().sceneToLoad);
        Debug.Log("Game loaded the last known level saved...");
    }
    public void GoToMainMenu()
    {
        //SceneManager.LoadScene("Main Menu");
        Debug.Log("Entered main menu screen!");
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
