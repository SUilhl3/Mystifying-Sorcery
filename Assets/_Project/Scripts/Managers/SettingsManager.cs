using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsPanel.SetActive(false);
            Debug.Log("ESC Key Pressed!");
        }
    }

    public void SettingPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void Close()
    {
        settingsPanel.SetActive(false);
    }
}
