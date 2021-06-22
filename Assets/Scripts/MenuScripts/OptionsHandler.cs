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
        if(_mute)
        {
            masterMixer.SetFloat("MasterVolume", -80.0f);
        }
        else
        {
            masterMixer.SetFloat("MasterVolume", currentMasterVol);
        }
    }
}
