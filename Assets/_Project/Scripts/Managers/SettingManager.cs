using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    public GameObject settingPanel;
    [SerializeField] Slider volumeSlider;
    public AudioSource volumeSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingPanel.SetActive(false);
            Debug.Log("ESC Key Pressed!");
        }
    }

    public void SettingPanel()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ChangeVolume()
    {
        volumeSource.volume = volumeSlider.value;
    }
}
