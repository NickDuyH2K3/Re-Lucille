using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolumeSettings();
        }
        else
        {
            SetSfxVolume();
            SetMusicVolume();
        }
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolumeSettings()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetSfxVolume();
        SetMusicVolume();
    }
}
