using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    private const string VolumeKey = "musicVolume";
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        audioSource.volume = savedVolume;
    }

}
