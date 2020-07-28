using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioTrack : MonoBehaviour
{
    private AudioManager audioManager;
    public int newTrackID;
    public bool noRepeat;
    public bool playOnStart;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if(playOnStart)
        {
            audioManager.PlayNewTrack(newTrackID);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.name.Equals("Player"))
        {
            audioManager.PlayNewTrack(newTrackID);
            if(noRepeat) gameObject.SetActive(false);
        }
    }
}
