using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioTracks;
    public int currentTrack;
    public bool audioCanBePlayed;

    void Update()
    {
        if(audioCanBePlayed)
        {
            if (!audioTracks[currentTrack].isPlaying)
            {
                audioTracks[currentTrack].Play();
            }
        }
        else
        {
            audioTracks[currentTrack].Stop();
        }
    }

    public void PlayNewTrack(int newTrack)
    {
        audioTracks[currentTrack].Stop();
        currentTrack = newTrack;
        audioTracks[currentTrack].Play();
    }
}
