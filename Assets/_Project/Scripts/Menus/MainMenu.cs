using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        //DataPersistenceManager.Instance.NewGame();
        //SceneManager.LoadSceneAsync("Main game");
        //DisableMenuButtons();
        Debug.Log("Game has started...");
        SceneManager.LoadScene("Level 1");
    }
    public void OnContinueGameClicked()
    {
        //SceneManager.LoadSceneAsync(SceneManagement.GetInstance().sceneToLoad);
        Debug.Log("Game loaded the last known level saved...");
    }


    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
