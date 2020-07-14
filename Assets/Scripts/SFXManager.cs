using System.Collections;
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

    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }

        sharedInstance = this;
    }

    public enum SFXType
    {
        ATTACK, DIE, HIT, KNOCK, MISSIONSTART, MISSIONEND
    }
    public AudioSource attack, die, hit, knock, missionEnd, missionStart;

    public void PlaySFX(SFXType type)
    {
        switch (type)
        {
            case SFXType.ATTACK:
                attack.Play();
                break;
            case SFXType.DIE:
                die.Play();
                break;
            case SFXType.HIT:
                hit.Play();
                break;
            case SFXType.KNOCK:
                knock.Play();
                break;
            case SFXType.MISSIONSTART:
                missionStart.Play();
                break;
            case SFXType.MISSIONEND:
                missionEnd.Play();
                break;
        }
    }
}