﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Singleton
    private static SFXManager sharedInstance = null;

    public static SFXManager SharedInstance
    {
        get
        {
            return sharedInstance;
        }
    }

    private List<GameObject> audios;

    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }

        sharedInstance = this;
        DontDestroyOnLoad(this);

        audios = new List<GameObject>();
        GameObject sounds = GameObject.Find("Sounds");
        foreach(Transform t in sounds.transform)
        {
            audios.Add(t.gameObject);
        }
    }

    public AudioSource FindAudioSource(SFXType.SoundType type)
    {
        foreach(GameObject g in audios)
        {
            if(g.GetComponent<SFXType>().type == type)
            {
                return g.GetComponent<AudioSource>();
            }
        }

        return null;
    }

    public void PlaySFX(SFXType.SoundType type)
    {
        FindAudioSource(type).Play();
    }
}