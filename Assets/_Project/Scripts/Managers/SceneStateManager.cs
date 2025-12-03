using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager instance;
    public string CurrentSceneName;
    public string LastSceneName;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);

        CurrentSceneName = SceneManager.GetActiveScene().name;
        LastSceneName = CurrentSceneName;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(CurrentSceneName == "GameOver" || CurrentSceneName == "Main Menu")
        {

        }
        else{LastSceneName = CurrentSceneName;}
        CurrentSceneName = scene.name;
        Debug.Log("Scene loaded: " + CurrentSceneName);
        Debug.Log("Last scene: " + LastSceneName);
    }
}
