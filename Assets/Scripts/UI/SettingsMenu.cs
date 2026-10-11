using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume(musicSlider.value);
        }
    }

    public void SetMusicVolume(float volume)
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);

    } 

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume(musicSlider.value);
    }
}
