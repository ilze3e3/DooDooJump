using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsHandler : MonoBehaviour
{

    public AudioMixer masterMixer;
    float currentMasterVol;
    float currentMusicVol;
    float currentSFXVol;
    bool isMuted;

    [SerializeField]Slider masterSlider;
    [SerializeField]Slider musicSlider;
    [SerializeField]Slider sfxSlider;
    [SerializeField]Toggle muteToggle;

    private void Start()
    {
        currentMasterVol = PlayerPrefs.GetFloat("MasterVolume", 0);
        currentMusicVol = PlayerPrefs.GetFloat("MusicVolume", 0);
        currentSFXVol = PlayerPrefs.GetFloat("SFXVolume", 0);
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            isMuted = false;
        }
        else
        {
            isMuted = true;
        }

        masterSlider.value = currentMasterVol;
        musicSlider.value = currentMusicVol;
        sfxSlider.value = currentSFXVol;
        muteToggle.isOn = isMuted;
    }
    public void ChangeMasterVolume(float _vol)
    {
        currentMasterVol = _vol;
        masterMixer.SetFloat("MasterVolume", _vol);
    }

    public void ChangeMusicVolume(float _vol)
    {
        currentMusicVol = _vol;
        masterMixer.SetFloat("MusicVolume", _vol);
    }

    public void ChangeSFXVolume(float _vol)
    {
        currentSFXVol = _vol;
        masterMixer.SetFloat("SFXVolume", _vol);
    } 

    public void ToggleMuteMasterVolume(bool _mute)
    {
        isMuted = _mute;
        if(_mute)
        {
            masterMixer.SetFloat("MasterVolume", -80.0f);
        }
        else
        {
            masterMixer.SetFloat("MasterVolume", currentMasterVol);
        }
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", currentMasterVol);
        PlayerPrefs.SetFloat("MusicVolume", currentMusicVol);
        PlayerPrefs.SetFloat("SFXVolume", currentSFXVol);
        if (isMuted) PlayerPrefs.SetInt("Mute", 1);
        else PlayerPrefs.SetInt("Mute", 0);
    }
}
