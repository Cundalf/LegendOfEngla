using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    private AudioVolumeController[] audios;
    [Range(0, 1)]
    public float maxVolumeLevel;
    [Range(0, 1)]
    public float CurrentVolumeLevel;


    void Start()
    {
        audios = FindObjectsOfType<AudioVolumeController>();
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.MUSIC);
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.SFX);
    }

    public void ChangeGlobalAudioVolume(AudioVolumeController.AudioType type)
    {
        if(CurrentVolumeLevel >= maxVolumeLevel)
        {
            CurrentVolumeLevel = maxVolumeLevel;
        }

        foreach(AudioVolumeController ac in audios)
        {
            if(ac.type == type)
            {
                ac.SetAudioLevel(CurrentVolumeLevel);
            }
        }
    }

    public void AudioChanged(Slider audioSlide)
    {
        CurrentVolumeLevel = audioSlide.value;
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.MUSIC);
    }

    public void SFXChanged(Slider audioSlide)
    {
        CurrentVolumeLevel = audioSlide.value;
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.SFX);
    }
}
