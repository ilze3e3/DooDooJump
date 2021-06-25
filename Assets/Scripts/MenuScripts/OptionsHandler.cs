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

    /// <summary>
    /// Grab data from player prefs if audio settings has been changed before and set
    /// the UI accordingly
    /// </summary>
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
    /// <summary>
    /// Changes the master volume
    /// </summary>
    /// <param name="_vol"></param>
    public void ChangeMasterVolume(float _vol)
    {
        currentMasterVol = _vol;
        masterMixer.SetFloat("MasterVolume", _vol);
    }
    /// <summary>
    /// Changes the background music volume
    /// </summary>
    /// <param name="_vol"></param>
    public void ChangeMusicVolume(float _vol)
    {
        currentMusicVol = _vol;
        masterMixer.SetFloat("MusicVolume", _vol);
    }
    /// <summary>
    /// Changes the Sound effects volume
    /// </summary>
    /// <param name="_vol"></param>
    public void ChangeSFXVolume(float _vol)
    {
        currentSFXVol = _vol;
        masterMixer.SetFloat("SFXVolume", _vol);
    } 
    /// <summary>
    /// Toggle mute or unmute all sounds
    /// </summary>
    /// <param name="_mute"></param>
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
    /// <summary>
    /// Upon exiting options, save all settings accordingly.
    /// </summary>
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", currentMasterVol);
        PlayerPrefs.SetFloat("MusicVolume", currentMusicVol);
        PlayerPrefs.SetFloat("SFXVolume", currentSFXVol);
        if (isMuted) PlayerPrefs.SetInt("Mute", 1);
        else PlayerPrefs.SetInt("Mute", 0);
    }
}
