using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public AudioSource audioSource;

    private const string VolumeKey = "musicVolume";

    void Start()
    {
        if (!PlayerPrefs.HasKey(VolumeKey))
        {
            PlayerPrefs.SetFloat(VolumeKey, 1f);
            PlayerPrefs.Save();
        }

        Load();
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;
    }
}
