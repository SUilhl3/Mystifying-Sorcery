using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
